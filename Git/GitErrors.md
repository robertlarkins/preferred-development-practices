# Git Errors

## `fatal: credential-cache unavailable; no unix socket support`
Follow the following to try and fix:
https://stackoverflow.com/questions/67951554/error-fatal-credential-cache-unavailable-no-unix-socket-support

Another option is to run either `git config --list` or `git config -l --show-origin` to see what credential helper is being used.
Alternatively the credential manager can just be reset:

```ps
git config --global --unset credential.helper
git config --global credential.helper manager
```
This comes from the _Set credential helper_ section of https://snede.net/git-does-not-remember-username-password/
