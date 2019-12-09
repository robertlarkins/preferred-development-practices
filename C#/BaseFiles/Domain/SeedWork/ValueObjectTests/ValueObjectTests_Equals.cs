using FluentAssertions;
using Moq;
using Rezare.rSite.Domain.SeedWork;
using Xunit;

namespace Rezare.rSite.Domain.Tests.SeedWork.ValueObjectTests
{
    public class ValueObjectTests_Equals
    {
        [Theory]
        [MemberData(nameof(ValueObjectScenarios.EquivalentObjectsScenarios), MemberType = typeof(ValueObjectScenarios))]
        public void EquivalentObjects_ReturnTrue(
            ValueObject left,
            ValueObject right,
            string becauseMessage)
        {
            // Act
            var result = left?.Equals(right);

            // Assert
            result.Should().BeTrue(becauseMessage);
        }

        [Theory]
        [MemberData(nameof(ValueObjectScenarios.DerivedObjectsAreNotEqualScenarios), MemberType = typeof(ValueObjectScenarios))]
        public void DifferentObjects_ReturnFalse(
            ValueObject left,
            ValueObject right,
            string becauseMessage)
        {
            // Act
            var result = left?.Equals(right);

            // Assert
            result.Should().BeFalse(becauseMessage);
        }

        [Fact]
        public void NullObject_ReturnFalse()
        {
            // Arrange
            var valueObjectMock = new Mock<ValueObject>();
            var valueObject = valueObjectMock.Object;

            // Act
            var result = valueObject.Equals(null);

            // Assert
            result.Should().BeFalse("valueObject is not null");
        }
    }
}
