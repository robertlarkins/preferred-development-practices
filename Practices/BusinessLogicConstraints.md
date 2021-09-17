# Business Logic Constraints

While the title of this page could be better, the idea is that business logic and data constraints can be applied and different levels in the code.

The first is at the Web or Mobile layer. This gives immediate feedback to the user around what is permitted.
The second is at the backend, either as part of query of command validation, or business logic in the domain.
The third is database constraints, where the database enforces what data and relationships it allows to be stored.


## Example - Database race condition on the backend processing
This example occurred when database 
