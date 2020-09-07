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
 
## Alternate Key
A composite key can be formed using this syntax
```C#
.HasAlternateKey(x => new { x.PropOneId, x.PropTwoId });
```
but it requires `PropOneId` and `PropTwoId` be actual properties on the class.
If these correpsond to navigation properties `PropOne` and `PropTwo`, then the syntax can be written as
```C#
.HasAlternateKey("PropOneId", "PropTwoId" });
```
which leverages the [Shadow Properties](https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties) on the class.
The EF Core naming convention for shadow properties can be found [here](https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties#foreign-key-shadow-properties).

See also:
 - https://stackoverflow.com/a/56519109/1926027
 - https://docs.microsoft.com/en-us/ef/core/modeling/keys?tabs=data-annotations#alternate-keys
