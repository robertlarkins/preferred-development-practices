# Business Logic Constraints

While the title of this page could be better, the idea is that business logic and data constraints can be applied and different levels in the code.

The first is at the Web or Mobile layer. This gives immediate feedback to the user around what is permitted.
The second is at the backend, either as part of query or command validation, or as business logic in the domain.
The third is database constraints, where the database enforces what data and relationships it allows to be stored.


## Example - Database race condition on the backend processing
This example occurred when a request to add results to the database starts with getting the data from the DB, processing the new additions, adding them into EF context and then saving them back to the database. As these results should only occur in the DB once, the race condition allows them to go into the database again. This is because the same request could be sent a second time before the first request has finished processing (and saved to the database). The second request gets the data from the database before the first request has saved, so the second request does not know that the data is about to be duplicated.

One fix is to break the request into smaller parts so that processing time is shorter, and it becomes more difficult for race conditions to occur.  
Another is to put constraints onto the database table so that the provided data is restricted to only occurring once in the table. This option will allow the request to finish first to be saved, but the second request will throw an exception when it tries to save. Handling the exception is needed, such as discarding the second request and returning something like a HTTP 409.
