# Analysers

 - [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
 - [CSharpGuidelinesAnalyzer](https://csharpcodingguidelines.com/)
 - [SonarAnalyzer.CSharp](https://www.sonarsource.com/products/codeanalyzers/sonarcsharp.html)
 - [codecracker.CSharp](http://code-cracker.github.io/)
 - [Roslynator.Analyzers](https://github.com/JosefPihrt/Roslynator)
 - [Roslynator.CodeFixes](https://github.com/JosefPihrt/Roslynator)
 - [ErrorProne.NET](https://github.com/SergeyTeplyakov/ErrorProne.NET)
 - [ErrorProne.NET.Structs](https://github.com/SergeyTeplyakov/ErrorProne.NET)
 - [ErrorProne.NET.CoreAnalyzers](https://github.com/SergeyTeplyakov/ErrorProne.NET)
 - [ReflectionAnalyzers](https://github.com/DotNetAnalyzers/ReflectionAnalyzers)
 - [Microsoft.CodeAnalysis.FxCopAnalyzers](https://github.com/dotnet/roslyn-analyzers)

 - [SecurityCodeScan.VS2017](https://security-code-scan.github.io/) To install once Warning CS4032 has been fixed.
 - [RoslynClrHeapAllocationAnalyzer](https://github.com/Microsoft/RoslynClrHeapAllocationAnalyzer) To install once VS2019 is supported.

## Microsoft.CodeAnalysis.CSharp

Microsoft.CodeAnalysis appears to be used for developing code analysers, and may not be intended as analyser itself. Is it also included with Visual Studio somehow? According to https://github.com/dotnet/roslyn/issues/23489#issuecomment-348228319 this NuGet package should only be included when writing custom analysers, and not for analysing C# projects.

This ID for this analzyer's rules start with CS. To ignore these, such as CS1591, add the following to the ruleset file:

```xml
<Rules AnalyzerId="Microsoft.CodeAnalysis.CSharp" RuleNamespace="Microsoft.CodeAnalysis.CSharp">
  <Rule Id="CS1591" Action="None" />
</Rules>
```

Other rules that may need to be disabled are CS1573 and CS1712.

## Dependent Projects

In .Net Core, a project's analysers are automatically added to dependent projects if `PrivateAssets` is not set to `all`. See the following for more information:
https://docs.microsoft.com/en-us/visualstudio/code-quality/use-roslyn-analyzers?view=vs-2019#dependent-projects

## Style Cop

Setting up the stylecop.json file:

 - http://www.softwarepronto.com/2018/05/stylecop-setting-to-use-it-for-solution.html
 - http://www.softwarepronto.com/2018/05/visual-studio-file-linking-using.html
 - https://blog.submain.com/stylecop-detailed-guide/
 
Adding individual stylecop.json files to each project that has the StyleCopAnalyzers NuGet package would duplicate the settings provided by the stylecop.json. Instead, a single stylecop.json file can be added (say at the root-level of the solution), and then linked into each project that requires it. At present, it is unknown if the rules in this stylecop.json file can be extended or overridden by a project specific version while still avoid duplication (like how ruleset files operate with the Include element). If a specific stylecop.json file is needed, then creating an independent one is likely needed. More details here: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2085

StyleCop is looking at using the editorconfig file in the future:
 - https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2745

## .ruleset file

See AnalyzerRulesets.md

## .editorconfig file

According to https://github.com/dotnet/roslyn-analyzers/issues/1844#issuecomment-427422189 rulesets are the legacy file format, and there is a shift towards using .editorconfig configurations instead. Implementation of .editorconfig files appears to be a work in progress, with the .editorconfig files being the preferred modern way, and [ruleset files being the legacy configuration](https://github.com/dotnet/roslyn-analyzers/issues/1844#issuecomment-427428400).

The .editorconfig file can be generated using the current Visual Studio settings. Steps to do this can be found [here](https://docs.microsoft.com/en-us/visualstudio/ide/code-styles-and-code-cleanup?view=vs-2019#code-styles-in-editorconfig-files).

Further information:
 - https://github.com/dotnet/roslyn/issues/19618
 - https://docs.microsoft.com/en-us/visualstudio/ide/create-portable-custom-editor-options?view=vs-2019
 - https://andrewlock.net/generating-editorconfig-files-automatically-using-intellicode/
 
### See current settings

If ReSharper is in use, then the settings overridden by the .editorconfig file can be viewed by going to ReSharper > Windows > File Formatting Info.

# XML Documentation File

For StyleCop rule SA0001, the project needs to generate an XML Documentation file. Turning this on via the Build tab (Output section) of the Project's properties for .Net Core projects will use the absolute path for the Documentation file in the the .csproj. To fix this manually change the .csproj to have:

```xml
  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
  </PropertyGroup>
```

Rebuilding the solution will turn on the XML Documentation file checkbox in the Build tab, and the correct path will be used.

More Info: https://developers.de/2018/01/11/publish-xml-documentation-asp-net-core/
