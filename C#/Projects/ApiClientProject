# API Client Project
An API client project is a project that consumes an API.

## Create Project
Under Solution Explorer (Visual Studio) go to the src/infrastructure folder, right click and go  

> Add > New Project...

Find the *Class Library (.NET Core)* project template and click Next.

### Naming
A project that consumes an API is often referred to as a client. Therefore it is worthwhile giving it a name along those lines, such as

> Company.Product.*[ResourceName]*ApiClient

where *ResourceName* is the name of the resource or service that whose API is being accessed.

### Location
Do not use the default location for the project location, as this will place it at the root of the repository.
Instead select the src/infrastructure directory.

## .csproj
If the TargetFramework is specified in the Directory.Build.props then
```
<PropertyGroup>
  <TargetFramework>netcoreapp3.1</TargetFramework>
</PropertyGroup>
```
can be removed from the .csproj.

## NuGet Packages

### NSwag.MSBuild
As NSwag will be used to generate the API model, the NSwag.MSBuild NuGet package is used as it can be 
can be called from the command line using the NSwag.MSBuild NuGet package to generate the API models, .
By making it a console project then it can be run when necessary for updating the models.

https://github.com/RicoSuter/NSwag/wiki/NSwag.MSBuild
https://github.com/RicoSuter/NSwag/wiki/CommandLine



### NSwag.CodeGeneration.CSharp
This package generates the models from the specified API. To configure it see the documentation [here](https://github.com/RicoSuter/NSwag/wiki/CSharpClientGenerator).


