# NuGet Commands

See Also:
- https://docs.microsoft.com/nl-nl/nuget/reference/cli-reference/cli-ref-list


## Options

- `-source` The package source to search. The `-Source` option can be specified multiple time, once for each source to search.


## See what Nuget packages are in a feed

```powershell
nuget list -PreRelease -AllVersions -Source [NuGetSourceUrl]
```

E.G.: `nuget list -PreRelease -AllVersions -Source https://mynugetfeed.com/somepath/`


## See specific package in feed

If the versions of a single package are to be listed, then the package name can be provided

```powershell
nuget list [PackageName] -PreRelease -AllVersions -Source [NuGetSourceUrl]
```

E.G.: `nuget list My.Package.Name -PreRelease -AllVersions -Source https://mynugetfeed.com/somepath/`
