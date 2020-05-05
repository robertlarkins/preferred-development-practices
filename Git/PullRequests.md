# Pull Requests

This is the current approach, but still very much a work in progress.
It will evolve as good practices become more ingrained.


## GitHub Branches

To setup pull requests in GitHub go to the Settings tab for a repo.

### Default branch

Change the default branch from *master* to *develop*, as this is the branch that pull requests will be merged into.

### Branch protection rule

This specifies the rules for how commits are merged into the branch(es) that match the *Branch name pattern*.

Enable the following options:
 - Require pull request reviews before merging  
   - Required approving reviews  
     This will vary based on team size, but can be set to 1 initially.
   - Dismiss stale pull request approvals when new commits are pushed
   - Require review from Code Owners *(Optional)*
     This would be useful for specifying who should review which code. It would use the CodeOwners file.
 - Require status checks to pass before merging
   - Require branch to be up to date before merging  
     A *Status Check* must be selected

Review all other options to determine if they need to be selected.


## Resources

There are lots of articles on how Pull Requests should be done, here are a few of the first results on google:

 - https://github.community/t5/Support-Protips/Best-practices-for-pull-requests/ba-p/4104
 - https://www.atlassian.com/blog/git/written-unwritten-guide-pull-requests
 - https://buttercms.com/blog/5-things-your-team-should-do-to-make-pull-requests-less-painful
 - https://medium.com/osedea/the-perfect-code-review-process-845e6ba5c31
 - https://www.braintreepayments.com/blog/effective-pull-requests-a-guide/

 - https://www.vinta.com.br/blog/2018/pull-requests-merging-good-practices-your-project-part-1/
 - https://www.vinta.com.br/blog/2018/pull-requests-merging-good-practices-your-project-part-2/
 - https://www.vinta.com.br/blog/2018/pull-requests-merging-good-practices-your-project-part-3/

 - https://github.blog/2015-01-21-how-to-write-the-perfect-pull-request/
 - https://www.kenneth-truyers.net/2018/11/01/best-practices-good-pr/
 - https://holgerfrohloff.de/code-quality/best-practices-on-doing-pull-requests/
 - https://blog.pragmaticengineer.com/pull-request-or-diff-best-practices/
