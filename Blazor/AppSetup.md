# Blazor App Setup

## Server App vs. WebAssembly App

### Server App


### WebAssembly App

Additional options:
 - ASP.NET Core hosted
 - Progressive Web Application

## Creating Blazor App in Visual Studio
1. Create a new project
   Search for _Blazor_, select Blazor App, and click Next
2. Configure your new project
   - Give it a Project name, this should following the same [naming convention](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces) as C#,
     e.g.: _Company.Product.Web_
   - Choose a location for this project, likely in an appropriately created git repo.
   - Based on the example project name above, the solution name should be _Company.Product_.
3. Create a new Blazor App
   Choose between a Blazor Server App or a WebAssembly App

> Note:  
> Currently Blazor WebAssembly apps will only run on Windows, not Linux:  
> https://docs.microsoft.com/en-us/aspnet/core/blazor/host-and-deploy/webassembly?view=aspnetcore-3.1#azure-app-service
