# Tasks

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
