# Testing

## Testing of `internal` classes
Classes can have the modifier `internal` making them only visible within their assembly.
This means that external test projects cannot see these internal classes to be able to test them.

However, we can add the following piece of code to the .csproj of the project being tested so that it allows the project doing the testing (the test assembly)
to see its internal classes:

```xml
<ItemGroup>
  <AssemblyAttribute Include="System.Runtime.CompilerServices.InternalsVisibleTo">
    <_Parameter1>My.Project.Ding.The.Tests.Unit</_Parameter1>
  </AssemblyAttribute>
</ItemGroup>
```
