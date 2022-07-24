# Testing

## Testing of `internal` classes
Classes can have the modifier `internal` making them only visible within their assembly.
This means that external test projects cannot see these internal classes to be able to test them.

However, we can add the following piece of code to the .csproj of the project being tested so that it allows the project doing the testing (the test assembly)
to see its internal classes:

```xml
<ItemGroup>
  <InternalsVisibleTo Include="My.Project.Doing.The.Tests.Unit" />
</ItemGroup>
```

If the above does not work, then try this instead:

```xml
<ItemGroup>
  <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
    <_Parameter1>My.Project.Doing.The.Tests.Unit</_Parameter1>
  </AssemblyAttribute>
</ItemGroup>
```

See:
 - https://docs.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#internalsvisibleto
 - https://www.meziantou.net/declaring-internalsvisibleto-in-the-csproj.htm
