# API Project

The following API project setup if for an API that supports presentation or client apps (front-facing or UI projects),
such as web or mobile applications.

## Create Project

Under Solution Explorer (Visual Studio) go to the src/presentation folder, right click and go

> Add > New Project...

Find the *ASP.NET Core Web Application* project template and click Next to go through each of the following sections.

### Naming

Give the project a name along the lines of

> Company.Product.Api

The *Company.Product* portion should match the solution name.

This documentation for naming may be made more specific later, maybe Company.Product.Api.Presentation?

### Location

Do not use the default location, as this will place the project at the root of the repository.
Instead select the src/presentation directory.

### Template

Ensure .NET Core is selected, along with a version of ASP.NET Core, at this time .NET Core 3.1 is the latest.
Select the *API* template and click Create.

This will likely create an example Controller and model (WeatherForecastController.cs and WeatherForecast.cs), which can be deleted.
