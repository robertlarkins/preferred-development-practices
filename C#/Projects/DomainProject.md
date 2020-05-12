# Domain Project

This is a Class Library project that holds all the domain objects used with the Application Layer.

## Create Project

Go to the *Solution Explorer > src > domain* folder, right click and go *Add > New Project...*

Find the *Class Library (.NET Core)* project template and click Next.

### Naming
Give the project a name along the lines of

> Company.Product.Domain

The Company.Product portion should match the solution name.
This name may be made more specific if there will be multiple domain projects. Maybe Company.Product.Domain.DomainArea?

### Location
Do not use the default location, as this will place the project at the root of the repository.
Instead select the src/domain directory.

## Analyzers
If analyzers are used, such as StyleCop then modify the code appropriately. For example adding XML documentation.

## .csproj
If the TargetFramework is specified in the Directory.Build.props then the TargetFramework in the csproj can be removed.



# Domain Unit Test Project
When creating the domain project, an associated test project should also be created.

## Create Project
Go to the *Solution Explorer > tests > domain* folder, right click and go *Add > New Project...*

Find the *xUnit Test Project (.NET Core)* project template and click Next.

### Naming
Give the project a name that matches the Domain project, so if the domain project is

> Company.Product.Domain

then the unit test project should be

> Company.Product.Domain.Tests.Unit

### Location
Do not use the default location, as this will place the project at the root of the repository.
Instead select the tests/domain directory.

## Analyzers
If analyzers are used, such as StyleCop then modify the code appropriately. For example adding XML documentation.

## .csproj
If the TargetFramework is specified in the Directory.Build.props then the TargetFramework in the csproj can be removed.

## NuGet Packages
This project type includes the following NuGet Packages
 - xunit
 - xunit.runner.visualstudio
 - coverlet.collector
 - Microsoft.NET.Test.Sdk
These packages will likely need to be updated, though upgrading xunit to 2.4.1 might best be avoided due to the number of dependencies installed.


The following packages should also be installed
 - FluentAssertions
 - FluentAssertions.Autofac
 - FluentAssertions.Analyzers
 - xunit.analyzers

