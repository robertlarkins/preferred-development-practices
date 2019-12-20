# Tasks

`install` vs `update` for use with the `dotnet tool` command. `install` installs the tool if not already available, otherwise it will throw an error. `update` will install the tool if not available, if it is available it will update to the newest version, unless a version is specified (`dotnet tool update <PACKAGE_ID> [--version <VERSION>]`). If the latest version is installed the tool is reinstalled, so as to *repair* it (uninstall,reinstall) incase it has any issues.

> Note:
> This doesn't appear to work in yaml at present. There is a message in one of the dotnet/cli issues that this should be working though?!

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
This task is only available on Windows agents and required either Visual Studio or Visual Studio Test Platform to be available on the agent.
