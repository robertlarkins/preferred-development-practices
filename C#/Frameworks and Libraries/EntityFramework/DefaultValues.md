# Default Values

https://docs.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=fluent-api

## Default Value
A default value can be configured for an entity's property, which specifies what value to use if not available.
```C#
builder.Property(p => p.Score)
    .HasDefaultValue(10);
```

See:
 - https://docs.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=fluent-api
 - https://www.learnentityframeworkcore.com/configuration/fluent-api/hasdefaultvalue-method


## Foreign Key Default Value
When adding a foreign key reference the `defaultValue` assigned in the Migration to the foreign key column is 0.
```C#
migrationBuilder.AddColumn<int>(
  name: "some_other_table_id",
  table: "my_table",
  nullable: false,
  defaultValue: 0);
```

At present for Entity Framework Core (v3.1.4) there appears to be no way of specifying the default foreign key reference in Code First / Fluent API.
It must be ammended in the generated Migration file. So once the Migration has been created, edit the default values as needed.

See:
 - https://stackoverflow.com/questions/31025967/code-first-migration-how-to-set-default-value-for-new-property
 - https://stackoverflow.com/questions/19554050/entity-framework-6-code-first-default-value/27920032
 - https://stackoverflow.com/questions/20199382/entityframework-code-first-fluentapi-defaultvalue-in-ef6-x
 - https://github.com/dotnet/efcore/issues/4403#issuecomment-183508924

## ValueObject Default Value
In the FluentAPI configuration a ValueObject can be given a default value to use if no value is provided:
```C#
builder.Property(p => p.StatusCode)
    .HasConversion(p => p.Value, p => StatusCode.Create(p).Value)
    .HasDefaultValue(StatusCode.Create("Unknown").Value);
```
This is useful for adding a column to an existing table and a specific default value is needed for the existing entries.
