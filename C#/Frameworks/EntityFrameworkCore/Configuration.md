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

See also:
 - https://www.learnentityframeworkcore.com/configuration/one-to-one-relationship-configuration
 - https://www.entityframeworktutorial.net/efcore/configure-one-to-one-relationship-using-fluent-api-in-ef-core.aspx
 
