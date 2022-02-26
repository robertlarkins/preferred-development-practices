# xUnit Attributes

The following attributes are what xUnit uses to identify a method as a test. They also specify what the test type is and how it runs.

## `[Fact]`

Facts are tests which provide a _fact_ about the system that should always be true. They test invariant conditions.

As such Facts are parameterless methods.

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
```

Fact has the following parameters
- `Skip` - When given a non-null, non-empty string (the skip *reason*), the test is not run.
- `DisplayName` - A custom name for the test
- `Timeout` - Some timeout (in milliseconds) for the test. Only works when parallelisation is disabled.

See:
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#basic-tests-using-xunit-fact-

## `[Theory]`

Theories are tests which are only true for a particular set of data. This data is passed into the test method as parameters.

The Theory attribute is used in conjunction with one or more *DataAttributes* to provide the test data.
These DataAttributes are InlineData, ClassData and MemberData, detailed below.
Additionally, a single Theory test can have the same DataAttribute as many times as needed to provide the desired data scenarios.

> Notes:  
> XUnit use to have the attribute `[PropertyData]`, but this is now obsolete and superseded by `[MemberData]`.


### `[InlineData]`

InlineData is the simplest way of providing scenario data into a theory test. It allows data to be provided as parameters. The order of the attribute parameters is the same order as the method parameters.

```C#
[Theory]
[InlineData(7, 13, 91)]
[InlineData(0, 1, 0)]
[InlineData(2, 4, 8)]
[InlineData(-2, -11, 22)]
public void MultiplicationOfTwoNumbers(int firstNumber, int secondNumber, int expected)
{
    var sut = new Calculator();
  
    var result = sut.Multiply(firstNumber, secondNumber);
  
    result.Should().Be(expected);
}
```

InlineData can only have parameters that are [primitive types](https://github.com/robertlarkins/software-engineering-glossary/blob/master/Types.md#primitive-or-simple) (also known as simple types or built-in value types), string, and implicit CLR type conversions, such as
- `string` to `Guid` (the string must be a valid Guid, eg: "7834200c-0748-4b77-ac77-624f950c0b96")
- `string` to `DateTime`  
  the string must be a valid DateTime, eg: "1989-5-13" or "2012-12-31T23:59:59.999Z"
  Note: Check the DateTime.Kind of the param as while `Z` might be provided it maybe converted to Local kind, which changes the presented datetime (it is still the correct time in UTC, just presented in the local time).

If there are many test scenarios, then an alternative DataAttribute may allow this data to be presented more clearly and concisely.

See
- https://github.com/xunit/xunit/issues/2131#issuecomment-670980154
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-the-theory-attribute-to-create-parameterised-tests-with-inlinedata-


### `[ClassData]`

```C#
[Theory]
[ClassData(typeof(CalculatorMultiplicationScenarios))]
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
```

**Does anything special need to be done if class is in its own file?**

See
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-a-dedicated-data-class-with-classdata-
- https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/#using-theorydata-with-the-classdata-attribute

### `[MemberData]`

MemberData can be a property or method in the same class or a separate class.

```C#
public class MemberDataExamples
{
    [Theory]
    [MemberData(nameof(CalculatorPositiveMultiplicationScenarios))]
    [MemberData(nameof(CalculatorNegativeMultiplicationScenarios))]
    [MemberData(nameof(MoreCalculatorScenarios.MultiplicationWithZeroScenarios, MemberType = typeof(MoreCalculatorScenarios)))]
    [MemberData(nameof(MoreCalculatorScenarios.PositiveTimesNegativeScenarios, MemberType = typeof(MoreCalculatorScenarios)))]
    public void MultiplicationOfTwoNumbers(int firstNumber, int secondNumber, int expected)
    {
        var sut = new Calculator();
  
        var result = sut.Multiply(firstNumber, secondNumber);
  
        result.Should().Be(expected);
    }

    // Memberdata using a Method
    public static TheoryData<int, int, int> CalculatorPositiveMultiplicationScenarios()
    {
        return new TheoryData<int, int, int>
        {
            { 7, 13, 91 },
            { 2, 4, 8 }
        }
    }

    // Memberdata using a Property
    public static TheoryData<int, int, int> CalculatorNegativeMultiplicationScenarios =>
        new TheoryData<int, int, int>
        {
            { -2, -11, 22 }
        };
}

public class MoreCalculatorScenarios
{
    // Memberdata Scenarios using a Method in another Class
    public static TheoryData<int, int, int> MultiplicationWithZeroScenarios()
    {
        return new TheoryData<int, int, int>
        {
            { 7, 0, 0 },
            { -2, 0, 0 }
        }
    }
    
    // Memberdata Scenarios using a Property in another Class
    public static TheoryData<int, int, int> PositiveTimesNegativeScenarios =>
        new TheoryData<int, int, int>
        {
            { -2, 11, -22 }
        };
}
```

Add MemberData parameter example to show how memberdata can pass a parameter into the scenario method.


See
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-generator-properties-with-the-memberdata-properties
- https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/#using-theorydata-with-the-memberdata-attribute

### `TheoryData`

In the past ClassData and TheoryData would provide the data using `IEnumerable<object[]>`, with each item in `object[]` being a passed in parameter.
However, this approach is not strongly typed, and if there is a value in the data that does not match the parameter type then this will cause a run time exception.
Due to this xUnit has introduced the strongly typed `TheoryData<>` class. The advantage of this is that data issues will be discovered at compile time.

- https://hamidmosalla.com/2020/04/05/xunit-part-8-using-theorydata-instead-of-memberdata-and-classdata/
- https://andrewlock.net/creating-strongly-typed-xunit-theory-test-data-with-theorydata/

See:
- https://xunit.net/docs/getting-started/netcore/cmdline#write-first-theory
- https://www.infoworld.com/article/3168787/how-to-work-with-xunit-net-framework.html


## Custom DataAttribute

https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/

