# C-Sharp Solution Structure

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

which for a personal project for a browser calculator could be named:

```
Larkins.BrowserCalculator
```

**Some other examples**:

### Source Control Repository

A solution should be stored in a source control repository so that change can be tracked and managed. So create a repository (such as in git), put the solution here and commit any changes.

See SourceControlPractices.md.
 - This file will have repository naming
 - atomic commits
 - Git commit messages
 - pull requests
 - branching, rebase, and features

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
  LICENSE
  NuGet.Config
  README.md
  build.cmd
  build.sh
  global.json
  {solution}.sln
```

- `artifacts` - Build outputs go here. Doing a build.cmd/build.sh generates artifacts here (nupkgs, dlls, pdbs, etc.)
- `build` - Build customizations (custom msbuild files/psake/fake/albacore/etc) scripts. This also contains any infrastructure templates that build the cloud environment (see [build folder](#build-folder) section)
- `docs` - Documentation stuff, markdown files, help files etc.
- `lib` - Things that can **NEVER** exist in a nuget package
- `samples` (optional) - Sample projects
- `src` - Main projects (the product code)
- `tests` - Test projects
- `.editorconfig` - 
- `.gitignore` - 
- `.gitattributes` - 
- `Directory.Build.props` - 
- `LICENSE` - 
- `NuGet.Config` - 
- `build.cmd` - Bootstrap the build for windows
- `build.sh` - Bootstrap the build for *nix
- `global.json` (optional) - ASP.NET vNext only
- `{solution}.sln`

Having folder names lower case rather than Pascal casing makes them less prominent. They are a way of grouping projects, and lower case makes them less conspicuous. It also helps differentiate these folder from ones that are used inside projects as part of the NameSpacing.

> #### Note
> A number of the [dotnet core repositories](https://github.com/dotnet) now use [Arcade](https://github.com/dotnet/arcade) to provide a consistent build experience. These repositories now use an *eng* directory instead of a *build* directory. This may become the new standard from dotnet core 3. But at present this is a wait-and-see.
>
> **Further Reading**:
> - https://devblogs.microsoft.com/dotnet/the-evolving-infrastructure-of-net-core/
> - https://github.com/dotnet/arcade/blob/master/Documentation/StartHere.md

### Build Folder
The build folder contains all the necessary templates and scripts to build the application AND the infrastructure required to run the application.

    .
    ├─ ...
    ├─ build
    │  ├─ app                 # Resources required to build the application code
    │  └─ infra               # CloudFormation Templates that build the hosting environment
    └─ ...

 - `app` folder
 
    This folder contains everything needed to build the project. This may include:
    - Build Scripts
    - Pipelines
    - Scripts that run tests
 - `infra` folder
 
    This folder contains everything needed to build the hosting environment. This is likely going to be:
    - Network
    - Application Hosting

The idea is that anything in there are no dependencies between the `app` and `infra` folders. It should be possible to run the infrastructure templates without building the application and vice-versa.

### Solution Items

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

[1]: https://docs.microsoft.com/en-us/visualstudio/get-started/tutorial-projects-solutions?view=vs-2019#solutions-and-projects
[Microsoft namespace naming conventions]: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces
