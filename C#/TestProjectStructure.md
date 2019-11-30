# Test project structure

Each *Code project* in a Solution will typically have an associated *Test project*. There are different ways that the tests in a test project can be structured.

The *Basic Unit Testing* and *Separated Unit Testing* approaches should only be used for utility code, code that doesn't contain business logic. An example of this is classes like `ValueObject` that are in the SeedWork folder. Further reading can be found at https://enterprisecraftsmanship.com/2019/08/22/you-naming-tests-wrong/, in particular the following:

> The only exception to this guideline is when you work on utility code. Such code doesn’t contain business logic – its behavior doesn’t go much beyond simple auxiliary functionality and thus doesn’t mean anything to the business people. It’s fine to use the SUT’s method names there.

## Test Project Naming

The name of the Test project should match the Code project name but with a *Tests.Type* identifier on the end. Where *Type* is the type of tests, such an *Unit* or *Integration*.

For example if a Code project is named:  
`Acme.Sales.Domain`  
then the associated unit test project will be named  
`Acme.Sales.Domain.Tests.Unit`
while the integration test project will be named
`Acme.Sales.Domain.Tests.Integration`.

This naming convention is suggested in *The Art of Unit Testing with examples in C#, Second Edition, by Roy Osherove, section 7.2*.

## Basic Unit Testing

### Test Project Structure

The basic structure is to have the Name Spaces in the test project match the code project so that the tests are found in the same respective location as the code they test.

For example:
```
Acme.Sales.Domain
├── Invoicing
│       Receipt.cs
│
└── Shipping
    ├── Customer
    │       Address.cs
    │       Name.cs
    │
    └── Cargo
            Route.cs
```
would have a test project like

```
Acme.Sales.Domain.Tests.Unit
├── Invoicing
│       ReceiptTests.cs
│
└── Shipping
    ├── Customer
    │       AddressTests.cs
    │       NameTests.cs
    │
    └── Cargo
            RouteTests.cs
```

### Test Class Naming

If a class is named `[ClassName].cs` then the associated test class will be named `[ClassName]Tests.cs`.

Futher detail can be found in *Art of Unit Testing*, Section 2.3.2.

### Test Method Naming

There are a variety of [naming conventions](https://dzone.com/articles/7-popular-unit-test-naming) that can be used for unit tests, with a commonly used approach being

```
[UnitOfWork]_[Scenario]_[ExpectedBehaviour]
```

 - `UnitOfWork`  
   The name of the unit being tests, which could be a method, or something more abstract that encompassees multiple methods or classes being tested.
 - `Scenario`  
   The conditions under which the unit is tested.
 - `ExpectedBehaviour`  
   What the unit is expected to occur when running the Unit of Work with this scenario.
   
Further detail can be found in *Art of Unit Testing*, Section 2.3.2.

Other links:
 - https://stackoverflow.com/questions/155436/unit-test-naming-best-practices
 - https://stackoverflow.com/questions/96297/what-are-some-popular-naming-conventions-for-unit-tests
 - http://agileinaflash.blogspot.com/2012/05/seven-steps-to-great-unit-test-names.html
 
### Test Method Structure

The method that forms the unit test usually follows the 3A approach, which is *Arrange*, *Act*, *Assert*.

1. *Arrange*  
   Arrange the objects needed for the test, such as creating and setting them so that they match the specified scenario.
2. *Act*  
   Act on the unit being tested.
3. *Assert*  
   Assert that the outcome matches the expected behaviour.

Further detail can be found in *Art of Unit Testing*, Section 2.4.

## Separated Unit Testing

An object being tested could have a number of methods and scenarios while still conforming to the Single Responsibility Principle. To improve the clarity around these tests an alternative approach is to split the test class into individual classes based on the units being tested. If different scenarios relate to the same unit of work they are placed in the same class.

### Test Project Structure

The project naming and structure is the same as that described in the **Basic Unit Testing** section, however instead of having `[ClassName]Tests.cs` for all the tests, a folder (also used in the namespace) is used to group the individual classes, and is given that name instead.

Taking the example test project structure from **Basic Unit Testing** and focussing on Customer would convert the structure from using test classes:

```
Acme.Sales.Domain.Tests.Unit
└── Shipping
    └── Customer
            AddressTests.cs
            NameTests.cs
```
to using folders:
```
Acme.Sales.Domain.Tests.Unit
└── Shipping
    └── Customer
        ├── AddressTests
        └── NameTests
```

the Tests.Unit class from above
For example if the


Having this in the name means its directly on an object, rather than on the parent folder, this is beneficial as Test Explorers generally display the whole path of a folder, but not for an object, meaning that the class that this group of tests relates to is directly specified. Having the first part match the folder makes it clearer where these files are located and that they are test files.
The second part of the test name is the Unit being tested. This unit could be the same in different classes, such as `Equals` or `ToString`, so having the group name first clearly specifies what object they relate to.

## Scenario based Testing

To write up: group tests based on scenarios rather than on individual units.

### Links
https://enterprisecraftsmanship.com/2019/08/22/you-naming-tests-wrong/  
https://jimmybogard.com/integration-testing-with-xunit/

