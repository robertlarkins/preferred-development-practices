# xUnit Attributes

## `[Fact]`

Facts are tests which are always true. They test invariant conditions.
As such Facts are parameterless methods.


Fact has the following parameters
- `Skip`

See:
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#basic-tests-using-xunit-fact-

## `[Theory]`

Theories are tests which are only true for a particular set of data. This data is passed into the test method as parameters.

The Theory attribute is used in conjunction with one or more *DataAttributes* to provide the test data.
These DataAttributes are InlineData, ClassData and MemberData, detailed below.
Additionally, a single Theory test can have the same DataAttribute as many times as needed to provide the desired data scenarios.

See:
- 

### `[InlineData]`

See
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-the-theory-attribute-to-create-parameterised-tests-with-inlinedata-


### `[ClassData]`


See
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-a-dedicated-data-class-with-classdata-


### `[MemberData]`

MemberData can be a property or method in the same class or a separate class.

See
- https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/#using-generator-properties-with-the-memberdata-properties


> Notes:  
> XUnit use to have the attribute `[PropertyData]`, but this is now obsolete and superseded `[MemberData]`.

See:
- https://xunit.net/docs/getting-started/netcore/cmdline#write-first-theory
- https://www.infoworld.com/article/3168787/how-to-work-with-xunit-net-framework.html


## Custom DataAttribute

https://andrewlock.net/creating-a-custom-xunit-theory-test-dataattribute-to-load-data-from-json-files/

