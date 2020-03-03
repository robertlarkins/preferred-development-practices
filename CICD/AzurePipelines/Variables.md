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
