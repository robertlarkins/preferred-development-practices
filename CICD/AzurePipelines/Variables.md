# [Variables](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/variables)

Variables provide a means of storing reusable information in the pipeline.

## Pipeline Variables

### [Run or Build Number](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/run-number?view=azure-devops&tabs=yaml)
A run or build number in a yaml pipeline can be set using the `name` property. There are [tokens](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/run-number?view=azure-devops&tabs=yaml#tokens) such as `$(Date)` that are specific to `name`, and won't be expanded if use elsewhere in the yaml file.

See [Build Numbers](BuildNumbers.md) for more details about how to approach build numbers.

### Inline

These are variables that are included in the yaml file.

### Interface

These are variables that are stored in Azure Pipelines variable interface, and can be referenced from the yaml file. These are useful for variables that need to be kept secret or persist across separate pipeline runs.

## [Variable Groups](https://docs.microsoft.com/en-us/azure/devops/pipelines/library/variable-groups)

## [Predefined Variables](https://docs.microsoft.com/en-us/azure/devops/pipelines/build/variables)

## [Expressions](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/expressions)

## [Variable from Script](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/variables?view=azure-devops&tabs=yaml%2Cbatch#set-variables-in-scripts)

A new pipeline variable can be set from a script tasks such as `PowerShell@2`. This does not require a job scoped variable to be defined, but does mean that the variable is only available to downstream steps within the same job. 

```yaml
- task: PowerShell@2
  displayName: Set propertyCopyright variable
  inputs:
    targetType: inline
    script: |
      $copyright = 'Copyright 2020'
      Write-Host "##vso[task.setvariable variable=propertyCopyright]$copyright"
```
In this example a variable `$(propertyCopyright)` is set and can be used in subsequent steps.

Other logging commands, like `task.setvariable` can be found [here](https://github.com/microsoft/azure-pipelines-tasks/blob/master/docs/authoring/commands.md).

### Date
