# C# Project Structure

## Directory.build.targets

https://www.strathweb.com/2018/07/solution-wide-nuget-package-version-handling-with-msbuild-15/

That overrules the version specified in the .csproj file. It will however have to be updated manually as updating NuGet packages through Visual Studio will only update the version in the .csproj file, which wont be the used version.

**Directory.build.targets is unreliable**
*This doesn't seem to work with Visual Studio at present, and will hopefully get better support in the future.*  
Link somewhat explaining .targets usage: https://github.com/MicrosoftDocs/visualstudio-docs/issues/2774

## Directory.Build.props

Directory.Build.props provides base settings that are automatically imported and applied to all projects in the solution. An example of one is provided in C#/BaseFiles/Directory.Build.props.

Recommended settings (placed inside a `<ProjectGroup>`) include:

```xml
<!-- Turns on Nullable Reference Types, available in C#8 -->
<Nullable>enable</Nullable>

<!-- Warnings are treated as errors so solution will only compile once they are fixed -->
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>

<!-- Each project will generate a documentation file, which is necessary for some analyzers -->
<GenerateDocumentationFile>true</GenerateDocumentationFile>
```

A Directory.Build.props file placed at the root-level of a solution allows for a place for elements common to all project .csproj files to be stored. This is useful if an analyser is used on every project, such as including a stylecop.json file. Or Assembly information such as company name.

 - https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019
 - http://code.fitness/post/2018/03/directory-build-props.html
 - https://stackoverflow.com/questions/42138418/equivalent-to-assemblyinfo-in-dotnet-core-csproj/42143079#42143079
 - https://tpodolak.com/blog/2018/04/03/solution-wide-project-properties-directory-build-props/
 - https://thomaslevesque.com/2017/09/18/common-msbuild-properties-and-items-with-directory-build-props/
 - https://cezarypiatek.github.io/post/non-nullable-references-in-dotnet-core/

## Example of Directory.Build.props

```xml
<Project>
  <PropertyGroup>
    <TreatWarningsAsErrors>True</TreatWarningsAsErrors>
    <CodeAnalysisRuleSet>$(MSBuildThisFileDirectory)Root.Level.ruleset</CodeAnalysisRuleSet>
  </PropertyGroup>
  
  <!-- StyleCop Analyzer -->
  <ItemGroup>
    <PackageReference Include="StyleCop.Analyzers" Version="1.1.1-rc.114">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <AdditionalFiles Include="$(MSBuildThisFileDirectory)stylecop.json" Link="stylecop.json" />
  </ItemGroup>
</Project>
```

The Element `<CodeAnalysisRuleSet>` allows a default ruleset file to be specified. If a ruleset file is specified in the .csproj file, it will override the ruleset file in Directory.Build.props. It seems that the .csproj file has higher precendence than Directory.Build.props. More documentation on build or include order would be useful to know exactly what occurs.

If for any reason there needs to be a check to stop an already specified ruleset from being overridden by another ruleset file, then add the following `Condition` to `<CodeAnalysisRuleSet>` to check that a ruleset file has not already been included as part of the MSBuild pipeline:

```xml
<CodeAnalysisRuleSet Condition="'$(CodeAnalysisRuleSet)' == ''">$(MSBuildThisFileDirectory)Root.Level.ruleset</CodeAnalysisRuleSet>
```

This example came from: https://stackoverflow.com/questions/34919517/check-if-propertygroup-item-is-set-to-a-value-in-csproj/34919766#34919766

### Variables

 - `$(MSBuildThisFileDirectory)` variable refers to the directory containing the current MSBuild file.
 - `$(MSBuildProjectDirectory)` variable refers to the directory containing the project being built.

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
> The project will use the version specified in the .csproj, but if this is removed from the .csproj, the version will fall back to that in Directory.build.props.

## Adding a new project to a solution

The requirements for a projects is dependent upon its purpose. The following are steps common to all projects.

### The Type of Project

The type of project being created determines where it should reside within the directory structure.
The first is whether it is a main project (product source code), or a test project. A main project is defined futher split into:

 - common
 
 - core
 
 - infrastructure
 
 - presentation

* Todo: add a description for each of these and examples of what types of projects go into each. *

### Naming the project




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
