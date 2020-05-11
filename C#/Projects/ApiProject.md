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

## Analyzers

If analyzers are used, such as StyleCop then modify the code appropriately. For example adding XML documentation.

## .csproj

If the TargetFramework is specified in the Directory.Build.props then the TargetFramework in the csproj can be removed.

## launchSettings.json

Update the launchUrl to something appropriate.

## Nuget Packages

### Swashbuckle.AspNetCore

Used for OpenAPI documentation generation.

https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio

The Swagger Optoins can be put into their own private method and referencing like this:

``` c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddSwaggerGen(GenerateSwaggerOptions);
}

private void GenerateSwaggerOptions(SwaggerGenOptions options)
{
    var openApiInfo = new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    };

    options.SwaggerDoc("v1", openApiInfo);

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
}
```

### Autofac

Used for dependency injection

https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#asp-net-core-3-0-and-generic-hosting

The following NuGet packages are needed:
 - Autofac.Extensions.DependencyInjection  
   This is used for the AutofacServiceProviderFactory in the Program.cs class.
