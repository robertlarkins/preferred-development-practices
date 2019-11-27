# Naming Practices

## Solution

## Project

## Namespace

## Classes

### Two classes with the same name, but differ due to generics
https://stackoverflow.com/questions/3108189/how-to-name-c-sharp-source-files-for-generic-classes

At present the easiest solution is to put them all into the same file.

## Method

## Local variables

## Properties

## Fields

# Types

## Bool

This applies to all variables and members of type `bool`.

Boolean variable names should be read like a question that has a true or false answer.
Using the following to prefix the variable name helps form the question structure.

Bool class members and variables should be named with an affirmative phrase (CanSeek instead of CantSeek). It is also recommened to prefix with the following:

- Preferred:
  - _is_, this is the recommended prefix
  - _can_, future tense
  - _has_, past tense, eg: `HasBeen...`;
  
- Acceptable, though need to be checked
  - _was_ [link](https://softwareengineering.stackexchange.com/questions/232213/should-i-always-use-is-as-prefix-for-boolean-variables)
    - was is the past tense of _is_, but should it be used?
  
  - _should_ [link](https://stackoverflow.com/questions/3874350/naming-conventions-for-java-methods-that-return-booleanno-question-mark) or [link2](https://petroware.no/javastyle.html#Specific)
  - _any_ [link](https://stackoverflow.com/questions/2691463/is-or-are-to-prefix-boolean-values)
  - _all_ [link](https://stackoverflow.com/questions/2691463/is-or-are-to-prefix-boolean-values)

The following prefixes might seem appropriate, but should still be avoided:
 - _are_, this is because it can be ambiguous. [link](https://stackoverflow.com/questions/2691463/is-or-are-to-prefix-boolean-values)
 - _does_, this can be switched to use is [link](https://stackoverflow.com/questions/5887450/does-a-method-name-starting-with-does-look-good). Ie:
   - doesUserExist to isExistingUser
   - doesNameHaveValue to isNameEntered.
 

If a method returns a bool then it should have a verb name in the form of a yes/no question.
https://stackoverflow.com/questions/1370840/naming-conventions-what-to-name-a-method-that-returns-a-boolean

 - https://news.ycombinator.com/item?id=9352847
 - https://stackoverflow.com/questions/1227998/naming-conventions-what-to-name-a-boolean-variable
 - https://softwareengineering.stackexchange.com/questions/232213/should-i-always-use-is-as-prefix-for-boolean-variables
 - https://stackoverflow.com/questions/3874350/naming-conventions-for-java-methods-that-return-booleanno-question-mark
 - https://www.cs.bgu.ac.il/~majeek/presentations/JavaProgrammingStyle%20Guidelines.html
 - https://stackoverflow.com/questions/2691463/is-or-are-to-prefix-boolean-values
 - https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-type-members
   - DO name Boolean properties with an affirmative phrase (CanSeek instead of CantSeek). Optionally, you can also prefix Boolean properties with "Is," "Can," or "Has," but only where it adds value.
 - https://softwareengineering.stackexchange.com/questions/348649/naming-of-bool-methods-is-vs-can-vs
 - https://javarevisited.blogspot.com/2014/10/10-java-best-practices-to-name-variables-methods-classes-packages.html Point 12
 - https://csharpcodingguidelines.com/naming-guidelines/ AV1715
 - https://seanmonstar.com/post/880164961/naming-booleans
 - http://wiki.c2.com/?GoodVariableNames - Has and Have, Am and Is for differences between noun and verb naming.
 
Bool naming should not be negative: https://wiki.eclipse.org/Recommenders/CodingConventions#Negated_boolean_variable_names_must_be_avoided.
 
Also look at names used in other opensource libraries, such as
 - [roslyn](https://github.com/dotnet/roslyn)
 - [EntityFrameworkCore](https://github.com/aspnet/EntityFrameworkCore)
 - [corefx](https://github.com/dotnet/corefx)
 - [NewtonSoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

## Null Objects

This is for naming objects that represent a null, empty, or default object, and used with the null object pattern.

https://stackoverflow.com/questions/8545994/choosing-names-for-null-or-missing-objects
In the article https://www.codeproject.com/Articles/1042674/NULL-Object-Design-Pattern#Proposaltochangename it proposes that names shouldn't used the word `Null` as this can be misinterpreted with the Null data type. The alternative for the proper name would be: *Default behaviour object design pattern*.

So instead of using NullObject, using prefix names like
 - Default
 - No
 - Empty
 - Absent
 - Nonexistent

But when using these names you need to be sure they are appropriate within the context of the object.
