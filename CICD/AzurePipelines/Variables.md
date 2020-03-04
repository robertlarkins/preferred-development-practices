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

### [Logging commands](https://docs.microsoft.com/en-us/azure/devops/pipelines/scripts/logging-commands?view=azure-devops&tabs=bash)
Logging commands are how tasks and scripts communicated with the agent (Windows, Linux and macOS), and can be called from both Bash and PowerShell scripts. The available commands can be found [here](https://github.com/microsoft/azure-pipelines-tasks/blob/master/docs/authoring/commands.md).

### Date
Storing the current date and time in a variable can be done using a script task. The following example shows how to get the current year for New Zealand and using it to store a copyright in a variable using the [PowerShell task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/powershell?view=azure-devops):

```yaml
- task: PowerShell@2
  displayName: Set propertyCopyright variable
  inputs:
    targetType: inline
    script: |
      $timeZoneId = 'New Zealand Standard Time'
      $currentDateTime = [System.TimeZoneInfo]::ConvertTimeBySystemTimeZoneId([DateTime]::Now, $timeZoneId)
      $copyright = "Copyright $($currentDateTime.year)"
      Write-Host "##vso[task.setvariable variable=propertyCopyright]$copyright"
```

The equivalent can be done using a [Bash task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/utility/bash?view=azure-devops) aswell. This example uses the bash task shortcut syntax:

```yaml
- bash: |
    currentYear=$(TZ=Pacific/Auckland date +"%Y")
    copyright="Copyright $currentYear"
    echo "##vso[task.setvariable variable=propertyCopyright]$copyright"
  displayName: Set propertyCopyright variable
```
