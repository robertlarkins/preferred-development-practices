# Secrets Manager

https://medium.com/datadigest/user-secrets-in-asp-net-core-with-jetbrains-rider-26c381177391


## Rider Setup

Go `File > Settings > Plugins` and install the *.NET Core User Secrets* Rider plugin.

To access the secrets for a project right-click on the project > Tools > Open Project User Secrets

The secrets.json file is found in `%APPDATA%\Roaming\Microsoft\UserSecrets\<project-user-secrets-id-guid>\secrets.json`


- https://plugins.jetbrains.com/plugin/10183--net-core-user-secrets
- https://www.infoworld.com/article/3576292/how-to-work-with-user-secrets-in-asp-net-core.html
- https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows

## How to use

How to add a key:
Go to the directory in terminal and enter `dotnet user-secrets set "key" "value"`


## How to inject the key

