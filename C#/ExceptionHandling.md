# Exception Handling

## Inner exception with specific value

The `catch` for an exception can be made quite specific, including what values it contains.

For example when saving to a Postgres database using NpgSql, and a unique violation occurs,
we can catch precisely and only that exception by using the [`when` contextual keyword](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/when).

```C#
try
{
    await context.SaveChangesAsync(cancellationToken);
}
catch (DbUpdateException ex) when (
    ex.InnerException is PostgresException { SqlState: "23505" })
{
    // Do something with this Npgsql unique violation.
}
```
