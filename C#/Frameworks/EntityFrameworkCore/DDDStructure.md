# DDD Structure with Entity Framework

This uses the approach described by Vladimir Khorikov in his [*DDD and EF Core: Preserving Encapsulation* course on pluralsight](https://app.pluralsight.com/library/courses/ddd-ef-core-preserving-encapsulation).
The associated git repo is: https://github.com/vkhorikov/DddAndEFCore

## Linking ValueObjects to Entities

### Single Value

### Multiple Properties

When linking a ValueObject with multiple properties to an Entity we need to set the ValueObject property to virtual so that Entity Framework can populate it.

The `private set` on the Entity properties is put in place to allow methods with specific actions to modify the property. Entity Framework does not need the `private set` as it accesses the corresponding shadow property directly for setting the value.

For example if we had wanted a list of static companies:

```C#
public class Company : Entity
{
    public static reasonly Company Acme = new Company(1, "Acme", Address.Create(123, "Fake St", "SpringField").Value);
    public static reasonly Company Xyz = new Company(1, "Xyz", Address.Create(99, "Alphabet Road", "Letterton").Value);

    protected Company()
    {
    }
    
    private Company(
        int id,
        string name,
        Address address)
        : base(id)
    {
        Name = name;
        Address = address;
    }
    
    public string Name { get; private set; } = string.Empty;
    
    public virtual Address Address { get; private set; } = null!;
}

public class Address : ValueObject
{
    protected Address()
    {
    }
    
    private Address(
        int streetNumber,
        string streetName,
        string city)
    {
        StreetNumber = streetNumber;
        StreetName = streetName;
        City = city;
    }
    
    public int StreetNumber { get; }
    
    public string StreetName { get; } = string.Empty;
    
    public string City { get; } = string.Empty;
    
    public static Result<Address> Create(
        int streetNumber,
        string streetName,
        string city)
    {
        return new Address(streetNumber, streetName, city);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StreetNumber;
        yield return StreetName;
        yield return City;
    }
}

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(k => k.id);
        
        // Straight property
        builder.Property(p => p.Name);
        
        // ValueObject with multiple property conversion
        builder.OwnsOne(p => p.Address, p =>
        {
            p.Property(pp => pp.StreetNumber).HasColumnName("street_number");
            p.Property(pp => pp.StreetName).HasColumnName("street_name");
            p.Property(pp => pp.City).HasColumnName("city");
        });
    }
}
```

> Note:
> Whether `Address` is an Entity or ValueObject depends on your application needs.

## Database Seeding
Seeding the database can be done from code, allowing the database to be updated with the latest seeding changes by doing an `add-migration` and running the solution.
One way this can be done is with an extensions class:

```C#
public class CompanyContext : DbContext
{
    public CompanyContext(DbContextOptions options) : base(options)
    {
    }
    
    public CompanyContext()
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration
        
        // Seed Data
        modelBuilder.SeedInspectionStatus();
    }
}

public class CompanyStatus : Entity<int>
{
    public static readonly CompanyStatus Open = new(1, "Open");
    public static readonly CompanyStatus Closed = new(2, "Closed");
    public static readonly CompanyStatus AfterHours = new(3, "AfterHours");
    
    protected CompanyStatus()
    {
    }
    
    private CompanyStatus(int id, string statusCode) : base(id)
    {
        StatusCode = statusCode;
    }
    
    public string StatusCode { get; private set; } = string.Empty;
}

public static class ModelBuilderSeedingExtensions
{
    public static void SeedInspectionStatus(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyStatus>().HasData(
            CompanyStatus.Open,
            CompanyStatus.Closed,
            CompanyStatus.AfterHours);
    }
}
```

### Seeding with a simple ValueObject
If an Entity has a ValueObject for a property, but the conversion is simple (see the Status code conversion)

```C#
public class StatusCode : ValueObject
{
    protected StatusCode()
    {
    }

    private StatusCode(string value)
    {
        Value = value;
    }

    public string Value { get; } = string.Empty;

    public static Result<StatusCode> Create(string code)
    {
        return new StatusCode(code.ToUpper());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToUpper();
    }
}

public class Product : Entity<int>
{
    public static readonly Product Unknown = new(1, "Unknown", StatusCode.Create("unknown").Value);
    public static readonly Product Fandangle = new(2, "Fandangle", StatusCode.Create("new_release").Value);

    protected Product()
    {
    }
    
    private Product(int id, string name, StatusCode statusCode) : base(id)
    {
        StatusCode = statusCode;
    }
    
    public string Name { get; private set; } = string.Empty;
    
    public StatusCode StatusCode { get; private set; } = null!;
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(k => k.id);
        
        // Straight property
        builder.Property(p => p.Name);
        
        // ValueObject with single property conversion
        builder.Property(p => p.StatusCode)
            .HasConversion(p => p.Value, p => StatusCode.Create(p).Value)
            .HasColumnName("product_status_code"); // The column name to use in the database for StatusCode
    }
}
```

then the seeding for the `Product` class will look like this:

```C#
public static class ModelBuilderSeedingExtensions
{
    public static void SeedProducts(this ModelBuilder modelBuilder)
    {
        var products = ProductGeneration();
    
        modelBuilder.Entity<Product>().HasData(
            Product.Unknown,
            CompanyStatus.Closed,
            CompanyStatus.AfterHours);
            
        statuc object[] ProductGeneration()
        {
            return new object[]
            {
                new { Product.Unknown.Id, Product.Unknown.Name, Product.Unknown.StatusCode },
                new { Product.Fandangle.Id, Product.Fandangle.Name, Product.Fandangle.StatusCode },
                
                // Items that are not statically defined on Product but are still seeded into the database
                new { Id = 3, Name = "Doodad", StatusCode = StatusCode.Create("generic").Value },
                new { Id = 4, Name = "Gismo", StatusCode = StatusCode.Create("generic").Value }
            }
        }
    }
}
```
For seeding `StatusCode` into the database requires that status code be passed as a StatusCode ValueObject.

### Seeding for an Entity with Owned Properties
In cases where there are owned properties which create split tables, the entity's values need to be assigned followed by
the owned properties, which are linked by the shadow primary key name.

The following shows an example of this where the `Code` and `DisplayName` are stored in a ValueObject which is an owned entity of Gender.

> Note:
> This is a simple example, `Code` and `DisplayName` would normally live in the Entity rather than having a GenderType ValueObject.

Entity and ValueObject
```C#
public class AnimalSex : Entity<int>
{
    public static readonly Gender Male = new(1, GenderType.Create("M", "Male").Value);
    public static readonly Gender Female = new(2, GenderType.Create("F", "Female").Value);
    public static readonly Gender Unknown = new(3, GenderType.Create("U", "Unknown").Value);

    protected Gender()
    {
    }

    private Gender(int id, GenderType genderType)
        : base(id)
    {
        GenderType = genderType;
    }

    public virtual GenderType GenderType { get; private set; } = null!;
}

public class GenderType : ValueObject
{
    protected GenderType()
    {
    }

    private GenderType(string code, string displayName)
    {
        Code = code;
        DisplayName = displayName;
    }

    public string Code { get; } = string.Empty;
    public string DisplayName { get; } = string.Empty;

    public static Result<GenderType> Create(string code, string displayName)
    {
        return new GenderType(code, displayName);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
```

The Configuration
```C#
public void Configure(EntityTypeBuilder<Gender> builder)
{
    builder.HasKey(k => k.Id);

    builder.OwnsOne(
        p => p.GenderType, p =>
        {
            p.Property(pp => pp.Code).HasColumnName("gender_type_code");
            p.Property(pp => pp.DisplayName).HasColumnName("display_name");
        });
}
```
Seeding:
```C#
modelBuilder.Entity<Gender>().HasData(
    new { Id = 1 },
    new { Id = 2 },
    new { Id = 3 });

modelBuilder.Entity<Gender>().OwnsOne(p => p.GenderType).HasData(
    new { GenderId = 1, Code = "M", DisplayName = "Male" },
    new { GenderId = 2, Code = "F", DisplayName = "Female" },
    new { GenderId = 3, Code = "U", DisplayName = "Unknown" });
```

This can alternatively be written like this:
```C#
modelBuilder.Entity<Gender>(
    e =>
    {
        e.HasData(
            new { Id = 1 },
            new { Id = 2 },
            new { Id = 3 });
        e.OwnsOne(p => p.GenderType).HasData(
            new { GenderId = 1, Code = "M", DisplayName = "Male" },
            new { GenderId = 2, Code = "F", DisplayName = "Female" },
            new { GenderId = 3, Code = "U", DisplayName = "Unknown" });
    });
```

See:
 - https://csharp.christiannagel.com/2018/09/12/efcoreseeding/
 - https://stackoverflow.com/questions/50862525/seed-entity-with-owned-property
 - https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
 - https://stackoverflow.com/questions/55172288/avoid-exposing-private-collection-properties-to-entity-framework-ddd-principles

Once the seeding data has been added to the code, the next step is to add a new migration.

> Note:
> If the database already has the same seeded data, then the database data needs to be removed and the primary key reset.
> See section [Truncate a Table](#truncate-a-table) for how to do this.

### Further Notes
If the Entity can not be instantiated directly with an id or does not have a hardcoded static type,
then EF Core can accept anonymous types with properties defined. The following provides more detail:
 - https://docs.microsoft.com/en-us/archive/msdn-magazine/2018/august/data-points-deep-dive-into-ef-core-hasdata-seeding#hasdata-with-anonymous-types

Additional `HasData` info can be found in the same article: https://docs.microsoft.com/en-us/archive/msdn-magazine/2018/august/data-points-deep-dive-into-ef-core-hasdata-seeding

### Adding a column to an already seeded table
If a table has already been seeded and a new column is to be added (that may reference another entity/table), you may get the following error:

> The seed entity for entity type 'MyEntity' cannot be added because there was no value provided for the required property 'MyEntityTypeId'.

It is likely that this is due to the seeding not able to link the `MyEntityType.Id` to a column called `MyEntityTypeId`.

If the seeding code was simply
```C#
modelBuilder.Entity<MyEntity>().HasData(
                MyEntity.Unknown,
                MyEntity.One,
                MyEntity.Two);
```

Then converting it to look something like this should work:
```C#
var myEntities = new[] {
    MyEntity.Unknown,
    MyEntity.One,
    MyEntity.Two
};
var seedValues = MyEntitySeedDataGeneration(myEntities);

modelBuilder.Entity<MyEntity>().HasData(seedValues);

static List<object> MyEntitySeedDataGeneration(
    IEnumerable<MyEntity> myEntities)
{
    return myEntities.Select(
        myEntity => (object)new
        {
            myEntity.Id,
            myEntity.SomeProperty,
            MyEntityTypeId = myEntity.MyEntityType.Id
        }).ToList();
}
```
