# Yaml files in Azure Pipelines

## Jobs

### Deployment and Nondeployment

Nondeployment jobs automatically checkout the source code from the repository. 

https://docs.microsoft.com/en-us/azure/devops/pipelines/process/deployment-jobs?view=azure-devops



## Tasks

### [`checkout`](https://docs.microsoft.com/en-us/azure/devops/pipelines/yaml-schema?view=azure-devops&tabs=schema#checkout)
Source code is automatically checked out with nondeployment jobs. The `checkout` keyword is used to configure or suppress this behaviour.
This is useful if a pipeline has multiple jobs which rely on the same built code.
The first job can build the code and publish it as an artifact, while subsequent jobs can download and operate on the artifact.
In this situation the code does not need to be checked out again.
