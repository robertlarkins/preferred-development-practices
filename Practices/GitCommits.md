# Git Commit Practices

## Atomic Commits

When using any source control management (SCM) systems, whenever you check in code, you should be asking yourself 'if somebody else needs to look at this commit, will they be able to quickly understand what has been done and why'?

How do I know when my changes would constitute a commit?



The advantage of atomic commits are:
 - more detailed comments on code changes
 - easier to find the cause of bugs, especially when using tools such as git bisect
 - feature migrations
 - rollbacks
 - others in the articles below

### Examples


### Links

 - https://seesparkbox.com/foundry/atomic_commits_with_git
 - https://www.freshconsulting.com/atomic-commits/
 - https://spin.atomicobject.com/2015/11/11/all-the-commits/
 - https://brainlessdeveloper.com/2018/02/19/git-best-practices-atomic-commits/
 - https://hashrocket.com/blog/posts/easier-atomic-commits
 - http://www.pauline-vos.nl/atomic-commits/
 - https://curiousprogrammer.io/blog/why-i-create-atomic-commits-in-git
 - https://coderwall.com/p/jmqp0a/why-and-how-i-craft-atomic-commits-in-git
 - https://www.codewithjason.com/atomic-commits-testing/
 - https://en.wikipedia.org/wiki/Atomic_commit

## Commit comments

A great article on how we should approach git comments and why: https://chris.beams.io/posts/git-commit/

An older article that gives a bit more detail on what the message body should contain: http://who-t.blogspot.co.nz/2009/12/on-commit-messages.html . I would suggest following the recommendation of 50 characters or less for the commit summary ( https://chris.beams.io/posts/git-commit/ ).

Commit messages should talk about WHAT changed, and WHY. Not HOW – how is the diff, and you don’t need to repeat it.

Tip 4 in here: https://robots.thoughtbot.com/5-useful-tips-for-a-better-commit-message gives some examples for what the commit message should cover.

### 5WsH&R

A commit should clearly and concisely convey details of the source code change so that it answers the following questions in a manner that makes it easy for a future reader to understand the intent of this commit. This provides context for what this commit accomplishes.

 - Who
 
   Who was the author of this commit. When performing a Git commit to a repository, you need to have some form of Id. This Id is attached to the commit. Therefore, Git automatically captures 'who' for each commit.
   
 - When
 
   When was this commit performed. Git adds the Date and Time that each change to the repository was committed.
 
 - Where
 
   Where in the repository were the changes in this commit done. This is provided by Git when it identifies the files and line numbers where the changes were done.
 
 - How
 
   The changes in the source code tell you how the code has been changed. This is anything that was added, deleted or changed. The code should be self explanitory. If it is too complex that it needs explaining, then either the code needs refactoring, or more rarely, there should be comments in the source code.
 
 - What
 
   What was done, 
 
 - Why
 
   The commit comment should explain why this change was done
   This provides the reasons for why the changes were made.
 
 - Repercussions
 
   The repercussions that the changes caused by this commit could have that others should be aware of.

 - Others
   - Example if appropriate
   - Identifier to associated story or bug

## Pull Requests

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

## Tools

### SourceTree

### Visual Studio Team Explorer

Team Explorer in Visual Studio allows a quick way to access the command prompt for running git commands: https://docs.microsoft.com/en-us/vsts/git/command-prompt
