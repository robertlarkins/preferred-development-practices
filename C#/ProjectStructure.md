# C# Project Structure

## Directory.build.targets

https://www.strathweb.com/2018/07/solution-wide-nuget-package-version-handling-with-msbuild-15/

That overrules the version specified in the .csproj file. It will however have to be updated manually as updating NuGet packages through Visual Studio will only update the version in the .csproj file, which wont be the used version.

**Directory.build.targets is unreliable**
*This doesn't seem to work with Visual Studio at present, and will hopefully get better support in the future.*

## Directory.build.props

This provides base settings that get applied to all projects in the solution. An example of one is provided in C#/BaseFiles/Directory.Build.props.

Recommended settings (placed inside a `<ProjectGroup>`) include:

```xml
<!-- Turns on Nullable Reference Types, available in C#8 -->
<Nullable>enable</Nullable>

<!-- Warnings are treated as errors so solution will only compile once they are fixed -->
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>

<!-- Each project will generate a documentation file, which is necessary for some analyzers -->
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

### StyleCop Analyser

StyleCop can be applied to all projects in the solution by adding it into Directory.build.props.
The stylecop.json file provides a set of preferred rules and settings, though this can be overridden at the project level.

StyleCop settings placed within the `<Project>` tag:

```xml
<!-- StyleCop Analyzer -->
<ItemGroup>
  <PackageReference Include="StyleCop.Analyzers" Version="1.1.1-rc.114">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  </PackageReference>
  <AdditionalFiles Include="$(MSBuildThisFileDirectory)stylecop.json" Link="stylecop.json" />
</ItemGroup>
```

> Note:  
> The StyleCop.Analyzers Version number specified in the Directory.build.props file does not automatically update.
> Therefore if StyleCop.Analyzers is updated for a project, a 
> ```xml
> <PackageReference Update="StyleCop.Analyzers" Version="updated.version.no">
> ```
> will be added to the project's .csproj file.
> The project will use the updated version, but if this is removed from the .csproj, the version will fall back to that in Directory.build.props.

## Adding a new project to a solution


## Analysers to add

Link to analysers document


## Storage of local settings (Asp.Net)

Using Secret Manager
 - https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2&tabs=windows
 
## Storage of Configuration (Debug, Release, etc.) settings

 - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2
 - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2#file-configuration-provider
 - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-2.2
 - https://stackoverflow.com/questions/46364293/automatically-set-appsettings-json-for-dev-and-release-environments-in-asp-net-c
