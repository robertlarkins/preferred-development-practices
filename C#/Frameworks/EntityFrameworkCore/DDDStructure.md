# DDD Structure with Entity Framework

This uses the approach described by Vladimir Khorikov in his [*DDD and EF Core: Preserving Encapsulation* course on pluralsight](https://app.pluralsight.com/library/courses/ddd-ef-core-preserving-encapsulation).
The associated git repo is: https://github.com/vkhorikov/DddAndEFCore

## Linking ValueObjects to Entities

### Single Value

### Multiple Properties

When linking a ValueObject with multiple properties to an Entity we need to set the ValueObject property to virtual so that Entity Framework can populate it.

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
        return new Company(streetNumber, streetName, city);
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
