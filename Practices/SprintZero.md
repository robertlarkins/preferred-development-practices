# Sprint Zero

A lot of the sections here will be generic, but this will be done with respect to GitHub and Azure DevOps.

The things that should be setup for a new project before development begins.

## Git Setup - GitHub

Create the repositories in GitHub.

### Repo Naming

Use kebab casing to name the repo, combining both company and product. This would then be something like *company-product*.

### Repositories
Depending on the nature of the project, there may need to be more than one repo, particularly if automatic versioning is done (GitVersion), i.e.: the version number of the mobile app shouldn't change if only the website or backend changes, and vice versa.
In this instance the mobile app and backend will each have their own repo. 

Append *-mobile* onto the mobile app repo name.

### Pull Request Setup

All commits have to be part of a feature, automatically built, tests run, and code reviewed before it can be merged into the develop branch.

### Branches

Master, Develop, Feature branches

### Versioning

GitVersion

### Code Owners
https://help.github.com/en/github/creating-cloning-and-archiving-repositories/about-code-owners

## Create Solution

See C# documents

## Azure DevOps Setup

### Azure Boards

#### GitHub connections

To link GitHub to Azure Boards go Project Setting > Boards > GitHub connections and Connect a GitHub account.
This will take you through the steps to connect the specific GitHub repo(s) to this project's Azure Boards.

#### Modify Kanban Board

The columns that can go in here are
 - Ready for Dev
 - Development
 - Testing
 - UAT
 - Completed


## CI/CD Pipelines

## Logging

## Definition of Ready, Definition of Done, Team Charter

## Feature Flags
Ideally to be able to turn features on and off directly in production without needing to push new builds.

 - https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core
 - https://featureflags.io/dotnet-feature-flags/

