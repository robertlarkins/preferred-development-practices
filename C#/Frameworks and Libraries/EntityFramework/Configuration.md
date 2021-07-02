# Fluent API Mapping Configuration

## One-to-One Relationship
The relationship between two classes, such as a `Student` and their `ContactDetails`.
Setting up this relationship with the `ModelBuilder`, this written like this:
```C#
modelBuilder.Entity<Student>()
  .HasOne(p => p.ContactDetails)
  .WithOne()
  .HasForeignKey<ContactDetails>();
  
modelBuilder.Entity<ContactDetails>()
  .HasOne(p => p.Student)
  .WithOne()
  .HasForeignKey<ContactDetails>();
```

> Note:
> In a one-to-one mapping the primary key and the foreign key get the same value.
> So it might be possible that only the primary key is needed?
> The information for this comes from [here](https://stackoverflow.com/a/51313916/1926027)

See also:
 - https://www.learnentityframeworkcore.com/configuration/one-to-one-relationship-configuration
 - https://www.entityframeworktutorial.net/efcore/configure-one-to-one-relationship-using-fluent-api-in-ef-core.aspx

## Index

An index is a mechanism for making lookups based on a column more efficient.

```C#
modelBuilder.Entity<Blog>()
  .HasIndex(b => b.Url);
```

See: https://docs.microsoft.com/en-us/ef/core/modeling/indexes?tabs=fluent-api

## Unique Index

Be default indexes are not unique, that is, multiple rows can have the same value(s) for an index.
If an index must contain unique values, then it can be made unique by doing:

```C#
modelBuilder.Entity<Blog>()
  .HasIndex(b => b.Url)
  .IsUnique();
```

See: https://docs.microsoft.com/en-us/ef/core/modeling/indexes?tabs=fluent-api#index-uniqueness

## Alternate Key

An alternate key can be used instead of a unique index/constraint to provide an addition to the primary key.
This allows it to be used as the target of a relationship, such as being the target of a foreign key.

A composite key can be formed using this syntax
```C#
modleBuilder.Entity<MyModel>()
  .HasAlternateKey(x => new { x.PropOneId, x.PropTwoId });
```
but it requires `PropOneId` and `PropTwoId` be actual properties on the class.
If these correspond to navigation properties `PropOne` and `PropTwo`, then the syntax can be written as
```C#
modleBuilder.Entity<MyModel>()
  .HasAlternateKey("PropOneId", "PropTwoId");
```
which leverages the [Shadow Properties](https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties) on the class.
The EF Core naming convention for shadow properties can be found [here](https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties#foreign-key-shadow-properties).

See also:
 - https://stackoverflow.com/a/56519109/1926027
 - https://docs.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations#alternate-keys

## Ignored and Ignoring Properties

### Automatically Ignored Properties
By convention Entity Framework automatically [ignores read-only properties](https://docs.microsoft.com/en-us/ef/core/modeling/constructors#read-only-properties).

An example of this is the `FullName` property which is ignored by EF Core:
```C#
public class Student : Entity<int>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    public string FullName => ConstructFullName();
    
    private string ConstructFullName()
    {
        if (string.IsNullOrEmpty(LastName))
        {
	    return FirstName;
        }

        if (string.IsNullOrEmpty(FirstName))
        {
	    return LastName;
        }

        return $"{FirstName} {LastName}";
    }
}
```
Explicitely mapping `FullName` will cause a migration exception as it has no setter and no backing field.

### Manually Ignoring Properties
Sometimes properties in an entity need to be explicitely ignored in the Configuration file.
This occurs if an entity needs a property in code, but does not need to be persisted to a database, or to stop EF Core performing some other undesired migration behaviour.

The simplest example is adding a property with both a public getter and setter:
```C#
public class Address : Entity
{
    public DateTime LoadedFromDatabase { get; set; }
}
```
then the Context can ignore `LoadedFromDatabase` by doing this:
```C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Address>()
        .Ignore(p => p.LoadedFromDatabase);
}
```


Another example is `CancelledAppointments` getter property, which triggers EF to produce an additional foreign key index (possibly it is seen as another navigation property)
```C#
public class Schedule : Entity<int>
{
    private readonly List<Appointment> appointments = new();
    
    protected Schedule() { } // Protected Constructor needed for lazy-loading 

    public virtual IReadOnlyList<Appointment> Appointments => appointments.AsReadOnly()
    
    public List<Appointment> CancelledAppointments => Appointments.Where(x => x.Status == AppointmentStatus.Cancelled);
}
```

`CancelledAppointments` can be ignored the same as above
```C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Address>()
        .Ignore(p => p.CancelledAppointments);
}
```


# PostgreSQL Item Naming
The recommended naming convention for PostGres tables and columns is snake case without pluralisation, eg: `student_course`, or `first_name`.
To allow a direct conversion from Entity Framework Entity classes, the following can be added to the `dbContext.cs` class

See:
- https://martendb.io/documentation/postgres/naming/
- https://stackoverflow.com/questions/2878248/postgresql-naming-conventions
- https://stackoverflow.com/questions/338156/table-naming-dilemma-singular-vs-plural-names

```C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Add entity configurations
    // Add seed data

    modelBuilder.RemovePluralizingTableNameConvention();
    modelBuilder.CustomiseEfCoreNamingConventionsForPostgreSql();
}
```

These two extension methods remove pluralisation and convert the postgres naming to snake case. They can be added by creating a `ModelBuilderExtensions` class.

```C#
using Humanizer;

internal static class ModelBuilderExtensions
{
    /// <summary>
    /// Removes pluralization of table names from the table creation.
    /// </summary>
    /// <remarks>
    /// Code obtained from:
    /// https://stackoverflow.com/questions/37493095/entity-framework-core-rc2-table-name-pluralization
    /// to remove pluralisation.
    /// The following details how to ignore entities that are owned (such as split tables):
    /// https://stackoverflow.com/questions/61565155/how-do-i-find-out-if-a-clrtype-is-owned-in-ef-core .
    /// </remarks>
    /// <param name="modelBuilder">modelBuilder.</param>
    public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            // Owned Entities, such as those formed from ValueObjects with multiple properties (split tables)
            // need to be ignored, otherwise this will create separate tables for them, rather than a single
            // table to hold all properties.
            if (entity.IsOwned())
            {
                continue;
            }
	          
            // By default the table name is the property name on the dbContext (if set), which has an 's' on the end.
            // Where as the .DisplayName() is the class name of the entity.
            // Because of this difference, we remove the pluralisation of the tableName. While we could take the displayName
	    // this does not account for using the FluentAPI method .ToTable("some_name") being used.
	    // Or a different DbSet property name being chosen.
            // Therefore we just take the tableName and singularize it.
            var tableName = entity.GetTableName();
	    var tableNameAsSingular = tableName.Singularize(false);

            entity.SetTableName(tableNameAsSingular);
        }
    }
    
    /// <summary>
    /// Rename table name, column name, key name and index name to snake_case i.e. lower case, word separated by underscore.
    /// </summary>
    /// <param name="modelBuilder">modelBuilder.</param>
    public static void CustomiseEfCoreNamingConventionsForPostgreSql(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName().ToSnakeCase();
            
            entity.SetTableName(tableName);
            
            foreach (var property in entity.GetProperties())
            {
                var columnNameInSnakeCase = property
                    .GetColumnName()
                    .ToSnakeCase();
                
                property.SetColumnName(columnNameInSnakeCase);
            }
            
            foreach (var key in entity.GetKeys())
            {
                var keyNameInSnakeCase = key.GetName().ToSnakeCase();
                
                key.SetName(keyNameInSnakeCase);
            }
            
            foreach (var key in entity.GetForeignKeys())
            {
               var constraintNameInSnakeCase = key.GetConstraintName().ToSnakeCase();
               
               key.SetConstraintName(constraintNameInSnakeCase);
            }
            
            foreach (var index in entity.GetIndexes())
            {
               var databaseNameInSnakeCase = index.GetName().ToSnakeCase();
               
               index.SetName(databaseNameInSnakeCase);
            }
        }
    }
}

> Note:
> This uses the Humanizer NuGet package to singularize words.
> https://github.com/Humanizr/Humanizer#singularize
```
