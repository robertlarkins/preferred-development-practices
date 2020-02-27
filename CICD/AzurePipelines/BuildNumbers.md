# Build Numbers

## Setting BuildNumber

There are different ways for setting the build number. The simplest is to use the [`name` property](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/run-number?view=azure-devops&tabs=yaml) with a desired value.
However, the `$(Rev:r)` token *only* works when used with the `name` property, and its value cannot be changed using conditional statements.
This means that if the build number needs to be changed based on the branch, or some other condition, then a different approach is needed.

The buildnumber can be set for different cases by adding a *Run Inline Powershell* task. The following is based on this [example](https://stackoverflow.com/a/59366731/1926027):

```yaml
- task: PowerShell@2
  displayName: Set the BuildNumber
  inputs:
    targetType: 'inline'
    script: |
      $branch = $Env:Build_SourceBranchName
      $buildNumber = $Env:betaBuildNumber
      Write-Host "Current branch is $branch"
      if ($branch -eq "master")
      {
        $buildNumber = $Env:masterBuildNumber
      }
      Write-Host "##vso[build.updatebuildnumber]$buildNumber"
```

To replicate the `$(Rev:r)` token a [`counter`](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/expressions?view=azure-devops#functions) function needs to be added to the Variables section through the Pipeline editor (separate from the yaml file).
While the revision variable can be made to reset when the main version changes, its value would still be incremented for each build even when it doesn't get used.
This means that when the main version changes, the revision value increments but isn't displayed. Next time it is used for a prerelease build the first revision value will be missed.
It is possible for the revision value to be adjusted, but this required further code in the yaml file.
An alternative way is to use a DevOps extension to construct the build number.

## GitVersion

### [Configuration](https://gitversion.net/docs/configuration)

### [Variables](https://gitversion.net/docs/more-info/variables)

### BuildNumber

The main version number to use that GitVersion produces is `GitVersion.SemVer`.
This can be structured as Major.Minor.Patch-PreReleaseLabel.PreReleaseNumber.

### [Versioning Modes](https://gitversion.net/docs/reference/versioning-modes/)
This relates to the `mode` property in the gitversion.yml file.

#### [Continuous Delivery](https://gitversion.net/docs/reference/versioning-modes/continuous-delivery)
The default mode. This requires git tags to communicate to gitversion when a release is to be done, as this is an external manual decision.

#### [Continuous Deployment](https://gitversion.net/docs/reference/versioning-modes/continuous-deployment)
This approach increments the version on every commit.
