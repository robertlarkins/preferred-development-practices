# Tips & Tricks

## Updating dotnet framework

When updating a project's target framework, the referenced nuget packages may need to be updated.
The nuget packages can be reinstalled solution wide by running the following command in the Package Manager Console:
```
Update-Package -reinstall
```

Some more details can be found here: https://ardalis.com/force-nuget-to-reinstall-packages-without-updating

## Renaming a Project (.Net Core)

The first step to renaming a project in a solution is to right-click the project name and click *Rename*, or click the name and press F2.
Give it a new name, ensuring that it maintains the solution name at the start. This will update the project name in the .sln, but not the path in the .sln, nor the folder name on disk.

To update the folder name where the project resides right-click on the solution name and click *Open Folder in File Explorer*. Go up a directory level and rename the project folder so it has the new name.

To update the path in the .sln open the .sln in a text editor and find any instances of the old project path and replace with the updated path. If Visual Studio is open, it will bring up a File Modification Detected warning, click Reload on this warning.

All projects referencing the one with the name change will need to be updated. These projects will typically be indicated by a yellow triangle on the Dependencies section. Remove the old reference (right-click project reference then Remove) then readd it by right-clicking 

> Dependencies > Add Reference...

select the new project name and click OK.
