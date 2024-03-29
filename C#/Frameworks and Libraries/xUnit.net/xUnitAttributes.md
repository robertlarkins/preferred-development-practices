# xUnit Attributes

The following attributes are what xUnit uses to identify a method as a test. They also specify what the test type is and how it runs.


## `[Fact]`

Facts are tests which provide a _fact_ about the system that should always be true. They test invariant conditions.

As such Facts are parameterless methods.

*References*:
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#basic-tests-using-xunit-fact-


#### Parameters

- `Skip`  
  When given a non-null, non-empty string (the skip *reason*), the test is not run.


#### Examples

```C#
[Fact]
public void MultiplicationOfTwoNumbers()
{
    // Arrange
    var firstNumber = 7;
    var secondNumber = 13;
    var sut = new Calculator();
  
    // Act
    var result = sut.Multiply(firstNumber, secondNumber);
  
    // Assert
    result.Should().Be(91);
}

[Fact(Skip = "Unimplemented test.")]
public void DivisionOfTwoNumbers()
{
    var sut = new Calculator();
    // var result = sut.Division(99, 11);
    result.Should().Be(9);
}
```


## `[Theory]`

Theories are tests which are only true for a particular set of data. This data is passed into the test method as parameters.

The Theory attribute is used in conjunction with one or more *DataAttributes* to provide the test data.
These DataAttributes are InlineData, ClassData and MemberData, detailed below.
Additionally, a single Theory test can have the same DataAttribute as many times as needed to provide the desired data scenarios.

> Note:  
> XUnit use to have the attribute `[PropertyData]`, but this is now obsolete and superseded by `[MemberData]`.

*References*:
- https://xunit.net/docs/getting-started/netcore/cmdline#write-first-theory


### `[InlineData]`

InlineData is the simplest way of providing scenario data into a theory test. It allows constant data to be provided as parameters.
The order of the attribute parameters is the same order as the method parameters.

If there are many test scenarios, then an alternative *DataAttribute* may allow this data to be presented more clearly and concisely.

*References*:
- https://github.com/xunit/xunit/issues/2131#issuecomment-670980154
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-the-theory-attribute-to-create-parameterised-tests-with-inlinedata-


#### Parameters

- `Skip`  
  When given a non-null, non-empty string (the skip *reason*), the test is not run.

InlineData can only have parameters that are constant expressions, such as [primitive types](https://github.com/robertlarkins/software-engineering-glossary/blob/master/Types.md#primitive-or-simple) (also known as simple types or built-in value types), `string`, `enum`, `Type`, and implicit CLR type conversions.
Examples of type conversions:
- `string` to `Guid` (the string must be a valid Guid, eg: "7834200c-0748-4b77-ac77-624f950c0b96")
- `string` to `DateTime`  
  the string must be a valid `DateTime`, eg: "1989-5-13" or "2012-12-31T23:59:59.999Z"  
  *Note*: Check the `DateTime.Kind` of the param as while `Z` might be provided it maybe converted to Local kind,  
  which changes the presented datetime (it is still the correct time in UTC, just presented in the local time).
- `int` to `Enum`


#### Examples

```C#
[Theory]
[InlineData(7, 13, 91)]
[InlineData(0, 1, 0)]
[InlineData(2, 4, 8)]
[InlineData(-2, -11, 22)]
[InlineData(2.5, 4, 10, Skip = "Floating point (real) numbers are not supported yet.")]
public void MultiplicationOfTwoNumbers(int firstNumber, int secondNumber, int expected)
{
    var sut = new Calculator();
  
    var result = sut.Multiply(firstNumber, secondNumber);
  
    result.Should().Be(expected);
}
```


### `[ClassData]`

The `ClassData` attribute allows test data to be defined as a class. Each scenario set would be its own class.
This also allows references types to be provided as parameters.

*References*:
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-a-dedicated-data-class-with-classdata-
- https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/#using-theorydata-with-the-classdata-attribute


#### Parameters

- `Skip`  
  When given a non-null, non-empty string (the skip *reason*), the test is not run.


#### Examples

```C#
[Theory]
[ClassData(typeof(CalculatorMultiplicationScenarios))]
[ClassData(typeof(UnimplementedScenarios), Skip = "Unimplemented")]
public void MultiplicationOfTwoNumbers(int firstNumber, int secondNumber, int expected)
{
    var sut = new Calculator();
  
    var result = sut.Multiply(firstNumber, secondNumber);
  
    result.Should().Be(expected);
}

public class CalculatorMultiplicationScenarios : TheoryData<int, int, int>
{
    public CalculatorMultiplicationScenarios()
    {
        Add(7, 13, 91);
        Add(0, 1, 0);
        Add(2, 4, 8);
        Add(-2, -11, 22);
    }
}

public class UnimplementedScenarios : TheoryData<int, int, int>
{
}
```


### `[MemberData]`

The `MemberData` attribute allows test data to be retrieved from a field, property or method.
These members can reside in the same class as the tests or in a separate class.
This test data can also contain references types.

*References*:
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-generator-properties-with-the-memberdata-properties
- https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/#using-theorydata-with-the-memberdata-attribute


#### Parameters

`MemberData` has the following two parameters (provided into its constructor) that must appear before the attribute properties:
- `memberName`  
  a required parameter for providing the name of the member where the data is coming from.
- `parameters`  
  an optional params parameter for supplying parameters into the member that provides the scenario data.
  Like `InlineData`, these parameters must be constant expressions. If the member is not a method then these parameters are ignored.

The following are attribute properties, they are *optional* parameters that need to be *named* to be set:
- `Skip`  
  When given a non-null, non-empty string (the skip *reason*), the test is not run.
- `DisableDiscoveryEnumeration`
- `MemberType`


#### Examples

```C#
public class MemberDataExamples
{
    [Theory]
    [MemberData(nameof(MultiplicationScenariosInField))]
    [MemberData(nameof(MultiplicationScenariosInProperty))]
    [MemberData(nameof(MultiplicationScenariosInMethod))]
    [MemberData(nameof(MoreCalculatorScenarios.MultiplicationScenarios), MemberType = typeof(MoreCalculatorScenarios))]
    [MemberData(nameof(MoreCalculatorScenarios.SkipExample), MemberType = typeof(MoreCalculatorScenarios), Skip = "Skip this scenario")]
    [MemberData(nameof(MultiplicationScenariosWithOneParameter), 1)]
    [MemberData(nameof(MultiplicationScenariosWithMultipleParameters), parameters: new object[] { 1, false })]  // naming the parameters param is optional
    [MemberData(nameof(MoreCalculatorScenarios.WithMultipleParameters), new object[] { 5, true }, MemberType = typeof(MoreCalculatorScenarios))]
    WithMultipleParameters
    public void MultiplicationOfTwoNumbers(int firstNumber, int secondNumber, int expected)
    {
        var sut = new Calculator();
  
        var result = sut.Multiply(firstNumber, secondNumber);
  
        result.Should().Be(expected);
    }

    // The readonly modifier is an expression of intent to convey that this value is immutable.
    // It is only applicable to a field.
    // This value shouldn't be changed by the tests anyway.
    public static readonly TheoryData<int, int, int> MultiplicationScenariosInField =
        new TheoryData<int, int, int>
        {
            { 7, 4, 28 }
        };

    public static TheoryData<int, int, int> MultiplicationScenariosInProperty =>
        new TheoryData<int, int, int>
        {
            { -2, -11, 22 }
        };

    public static TheoryData<int, int, int> MultiplicationScenariosInMethod()
    {
        return new TheoryData<int, int, int>
        {
            { 7, 13, 91 },
            { 2, 4, 8 }
        }
    }
    
    public static TheoryData<int, int, int> MultiplicationScenariosWithOneParameter(int p1)
    {
        return new TheoryData<int, int, int>
        {
            { p1, 0, 0 }
        };
    }
    
    public static TheoryData<int, int, int> MultiplicationScenariosWithMultipleParameters(int p1, bool p2)
    {
        return new TheoryData<int, int, int>
        {
            { p2? p1 : 7, 0, 0 }
        };
    }
}

public class MoreCalculatorScenarios
{
    // Memberdata Scenarios using a Method in another Class
    public static TheoryData<int, int, int> MultiplicationScenarios()
    {
        return new TheoryData<int, int, int>
        {
            { 7, 0, 0 },
            { -2, 0, 0 }
        }
    }
    
    // Memberdata Scenarios using a Property in another Class
    public static TheoryData<int, int, int> SkipExample =>
        new TheoryData<int, int, int>
        {
            { -2, 11, -22 }
        };
        
    public static TheoryData<int, int, int> WithMultipleParameters(int p1, bool p2)
    {
        return new TheoryData<int, int, int>
        {
            { p2? p1 : 7, 0, 0 }
        };
    }
}
```


### `TheoryData` Type

Provides strong typing for the `ClassData` and `TheoryData` implementations.

In the past ClassData and TheoryData would provide the data using `IEnumerable<object[]>`, with each item in `object[]` being a passed in parameter.
However, this approach is not strongly typed, and if there is a value in the data that does not match the parameter type then this will cause a run time exception.
Due to this xUnit has introduced the strongly typed `TheoryData<>` class. The advantage of this is that data issues will be discovered at compile time.

*References*:
- https://hamidmosalla.com/2020/04/05/xunit-part-8-using-theorydata-instead-of-memberdata-and-classdata/
- https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/


## Custom DataAttribute

https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/

