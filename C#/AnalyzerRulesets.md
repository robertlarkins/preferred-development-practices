# Custom Analyzer Rulesets

## Solution level ruleset

The following includes instructions for having a child (global) ruleset with parent (local) ruleset. The global ruleset is the child as it is included in the local ruleset, which is why the local ruleset is deemed the parent.
 - https://docs.microsoft.com/en-us/visualstudio/code-quality/how-to-create-a-custom-rule-set?view=vs-2019
 - https://docs.microsoft.com/en-us/visualstudio/code-quality/using-rule-sets-to-group-code-analysis-rules?view=vs-2019

Add a ruleset file to *solution items* by right clicking *solution items* and going Add->New Item, select Code Analysis Rule Set and giving it the name &lt;solution name&gt;.ruleset

Double click the ruleset file to open it (if not already open). Click the wrench at the top with the tooltip *Edit ruleset properties in the Properties window* and give it a Name and a Description.


## Project level ruleset

Create a ruleset for a project, same as for the solution but at the project level.

Once a project ruleset file has been added, open the ruleset file and click the folder button (which has the tooltip *Add or remove child rule sets...*) to add a child ruleset file. Select the global ruleset file.


> **Note:**
> If the global ruleset file has a name with upper case letters, such as *My.Global.ruleset*, it gets included in the local ruleset file with lowercase letters and backslashes in the path:
> 
> ```xml
> <Include Path="..\..\..\my.global.ruleset" Action="Default">
> ```
> In Windows this is fine, but for other operating systems, such as Linux (including Linux containers, such as through Docker), the path has to have forward slashes and the letter casing much match the file name. Therefore the local ruleset file needs to be manually modified to match:
> ```xml
> <Include Path="../../../My.Global.ruleset" Action="Default">
> ```
> Windows will recognise the manually modified path as well.

To add the ruleset to project, it needs to (at present) be manually added to the project's csproj file.

```xml
<PropertyGroup>
  ...
  <CodeAnalysisRuleSet>custom.ruleset</CodeAnalysisRuleSet>
</PropertyGroup>
```

At present, ReSharper is not providing a quick action for ignoring a rule. So go to Dependencies->Analyzers for the project in Solution Explorer and select the rule to change.
This will add it to the local ruleset file. If this rule should be in the global ruleset, then manually move it to the global ruleset.
If the warning for the removed rule still occurs, then try unloading and reloading the project.

In the ruleset file, the `Action` field of `Include` determines how the rules should be treated for the parent ruleset. This is usually `Default`, but could be any of the other action values, such as `Error` or `Warning`.

**Example**
```xml
<?xml version="1.0" encoding="utf-8"?>
<RuleSet Name="Company.Product.Domain ruleset" Description="The analyzer rules customized specific for the Domain project" ToolsVersion="16.0">
  <Include Path="../../../global.ruleset" Action="Default" />
  <Rules AnalyzerId="StyleCop.Analyzers" RuleNamespace="StyleCop.Analyzers">
    <Rule Id="SA1413" Action="None" />
    <Rule Id="SA1600" Action="None" />
  </Rules>
</RuleSet>
```

The `RuleSet` start tag includes a `ToolsVersion` attribute. This attribute specifies the MSBuild Toolset version to use, and is linked to the major Visual Studio version number.
See https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-toolset-toolsversion?view=vs-2019 for more info.

 
## Suppress Warnings
 
Suppressing certain rules via the ruleset file does not seem to work. This more appears to be related to the compiler warnings such as CS1591.
Instead, they can be added to the project's Suppress warnings list (Right click your project > Properties > Build > Errors and warnings > Suppress warnings).
For example if you want to suppress rules CS1701, CS1702, and CS1591, then added them to the Suppress warnings textbox like as `1701;1702;1591`, this will added them to the .csproj.
Alternatively they can be added directly to the .csproj by going
```xml
<PropertyGroup>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

### `$(NoWarn)`
The `$(NoWarn)` MSBuild macro contains the error codes 1701 and 1702. So it can be used in place of putting in `1701;1702` into the `<NoWarn>` element.
 
> Note:
> A MSBuild macro in this context corresponds to a MSBuild property, `$(NoWarn)` is the macro, `NoWarn` is the property.
 
See:
 - https://github.com/dotnet/AspNetCore.Docs/issues/9708
 - https://github.com/dotnet/sdk/blob/2eb6c546931b5bcb92cd3128b93932a980553ea1/src/Tasks/Microsoft.NET.Build.Tasks/targets/Microsoft.NET.Sdk.CSharp.props#L16
 - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings


### CS1701 and CS1702
These two rules appear to be redundant now and are disabled by default. It appears they are a relic from early .Net Framework.

See:
 - https://github.com/dotnet/roslyn/issues/19640

 
### CS1591
CS1591 occurs when the DocumentationFile compiler option is enabled (Right click your project > Properties > Build > Output > XML documentation file).
But if `<GenerateDocumentationFile>true</GenerateDocumentationFile>` is used in Directory.Build.props then the CS1591 warning will occur.
One scenarios for disabling CS1591 warnings would be unit test projects, as often the xml docs do not provide anything additional over the test name.
 
See:
 - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs1591
 
