# Dapper Readme

## Dapper Setup

Dependency Injection setup.

Whenever a database is called using Dapper, a database connection needs to be established.

If a repository class is being used, then we need

Options:
- Inject a Connection Factory or Provider into the Repository:
  - https://stackoverflow.com/questions/42937942/dapper-with-net-core-injected-sqlconnection-lifetime-scope
  - https://gunnarpeipman.com/aspnet-core-dapper/
- Inject IConfiguration and create a SqlConnection from the connection string
  - https://codewithmukesh.com/blog/dapper-in-aspnet-core/
  - https://code-maze.com/using-dapper-with-asp-net-core-web-api/
  - https://www.c-sharpcorner.com/article/implement-readwrite-operations-using-cqrs-and-dapper-in-asp-net-core-detailed/
- Inject a IDbConnection into the Repository?
  - This approach is less likely to work if there different databases,
    as startup wouldn't be able to specify (at least that I'm aware) which connection string to used for the SqlConnection.
    Whereas specific providers could be created for each database.
