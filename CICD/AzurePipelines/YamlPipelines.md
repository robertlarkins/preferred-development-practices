# Yaml files in Azure Pipelines

## Stages

## Jobs

### Deployment and Nondeployment

Nondeployment jobs automatically checkout the source code from the repository. 

https://docs.microsoft.com/en-us/azure/devops/pipelines/process/deployment-jobs?view=azure-devops

## [Triggers](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/triggers?view=azure-devops&tabs=yaml)

### [Paths](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/triggers?view=azure-devops&tabs=yaml#paths)
Paths can use [wildcards](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/triggers?view=azure-devops&tabs=yaml#wildcards) in their syntax, but a `*` at the end of a directory name, such as `- src/*` is the same as specifying the directory by itself `- src`.

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

## [Artifacts](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/artifacts-overview)
Artifacts are the files produced by a pipeline that are then published in some manner. This includes packages that are constructed and published to package repositories (NuGet, npm, Maven, etc.), build artifacts and pipeline artifacts.

### [Publish NuGet package](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/nuget)

### [Build Artifact](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/build-artifacts)

Going forward build artifacts will be replaced with pipeline artifacts: https://github.com/MicrosoftDocs/vsts-docs/issues/2341#issuecomment-439483135

### [Pipeline Artifact](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/pipeline-artifacts)

#### Publish

Publishing an artifact provides a way of sharing files between jobs or pipelines. The following example shows how to publish the *Default Working Directory* as a pipeline artifact. If a root level variable is provided for the artifact name, such as `buildArtifactName`:

```yaml
variables:
  buildArtifactName: 'my_artifact_name'
```

then each job can reference this variable, either to publish or download. The following shows the publish task:

```yaml
- publish: $(System.DefaultWorkingDirectory)
  artifact: $(buildArtifactName)
  displayName: Publish artifact $(buildArtifactName)
```

> Note: The `publish` keyword is a shortcut for the [`PublishPipelineArtifact`](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/publish-pipeline-artifact) task.

Control over what files should be included or excluded from the artifact can be specified using a [.artifactignore](ArtifactIgnore.md) file.

#### [Download](https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/pipeline-artifacts?view=azure-devops&tabs=yaml#downloading-artifacts)
A job that needs to consume an artifact can download it. The following example uses the `download` keyword and would be used in conjunction with the [Publish](#publish) example:

```yaml
- download: current
  artifact: $(buildArtifactName)
  displayName: 'Retrieve build artifact'
```

which downloads the artifact to the [`$(Pipeline.Workspace)/`](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/variables?view=azure-devops&tabs=yaml#pipeline-variables) directory.

> Note: The [`download`](https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema?view=azure-devops&tabs=schema#download) keyword is a shortcut for the [`DownloadPipelineArtifact`](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/download-pipeline-artifact) task, which allows for further customisation, such as what directory to download the artifact to.

## [Variables](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/variables)

Variables provide a means of storing reusable information in the pipeline.

### Pipeline Variables

#### Inline

These are variables that are included in the yaml file.

#### Interface

These are variables that are stored in Azure Pipelines variable interface, and can be referenced from the yaml file. These are useful for variables that need to be kept secret or persist across separate pipeline runs.

### [Variable Groups](https://docs.microsoft.com/en-us/azure/devops/pipelines/library/variable-groups)

### [Predefined Variables](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/variables)

### [Expressions](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/expressions)

#### Compile Time

#### Run Time

## Testing

### [`VisualStudioTestPlatformInstaller`](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/tool/vstest-platform-tool-installer?view=azure-devops)

This task is used to provide the Visual Studio test platform rather than needing Visual Studio to be installed on the agent.
Either Visual Studio test platform or Visual Studio are necessary on the agent to allow the `vstest` task to be run.

> Note:  
> The task is only supported by Windows agents and cannot be used on other platforms.

Here's an example of the task for using the latestStable provided from the official NuGet feed:

```yaml
- task: VisualStudioTestPlatformInstaller@1
  inputs:
    packageFeedSelector: 'nugetOrg'
    versionSelector: 'latestStable'
```

### [`VSTest`](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/test/vstest?view=azure-devops)

VSTest is used to run unit and functional tests.
This task is only available on Windows agents and requires either Visual Studio or Visual Studio Test Platform to be available on the agent.
