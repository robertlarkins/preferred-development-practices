# Yaml Code for Testing
The following shows how to add testing to a yaml pipeline.
The expectation is that the code is .NET Core and is designed to work on any system (Windows, OSX, Linux).

## Code Coverage
Before code coverage can be done in the following manner, the NuGet package [*coverlet.collector*](coverletNuGet) needs to be installed
on each test project.
If the test project was created using the Visual Studio xUnit template then *coverlet.collector* is already included.

Code coverage of the solution is done as part of running the tests. The following `DotNetCoreCLI` test task collected code coverage for
any project that is in a *Tests.Unit* directory under *tests*.

```yaml
- task: DotNetCoreCLI@2
  displayName: 'Run unit tests'
  inputs:
    command: test
    projects: 'tests/**/*.Tests.Unit/*.csproj'
    arguments: --configuration $(buildConfiguration) --collect:"XPlat Code Coverage"
```

At present this approach generates a separate report for each project with each report directory having a guid for its name.
For example:
> /home/vsts/work/_temp/2db7b59d-f33d-4c1d-aa8f-8d7dae48b0bd/coverage.cobertura.xml

which is of the structure
> $(Agent.TempDirectory)/[guid]/coverage.cobertura.xml

There is disucussion of how coverlet might combine these reports, but it doesn't do it yet.
 - https://github.com/tonerdo/coverlet/issues/357

Combining reports together can be done in a separate task using the [*Report Generator*](reportGenerator) DevOps extension.
A simples yaml task for this is:
```yaml
- task: reportgenerator@4
  inputs:
    reports: '$(Agent.TempDirectory)/*/coverage.cobertura.xml'
    targetdir: 'combinedreport'
    reporttypes: 'Cobertura'
```
which finds each report, combines them and puts them into a *combinedreport* directory. When a directory is specified like this
it is just placed in `$(Pipeline.Workspace)`, so `targetdir: 'combinedreport'` is `targetdir: '$(Pipeline.Workspace)/combinedreport'`.

Publishing code coverage results is done using the [`PublishCodeCoverageResults`](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/test/publish-code-coverage-results?view=azure-devops) task.
The yaml for this task is:
```yaml
- task: PublishCodeCoverageResults@1
  displayName: 'Publish code coverage'
  inputs:
    codeCoverageTool: cobertura
    summaryFileLocation: 'combinedreport/Cobertura.xml'
```

### coverlet.runsettings
Coverlet can have a specific settings file. This should be called `coverlet.runsettings`.

https://github.com/coverlet-coverage/coverlet/issues/889
https://docs.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file  
https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/VSTestIntegration.md

### Running Coverlet Locally

#### Command Line
The usage for running Coverlet from the command line is found [here](https://github.com/coverlet-coverage/coverlet#usage).

#### *Run Coverlet Report* Visual Studio Extension


### Further Reading
 - https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/VSTestIntegration.md

## Quality Gates
https://marketplace.visualstudio.com/items?itemName=mspremier.BuildQualityChecks

The Azure DevOps extension *Build Quality Checks* provides quality gates for code coverage. The task *Settings* provide good info links for explaining what each options provides.

[coverletNuGet]: https://www.nuget.org/packages/coverlet.collector/
[reportGenerator]: https://marketplace.visualstudio.com/items?itemName=Palmmedia.reportgenerator

## Mutation Testing
Mutation testing can be performed on C# solutions using [stryker-net](https://github.com/stryker-mutator/stryker-net).
For dotnet core 3.0+, stryker is installed at the project level: https://github.com/stryker-mutator/stryker-net#project-install

Details about local tools: https://docs.microsoft.com/en-us/dotnet/core/tools/local-tools-how-to-use

If when running `dotnet tool install dotnet-stryker` it gives a 401 (Unauthorized) error, try running it with the `--ignore-failed-sources` flag, `dotnet tool install dotnet-stryker --ignore-failed-sources`.

Once added to each desired project the mutation testing can be run against the solution: https://stryker-mutator.io/blog/2019-04-05/announcing-dotnet-framework-support#start-using-stryker-net

To add this into Azure Pipelines go to https://stryker-mutator.io/blog/2020-05-15/azure-pipelines-integration

https://rajbos.github.io/blog/2019/09/03/Running-dotnet-tools-in-azure-devops

