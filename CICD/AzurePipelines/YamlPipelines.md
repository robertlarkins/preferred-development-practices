# Yaml files in Azure Pipelines

## Jobs

### Deployment and Nondeployment

Nondeployment jobs automatically checkout the source code from the repository. 

https://docs.microsoft.com/en-us/azure/devops/pipelines/process/deployment-jobs?view=azure-devops



## Tasks

### [`checkout`](https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema?view=azure-devops&tabs=schema#checkout)
Source code is automatically checked out with nondeployment jobs. The `checkout` keyword is used to configure or suppress the checkout behaviour. This is useful if a pipeline has multiple jobs which rely on the same built code.
The first job can build the code and publish it as an artifact, while subsequent jobs can download and operate on the artifact.
In these subsequent jobs the code does not need to be checked out again.

### [`dotnet tool`](https://docs.microsoft.com/en-us/dotnet/core/tools/#tool-management-commands)

The `dotnet tool` cli command can be written in Azure DevOps using the `DotNetCoreCLI` task or a straight `script` tasks. The following are examples for both of these tasks using `update`.

Using the `DotNetCoreCLI` task:
```yaml
- task: DotNetCoreCLI@2
  inputs:
    command: custom
    custom: tool
    arguments: update -g <PACKAGE_ID>
  displayName: Update (or install) <PACKAGE_ID> to default global location
```

Using the `script` task:
```yaml
- script: dotnet tool update -g <PACKAGE_ID>
```

#### `install` vs `update`
The `install` command installs the tool if not already available, otherwise it will throw an error. Whereas `update` will install the tool if not available, if it is available it will update to the newest version, unless a version is specified (`dotnet tool update <PACKAGE_ID> [--version <VERSION>]`). If the latest version is installed the tool is reinstalled, so as to *repair* it (uninstall,reinstall) incase it has any issues.

> Note:
> Using the `update` command to install a package [requires dotnet core 3](https://github.com/dotnet/cli/pull/10205).
