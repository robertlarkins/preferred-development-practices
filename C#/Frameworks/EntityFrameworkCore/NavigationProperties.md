# Navigation Properties

## Eager Loading

When forming a query you can used the `Include` method to specify which related data will be included with the query results.

https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager

### Split Queries

If eagerly loading a large collection then using Split queries will split the original query into multiple SQL queries.

 - https://docs.microsoft.com/en-us/ef/core/querying/single-split-queries
 - https://docs.microsoft.com/en-us/ef/core/querying/single-split-queries#characteristics-of-split-queries


## Lazy Loading

By adding the `.UseLazyLoadingProcies()` method to the Entity Framework configuration, related data will be retrieved at the time the code accesses it.

 - https://docs.microsoft.com/en-us/ef/core/querying/related-data/lazy


## Explicit Loading

https://docs.microsoft.com/en-us/ef/core/querying/related-data/explicit
