# Unit Testing

http://blog.stevensanderson.com/2009/08/24/writing-great-unit-tests-best-and-worst-practises/ (the broken bullet points at the bottom are listed here: https://pastebin.com/n8PZBEU4).
The article mentions unit tests aren't good at finding bugs, which is true, as bugs should only occur when there has been a lapse in unit testing.
Unit testing (especially TDD) does three things: reduces bugs, provides living documentation, and works as a code design tool by making the developer think about code structure.

# Property Based Testing

Xunit, FsCheck, FsCheck.Xunit

Find non-obvious deficiencies

It is not deterministic - different tests will be run.
Running the property tests once may pass all tests but running it again may produce a failed test.

Value based testing is still needed, none of it is replaced, property testing is used in addition to find missing test cases for value testing.

Include both original and minimal found failed cases in the value testing as there are no guarantees that a shrinkage will hit the same fault.

Probably need to test the property tests with value tests, as they require logic for testing.

Need to use generators for constrained values.
