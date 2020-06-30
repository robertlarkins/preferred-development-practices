# Git Tools

## SourceTree

https://community.atlassian.com/t5/Sourcetree-questions/Stashing-selected-files/qaq-p/137139  
https://confluence.atlassian.com/sourcetreekb/stash-a-file-with-sourcetree-785332122.html  
https://www.atlassian.com/blog/sourcetree/interactive-rebase-sourcetree
https://www.igorkromin.net/index.php/2018/10/28/how-to-force-sourcetree-to-abort-a-stuck-rebase/

### Installing SourceTree

Version 3.3.8 of SourceTree does not add a Start Menu icon, so this will need to be done manually.
Go to `%localappdata%\SourceTree` and right click SourceTree.exe and click Pin to Start.

### Staging Hunks

In SourceTree you can stages hunks, but if you click a line in the hunk, you can stage the individual line.

### Repo Author Details

https://community.atlassian.com/t5/Sourcetree-questions/SourceTree-commit-author-change-update/qaq-p/144926

### Clickable issue links in commit message

https://blog.sourcetreeapp.com/2012/02/06/jira-integration-other-external-project-links/

In the repo go Settings > Advanced > Commit text links > Add
For Azure DevOps set the Commit text link details to:  
*Replacement type*: Other  
*Regex pattern*: AB#(\d+)  
*Link to URL*: https://dev.azure.com/[organisation]/_workitems/edit/$1

For Azure DevOps, the following link can be used: 

### Update Embedded Git

### Line length indication
https://stackoverflow.com/questions/30414091/keep-commit-message-subject-under-50-characters-in-sourcetree

### Create Pull Request

SourceTree can start a pull request directly in GitHub by right clicking on the local feature branch and going *Create pull request...*.
Keep all the fields (Submit via remote, Local branch and Remote branch) as default and click *Create Pull Request On Web*. This will commit the feature branch to origin and open the pull request page in GitHub to request merging the feature into the branch that it was originally forked from.

Note: Changing *Remote branch* to develop commits the feature directly into develop, though this has only been tested while being a GitHub admin, branch protection rules should stop this occurring.

### Red Exclamation Mark

The red exclamation mark on the Remote button is there because the Remote account isn't set in the repo settings.
So go to Settings, edit the origin Remote and updated the Remote Account.
Unfortunately as at v3.3.8, SourceTree doesn't seem to save the account change. This appears to be a common issue (https://community.atlassian.com/t5/Sourcetree-questions/Sourcetree-is-showing-a-red-exclamation-mark-next-to-Remote/qaq-p/909571) so hopefully it will be fixed in a new version.

### Remove old branches from Remotes list
Git keeps a local record of remote branches, even if they have been removed from origin.
To clean these up from the Remotes > origin list in SourceTree, click Fetch, check *Prune tracking branches no longer present on remote(s)*, and click Ok.

### Clearing an installed version of SourceTree

If SourceTree has issues running properly, the first thing to try is upgrading to the latest version and seeing if that helps. If you download and install a new version, rather than updating, then ensure you are running the latest version, as sometimes the SourceTree shortcuts don't get updated, and older versions might be in Program Files, while newer versions are under AppData for the user.

If this doesn't work, then uninstall and reinstall SourceTree. Start this by going to Control Panel > Programs > Programs and Features and uninstalling SourceTree. To ensure it has fully been removed from your system ensure SourceTree has been deleted from the following folders (put into address bar):
 - `%programfiles%`
 - `%programfiles(x86)%`
 - `%localappdata%`
 - `%localappdata%\Atlassian`  
   There is likely two folders in here, *SourceTree* and *SourceTree.exe_Url_someuniquecode*

If any folders can't be deleted due to Git, then see if a System version of Git has been installed. If it has, then either kill the associated processes in Task Manager or uninstall Git, and then try again. It might be a good idea to do a pc restart.

If the desire is to use System Git, then either uninstall and reinstall, or in powershell run the command
```
git update-git-for-windows
```
to update to the latest version.

It may be worth also checking that any credentials are cleared out, in case these are causing the issue.
So go to `Control Panel\User Accounts\Credential Manager` click on Windows Credentials and remove any git or sourcetree related credentials.

Then reinstall SourceTree with the latest version.

Following this, if embedded Git is used, check that the latest version is used. Follow the other sections to personalise SourceTree.

If for any reason credentials aren't working, then try [downloading Git](https://git-scm.com/downloads) and installing it directly. Then switch SourceTree to use System Git rather than embedded.

### Adding credentials to SourceTree

If you are prompted, use WinCred as your Authentication manager. If it appears in the popup list twice, use the first occurrence (not sure if this makes any difference though).

Another option which may or may not work is to append your username before the repo path. So for example if your repo path is
https://github.com/myorg/myrepo.git
then change it to
https://mygithubusername@github.com/myorg/myrepo.git

Another way of doing authentication is to add a Remote account. This is done by clicking the + for a new tab > Remote > Add an account... and adding in the account you want to use.

## Visual Studio Team Explorer

Team Explorer in Visual Studio allows a quick way to access the command prompt for running git commands: https://docs.microsoft.com/en-us/vsts/git/command-prompt
