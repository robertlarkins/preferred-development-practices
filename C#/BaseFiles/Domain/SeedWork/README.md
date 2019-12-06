# SeedWork

This is the set of files that are common to each Domain Project.
These files along with their associated tests can be copied and place in to the Domain and Domain.Tests.Unit projects.

## ValueObject

ValueObject.cs provides the base implementation for value object classes.

 - https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects
 - https://enterprisecraftsmanship.com/2017/08/28/value-object-a-better-implementation/
 - old implementation: https://enterprisecraftsmanship.com/2015/01/03/value-objects-explained/
 - https://docs.microsoft.com/en-us/visualstudio/code-quality/use-roslyn-analyzers?view=vs-2019
 - https://stackoverflow.com/questions/125319/should-using-directives-be-inside-or-outside-the-namespace?rq=1
 - https://stackoverflow.com/questions/39708604/reorder-usings-and-keep-them-outside-of-the-namespace
 - https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/Configuration.md
 - https://blog.submain.com/stylecop-detailed-guide/
 - https://hackernoon.com/value-objects-like-a-pro-f1bfc1548c72

### Current Implementation

The `==` operator overload has [rule S3875](https://rules.sonarsource.com/csharp/RSPEC-3875) suppressed.
This rule would be ignored if `ValueObject` implements `IComparable<T>` or `IEquatable<T>`, but this has not been done for the reasons below. Overloading `==` is compariable to using `Equals`, thus suppressing S3875 seems to be the most appropriate approach.

`IComparable<ValueObject>` is not suitable on ValueObject as the decision to implement this is for the derived class, as `CompareTo` is used for object sort order.

`IEquatable<ValueObject>` could be implemented, but in the comments of this [article](https://enterprisecraftsmanship.com/posts/value-object-better-implementation/) Vladimir Khorikov states there is no need to implement `IEquatable<T>` as it is more for structs and has negligible benefit for reference types (removes a single cast), which `ValueObject` is. Additionally, if `IEquatable` is implemented, then [rule S4035](https://rules.sonarsource.com/csharp/RSPEC-4035) is applied in which `ValueObject` should be sealed. If this was to occur, then `ValueObject` could not be derived. The reason why `IEquatable` should be sealed is because it should always have the same behaviour as `Object.Equals`. If it is not sealed `IEquatable` could be implemented at different points in the object hierarchy without overriding `Object.Equals`, meaning they could each expressing equality differently.
Further Reading: https://stackoverflow.com/questions/1868316/should-iequatablet-icomparablet-be-implemented-on-non-sealed-classes

**Todo: Provide a description for why `Equals(ValueObject? obj)` is not implemented**
Only `Equals(object? obj)` has been implemented as `Equals(ValueObject? obj)`, `Equals(ValueObject? obj)` would avoid boxing of the object, but this adds additional code to ValueObject for minimal benefit.
https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=netframework-4.8
https://www.codeproject.com/Articles/20592/Implementing-IEquatable-Properly

In https://enterprisecraftsmanship.com/posts/value-object-better-implementation/ there is this comment:
> There's no need in implementing IEquatable<t>, this interface was introduced specifically to avoid boxing/unboxing of .NET value types (structs) when dealing with comparison. As long as you implement ValueObject as class, you can safely omit implementing IEquatable<t>.

which refers to by this Equals method isn't needed. Add the explanation to here.

There is no `ReferenceEquals(obj, this)` in `Equals(object? obj)`. `ReferenceEquals(obj, this)` would be used to check if `obj` had the same reference as `this`, and if it did it would just return `true`. No implementation I can find uses this to short-circuit checking each value. Should `ReferenceEquals(obj, this)` be included, or just ommitted and rely on checking each value?
 - Not using this keeps the code simpler.
 - Comparing an object against itself would likely be an infrequent occurrence.
 - This object is suppose to represent a ValueObject, so objects with different references could be deemed the same given their values. So not having a `ReferenceEquals` check means that if the object is compared against itself then it just does the same checking as if it was compared against some other ValueObject of the same type.
