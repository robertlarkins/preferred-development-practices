# Solution Seed
Do the following to set up a new solution

## .editorconfig
Modify as necessary.

## .gitignore
Created using http://gitingnore.io using the angular, visualstudio, and visualstudiocode templates. Recreate to get latest changes.

## .gitattributes
Not included, add if necessary.

## Directory.Build.props

Update the following lines in Directory.Build.props:

Under `PropertyGroup` change the TargetFramework to latest .Net Core version
```
<TargetFramework>netcoreapp3.1</TargetFramework>
```

Under the StyleCop Analyzer section change the line
```
<PackageReference Include="StyleCop.Analyzers" Version="1.2.0-beta.66">
```
to reference the latest version of StyleCop.Analyzers.

## Directory.Build.targets
Not included, add if necessary.

## LICENSE
Not included, add if necessary.

## NuGet.config
Add needed NuGet feeds to the config. Official NuGet feed added.

## README.md
Delete the contents of this README.md and put relevant details into it.

## build.cmd
Not included, add if necessary.

## build.sh
Not included, add if necessary.

## global.json
Not included, add if necessary.

## global.ruleset
Includes available rulesets built into Visual Studio.
Disables the following rules for StyleCop.Analyzers:
 - SA1101: Prefix local calls with *this*
 - SA1413: Use trailing commas in multiline initializers
 - SA1633: File Must Have Header
Add or remove to these as necessary.

## stylecop.json
Use to customise the behaviour of stylecop. Currently specifies that *usings* go outside the namespace.

## Company.Product.sln
Rename to an appropriate .sln name.




