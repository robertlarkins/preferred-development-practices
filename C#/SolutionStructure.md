# C# Solution Structure

## Creating a New Solution

In Visual Studio 2019 a [blank solution][1] (one without any projects) can be created by clicking _Create a new project_,
then searching for _blank solution_ in the _Search for templates_ entry box.
Now follow these next steps.

### Solution Name

The solution will have a high level name which groups the projects together. In Visual Studio 2019 this is entered in *Project name*.
The solution name should follow the [Microsoft naming convention][Microsoft namespace naming conventions] of

```
<Company>.(<Product>|<Technology>)
```

which for a personal project for C# Katas could be named:

```
Larkins.CSharpKatas
```

**Other examples**:

 - `Larkins.BrowserCalculator`


### Source Control Repository

A solution should be stored in a source control repository allowing changes to be tracked and managed. So create a repository (such as in git), put the solution here and commit any changes.

See SourceControlPractices.md.

## Physical Folder Structure

The base structure of a solution is provided by its physical folder layout on the disk. David Fowler provides a [good layout for dotnet folders](https://gist.github.com/davidfowl/ed7564297c61fe9ab814) that has seen wide adoption (eg: [Mvc](https://github.com/aspnet/Mvc)). The following layout is based on his recommended .NET project structure, but updated for .Net Core 3 or newer:

```
$/
  artifacts/
  build/
  docs/
  lib/
  samples/
  src/
  tests/
  .editorconfig
  .gitignore
  .gitattributes
  Directory.Build.props
  Directory.Build.targets
  LICENSE
  NuGet.config
  README.md
  build.cmd
  build.sh
  global.json
  {solution}.sln
```

- `artifacts/` - Build outputs go here. Doing a build.cmd/build.sh generates artifacts here (nupkgs, dlls, pdbs, etc.)
- `build/` - Build customizations (custom msbuild files/psake/fake/albacore/etc) scripts. This also contains any infrastructure templates that build the cloud environment (see [build folder](#build-folder) section)
- `docs/` - Documentation stuff, markdown files, help files etc.
- `lib/` - Things that can **NEVER** exist in a nuget package
- `samples/` (optional) - Sample projects
- `src/` - Main projects (the product code)
- `tests/` - Test projects
- [`.editorconfig`][2] - Enforces consistent coding style at the solution level.
- [`.gitignore`](https://git-scm.com/docs/gitignore) - Specifies files to be ignored and intentionally untracked in the repository.  
  The site [gitignore.io](http://gitignore.io/) creates useful .gitignore files for different environments.
- `.gitattributes` - 
- [`Directory.Build.props`][4] - 
- `Directory.Build.targets` - 
- `LICENSE` - 
- [`NuGet.config`][5] (optional) - Specifies NuGet behavior at the solution level.
- `build.cmd` - Bootstrap the build for windows
- `build.sh` - Bootstrap the build for *nix
- [`global.json`][3] (optional) - ASP.NET vNext only
- `{solution}.sln`

Having folder names lower case rather than Pascal casing makes them less prominent. They are a way of grouping projects, and lower case makes them less conspicuous. It also helps differentiate these folder from ones that are used inside projects as part of the NameSpacing.

Custom versions of some of these files can be found in C#/CustomFiles.

> #### Note
> A number of the [dotnet core repositories](https://github.com/dotnet) now use [Arcade](https://github.com/dotnet/arcade) to provide a consistent build experience. These repositories now use an *eng* directory instead of a *build* directory. This may become the new standard from dotnet core 3. But at present this is a wait-and-see.
>
> **Further Reading**:
> - https://devblogs.microsoft.com/dotnet/the-evolving-infrastructure-of-net-core/
> - https://github.com/dotnet/arcade/blob/master/Documentation/StartHere.md

### `build` Folder

The build folder contains all the necessary templates and scripts to build the application AND the infrastructure required to run the application.

    .
    ├─ ...
    ├─ build
    │  ├─ app                 # Resources required to build the application code
    │  └─ infra               # CloudFormation Templates that build the hosting environment
    └─ ...

 - `app` folder  
   Contains everything needed to build the project. This may include:
   - Build Scripts
   - Pipelines
   - Scripts that run tests
 - `infra` folder  
   Contains everything needed to build the hosting environment. This is likely going to be:
   - Network
   - Application Hosting

No dependencies between the `app` and `infra` folders should exist, allowing the application or infrastructure templates to be run independently from each other.

### `src` Folder

The `src` folder contains the main projects that comprise the software solution. As a solution has different aspects to it, subfolders are used to further group projects together:

    .
    ├─ ...
    ├─ src
    │  ├─ common                 # 
    │  ├─ core                   # 
    │  ├─ infrastructure         # 
    │  └─ presentation           # 
    └─ ...

 - `common`  
   Projects containing classes that are generic and common for other projects are placed here. This will likely have a single *Common* project.

 - `core`  
   Contains projects that relate to the Domain entities or the Application business rules.

 - `infrastructure`  
   Any projects that implement or access anything external, such as persistent storage, are placed here.
 
 - `presentation`
   Projects related to presentation, including Api projects, go here.

### `tests` Folder

The `tests` folder contains the projects used to automatically test the main projects. The subfolders will match the `src` folder:

    .
    ├─ ...
    ├─ tests
    │  ├─ common                 # 
    │  ├─ core                   # 
    │  ├─ infrastructure         # 
    │  └─ presentation           # 
    └─ ...

*This needs to be researched further as to whether integration testing can encompasses multiple projects, or if those types of tests have a different name. Test projects that span multiple projects should live under the tests folder.*

https://www.atlassian.com/continuous-delivery/software-testing/types-of-software-testing
http://www.continuousagile.com/unblock/test_types.html
https://www.softwaretestinghelp.com/system-vs-end-to-end-testing/

## Solution Folders

Solution Folders in Visual Studio's Solution Explorer are virtual folders used as an organisation aid, they do not create a physical folder on disk. However, the solution folder structure should match the physical folder structure on disk. Therefore, the following solution folders should be added:

 - `samples\` (optional)
 - `src\`
 - `tests\`

**Further Reading**
 - http://www.blackwasp.co.uk/VSSolutionFolders.aspx
 - https://blogs.msdn.microsoft.com/zainnab/2010/03/27/using-solution-folders/
 - http://mikehadlow.blogspot.com/2007/07/how-to-structure-visual-studio.html

## Solution Items

When adding items directly to the Solution, Visual Studio creates a *Solution Items* solution directory to hold them as they cannot be put directly under the Solution heading. Visual Studio does not create a corresponding physical directory. This is the only directory that should remain virtual as items in this directory occur in the top-level physical folder for the solution. Examples of items common to this folder are:

 - `.editorconfig`
 - `Directory.Build.props`
 - `README.md`
 - `LICENSE`
 - `{solution}.ruleset`
 - `stylecop.json`

Creating a physical version of this folder is pointless, as the items would be put in here should remain at the top level of the directory. This maintains consistency with the recommended Physical Folder layout, which is necessary as other tools (Github) rely on files being at the top-level directory for the solution (eg: README.md, LICENSE).

To keep the naming convetions the same as other directories, make it lower case, so that it is *solution items*.

Note: Simply renaming the directory to lower case letters will not change it. Rename it to something else, then back to *solution items*.

A *project items* directory is not created in each project as the items can be put directly under the Project heading.

### `.editorconfig`

The `.editorconfig` file enforces a consistent coding style for the code base. The specified settings take precedence over the IDE's default settings. `.editorconfig` files have a hierarchical relationship, with the settings of ones deeper in the file hierarchy overruling those above it.

A recommended solution level `.editorconfig` file can be found [here](BaseFiles/Solution/.editorconfig).

**Further Reading**
 - https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options?view=vs-2019


[1]: https://docs.microsoft.com/en-us/visualstudio/get-started/tutorial-projects-solutions?view=vs-2019#solutions-and-projects
[2]: https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options?view=vs-2019
[3]: https://docs.microsoft.com/en-us/dotnet/core/tools/global-json
[4]: https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2019
[5]: https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file
[Microsoft namespace naming conventions]: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces
