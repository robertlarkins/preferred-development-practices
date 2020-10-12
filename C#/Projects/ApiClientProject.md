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
called from the command line using the NSwag.MSBuild NuGet package to generate the API models.
By making it a console project then it can be run when necessary for updating the models.

 - https://github.com/RicoSuter/NSwag/wiki/NSwag.MSBuild
 - https://github.com/RicoSuter/NSwag/wiki/CommandLine
 - https://github.com/RicoSuter/NSwag/wiki/Assembly-loading#net-core
 - https://github.com/RicoSuter/NSwag/wiki/NSwag-Configuration-Document

The easiest way to generate the nswag.json file is to use NSwagStudio. Specify the desired options and go File > Save.

Some examples of what the output file can be named is MyApiModels or AcmeApiAccessModels or AcmeClientModels. Anything that conveys that this is an automatically generated file for accessing the API.

To run the NSwag build to generate new API models go into Package Manager Console and run `dotnet build`. Using the addition to the .csproj file, the client class will be rebuilt on each build.

To incorporate authentication into the NSwag Client code, the HttpClient passed in via the constructor needs to facilitate the authentication, this isn't through NSwag: https://github.com/RicoSuter/NSwag/issues/1312

The NSwag client code should be stored in the repo, rather than being created at deployment time. This means that the client code used in the deployment can be seen and debugged without trying to extract what might be used within the live environment. 

NSwag is setup to build the client code using NSwag.MSBuild, which is called from the csproj file at compile time. See https://github.com/RicoSuter/NSwag/wiki/NSwag.MSBuild .

There is a MSBuild condition on the NSwag .csproj target so that the NSwag client code is only built if a custom MSBuild property `GenerateNSwagClientCode` is used.
This was added to stop DevOps trying to build a new Client, which might result in different Client code being used in the containers than what is in the repository.
The addition of this property is based on https://blog.sanderaernouts.com/autogenerate-csharp-api-client-with-nswag .

In a DevOps pipeline, a custom MSBuild property can be added by adding '-p:GenerateNSwagClientCode=False' to the arguments section in the dotnet core build task.
While the `GenerateNSwagClientCode` property is being provided for every project, if a project does not use it, it will be ignored.
The `dotnet test` task also needs this property specified as `test` seems to rebuild all projects.

# Integration Testing

To create an integration test project for the ApiClientProject start by selecting the *xUnit Test Project (.Net Core)* project template.

It is undecided yet how to run the integration tests as xUnit runs its tests in parrallel and is better designed for unit testing.

## Naming
> Company.Product.*[ResourceName]*.Tests.Integration

## Location
Place in the tests/infrastructure folder.
