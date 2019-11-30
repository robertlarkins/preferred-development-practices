# Preferred Development Practices
Preferred practices for how software should be developed and maintained.

## Documents to Add or Extend

C#/SolutionStructure.md
 - editorconfig file
 
C#/TestProjectStructure.md

C#/SrcProjectStructure.md

C#/Analysers.md

Improve and maybe relocate Directory.Build.props that is in the C# Project Structure directory.

C#/DevelopmentTools.md
 - Visual studio extensions

C#/ExamplesArchitectureRepos.md

C#/CustomFiles/SeedWork  
C#/CustomFiles/.editorconfig
C#/CustomFiles/LICENSE
C#/CustomFiles/Directory.Build.props
C#/CustomFiles/stylecop.json

TypeScript/

Angular/

SourceControl/RepositoryConstruction.md
 - This file will have repository naming
 - branching, rebase, and code features

SourceControl/GitPractices.md
 - Atomic Commits
 - Git Commit messages
 - .gitignore
 - .gitattributes
 
SourceControl/PullRequests.md

DevelopmentPractices/Naming.md

DevelopmentPractices/CodeReviews.md


# Things to add in (Taken from SolutionStructure)

## Open Source Software (OSS) Projects to use as a basis

 - [EntityFrameworkCore](https://github.com/aspnet/EntityFrameworkCore)
 - [Mvc](https://github.com/aspnet/Mvc)
 - [AspNetCore](https://github.com/aspnet/AspNetCore)
 - [machinelearning](https://github.com/dotnet/machinelearning)

## Naming Conventions

### Namespaces

### Test Projects

If you have a project named _Company.AwesomeProduct.NovelFeature_ then the associated test project should be named _Company.AwesomeProduct.NovelFeature.Tests_.

## Naming Conventions

 - [Microsoft namespace naming conventions](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces)
 - [Microsoft names of type members](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-type-members)
 - [Microsoft Naming Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)

### Namespace

# The following needs their own homes
A set of recommended practices for how a .Net repository is set up, its structure and how it is maintained.

## Onion Architecture

https://medium.com/@stephanhoekstra/clean-architecture-in-net-8eed6c224c50

### Logging
What layer or layers should logging live on, should it only be the business logic layer. Or should it be on other layers as well?

## Naming

### Property Names

[Microsoft Property Design](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/property)

### Field Names
Fields in a class should not be public.

[Microsoft Field Design](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/field)

### Local Variables

### Parameters

[Microsoft Parameter Design](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/property)
[Microsoft Naming Parameters](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-parameters)

Should constant values be passed into methods as parameters? or should they be accessed from within the method?
They should be accessed from within, as this constant value is an implementation detail, and the expectation is that it wont change (such as Pi, or days in a week).
Additionally, if for some reason the constant changes and the method implementation remains the same, then the associated unit tests should fail, indicating what areas are effected by the change. Then where applicable the tests or the implementation should be changed accordingly. Ideally the test is testing some appropriate business rule with expected inputs and output, therefore, if the constant changes, but other internals in the method are also changed (implementation), the method still operates as expected and the tests pass.

## Enums

Provide a description of what Enums should be used for, and what they should not be used for.

https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types

where enums could be stored in a project. Here are the possibilities: 
 
1 - Nested in a class: only use if the enum is not public ( https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/nested-types )

2 - Top Level Type in class (outside of class but in same namespace in file): use only when the enum is directly related and only used with that class.

3 - A file holding a number of enums: group all or related enums together, though this is frowned upon ( https://softwareengineering.stackexchange.com/questions/178733/is-it-a-bad-practice-to-include-all-the-enums-in-one-file-and-use-it-in-multiple )

4 - A single file per enum: Usual recommendation, results in more files, but potentially easier to search and find. Can see its history in source control. Also more suitable if wanting to convert to an Enumeration class. ( https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types - additional articles at the bottom of this link. )
 
Some more discussion here:
https://stackoverflow.com/questions/2282976/should-enums-in-c-sharp-have-their-own-file
 
 - https://codecraft.co/2012/10/29/how-enums-spread-disease-and-how-to-cure-it/
 - https://www.planetgeek.ch/2009/07/01/enums-are-evil/
 - https://ardalis.com/enum-alternatives-in-c
 - https://ardalis.com/listing-strongly-typed-enum-options-in-c
 - http://www.drdobbs.com/windows/fixing-the-enum-in-c/240004191



## Class Members

### When to use Protected Accessibility

https://stackoverflow.com/questions/3182653/are-protected-members-fields-really-that-bad
https://ceylon-lang.org/documentation/faq/language-design/#no_protected_modifier
https://stackoverflow.com/questions/76194/should-protected-attributes-always-be-banned

### Fields

Fields should never be public, as if the class implements an interface fields cannot be defined on the interface, and it allows more control over how the variable is accessed.

Fields are implementation details, and implementation details should not be exposed. So fields should only every be declared with a private accessor (except in rare, exceptional cases, such as link from Jon Skeets page).

https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/fields the example says that public fields are generally not recommended.
https://softwareengineering.stackexchange.com/questions/161303/is-it-bad-practice-to-use-public-fields

http://csharpindepth.com/Articles/Chapter8/PropertiesMatter.aspx
https://www.reddit.com/r/csharp/comments/7oypaa/c_interface_private_field_declaration/
https://softwareengineering.stackexchange.com/questions/312425/why-have-private-fields-isnt-protected-enough/312429
https://stackoverflow.com/a/3182671
https://stackoverflow.com/questions/379041/what-is-the-best-practice-for-using-public-fields

## Projects

The architecture of the projects within the solution

The names for these projects based on their role (follow the onion model)


## Method structure

### XML Documentation Comments
 - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments
 - https://stackoverflow.com/questions/7535584/rules-guidelines-for-documenting-c-sharp-code
 - https://blog.rsuter.com/best-practices-for-writing-xml-documentation-phrases-in-c/

### Modifying parameters
Return a value rather than modifying the parameter by reference.
https://softwareengineering.stackexchange.com/questions/245767/is-modifying-an-incoming-parameter-an-antipattern

https://stackoverflow.com/a/556103/1926027



https://softwareengineering.stackexchange.com/questions/232350/does-designing-a-method-that-changes-the-arguments-if-was-object-values-a-goo

#### Avoid ref and out
https://stackoverflow.com/questions/3539252/when-is-using-the-c-sharp-ref-keyword-ever-a-good-idea

There are times when ref and out are valid, but this is rare. One such example for out is the TryParse method construction.

### Local Functions
Local functions should be placed at the bottom of a method. While not explicitely stated in the MSDN documentation, their [examples](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions) all places the local functions at the bottom of the method.

## Commits

### Atomic Commits

https://seesparkbox.com/foundry/atomic_commits_with_git

https://spin.atomicobject.com/2015/11/11/all-the-commits/

https://robots.thoughtbot.com/5-useful-tips-for-a-better-commit-message

### Commit Message

https://stackoverflow.com/questions/2290016/git-commit-messages-50-72-formatting?noredirect=1&lq=1

https://stackoverflow.com/questions/30414091/keep-commit-message-subject-under-50-characters-in-sourcetree

https://chris.beams.io/posts/git-commit/

http://who-t.blogspot.com/2009/12/on-commit-messages.html

### Consistent identity on repository from different locations




## Git Branching

Using Gitflow with develop and master branches, and when branching should occur (for features), and when to merge back in.

## Git Tags

When to use these

## Analyzers
Common and useful analyzers to use with C# projects.

 - StyleCop.Analyzers
 - CSharpGuidelinesAnalyzer
 - xunit.analyzers
 - FluentAssertions.Analyzers


### Ruleset file structure

## Testing

Shared contexts in xunit tests: https://xunit.github.io/docs/shared-context.html

### Test Project Structure
https://ardalis.com/unit-test-naming-convention
https://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx/

### Test naming conventions

```CSharp
[Fact]
public void UnitBeingTested_StateUnderTest_ExpectedBehaviour()
{
    // Arrange


    // Act


    // Assert

}
```

### Unit vs Integration testing
https://softwareengineering.stackexchange.com/questions/356236/definition-of-brittle-unit-tests
http://blog.stevensanderson.com/2009/08/24/writing-great-unit-tests-best-and-worst-practises/


## README.md

## .gitignore

When creating a new respository on Github it will offer to generate a .gitignore file. This is the easiest way to generate one.
Alternatively, the following website can be used to generate .gitignore files: [GitIgnore.io](https://gitignore.io)




[Microsoft namespace naming conventions]: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces

