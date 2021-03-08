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
