# Running a GitHub action

## To Azure
In an App Service Deployment you can have it create the GitHub actions .yml by going through the setup deployment process.
This does have a problem with the generated secret that can be found in the GitHub repo's secret settings.
To fix this requires regenerating the PublishSetting (by going to the desired App Service's Overview and clicking Get publish profile) and changing the MSDeploy publishUrl to `publishUrl="[my-app-name].scm.azurewebsites.net:443"`. This is discussed here: 
https://github.com/Azure/webapps-deploy/issues/28#issuecomment-660698494

See also:
 - https://github.com/Azure/actions-workflow-samples/blob/master/assets/create-secrets-for-GitHub-workflows.md

## Manual deployment
GitHub actions can be manually triggered by changing or adding `workflow_dispatch` as the `on` type.

See:
 - https://levelup.gitconnected.com/how-to-manually-trigger-a-github-actions-workflow-4712542f1960
 - https://dev.to/github/github-actions-manual-triggers-with-workflowdispatch-3di1
