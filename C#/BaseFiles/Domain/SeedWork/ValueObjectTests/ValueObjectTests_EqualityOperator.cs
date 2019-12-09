using FluentAssertions;
using Rezare.rSite.Domain.SeedWork;
using Xunit;

namespace Rezare.rSite.Domain.Tests.SeedWork.ValueObjectTests
{
    public class ValueObjectTests_EqualityOperator
    {
        [Theory]
        [MemberData(nameof(ValueObjectScenarios.EquivalentObjectsScenarios), MemberType = typeof(ValueObjectScenarios))]
        [MemberData(nameof(ValueObjectScenarios.NullValueObjectsScenario), MemberType = typeof(ValueObjectScenarios))]
        public void EquivalentObjects_ReturnTrue(
            ValueObject? left,
            ValueObject? right,
            string becauseMessage)
        {
            // Act
            var result = left == right;

            // Assert
            result.Should().BeTrue(becauseMessage);
        }

        [Theory]
        [MemberData(nameof(ValueObjectScenarios.DifferentValueObjectsScenarios), MemberType = typeof(ValueObjectScenarios))]
        [MemberData(nameof(ValueObjectScenarios.DerivedObjectsAreNotEqualScenarios), MemberType = typeof(ValueObjectScenarios))]
        public void DifferentObjects_ReturnFalse(
            ValueObject? left,
            ValueObject? right,
            string becauseMessage)
        {
            // Act
            var result = left == right;

            // Assert
            result.Should().BeFalse(becauseMessage);
        }
    }
}
