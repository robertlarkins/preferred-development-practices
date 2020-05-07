# Tips & Tricks

## Updating dotnet framework

When updating a project's target framework, the referenced nuget packages may need to be updated.
The nuget packages can be reinstalled solution wide by running the following command in the Package Manager Console:
```
Update-Package -reinstall
```

Some more details can be found here: https://ardalis.com/force-nuget-to-reinstall-packages-without-updating
