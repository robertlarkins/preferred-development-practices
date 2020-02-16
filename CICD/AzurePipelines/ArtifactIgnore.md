# .artifactignore
The .artifactignore file specifies what items in the directory to be published should be included or excluded.

## Where to place
The .artifactignore is stored in the git repository, and would ideally be placed at the root of the directory tree that is going to be published.
If publishing the entire repository, then this would be at the root of your repository.

When publishing a directory that is only construcuted at build time, for example `$(System.DefaultWorkingDirectory)/src/My.Project/bin/Release/`,
then for the .artifactignore to be applied to the publish task, it must be placed in `$(System.DefaultWorkingDirectory)/src/My.Project/bin/Release/`.
Because .artifactignore cannot be stored here, it must be copied to this directory as part of the pipeline and after the build has occurred.

In this situation an ideal location for storing the .artifactignore is under the repository's `build` directory.
It maybe useful to add a pipeline specific directory under `build` to make it obvious as to what publish task the .artifactignore gets applied to.
Using specific directories is necessary if multiple .artifactignore files are needed as publish tasks only looks for .artifactignore.
This would require copying each one to the directories to be publish after build.


## Syntax
The .artifactignore file uses the same syntax as .gitignore. See the [gitignore documentation](https://git-scm.com/docs/gitignore) for more details of this syntax. 

## .git
The Azure DevOps Publish task ignores the .git directory by default, as specified [here](https://docs.microsoft.com/en-us/azure/devops/artifacts/reference/artifactignore?view=azure-devops).

## Further Reading

https://www.jfe.cloud/control-pipeline-artifacts-with-artifactignore-file/
