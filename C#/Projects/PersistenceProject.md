# Persistence Project

## Connection String

When developing locally, one place for storing a connection string (or strings) is in the appsettings.json
file under the `ConnectionStrings` property:
```json
{
  ConnectionStrings: {
    MyDatabaseConnection: "User ID=postgres;Password=\"password\";Host=localhost;Port=5432;Database=my_db;",
    SomeOtherConnection: "some connection details"
  }
}
```

This can then be stored in appsettings.Development.json, and kept in local rather than being checked into git.

See:
- https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-strings#aspnet-core
- https://stackoverflow.com/a/45845041
