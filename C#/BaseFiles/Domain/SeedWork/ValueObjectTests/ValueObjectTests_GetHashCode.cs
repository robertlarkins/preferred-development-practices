using FluentAssertions;
using Xunit;

namespace Rezare.rSite.Domain.Tests.SeedWork.ValueObjectTests
{
    public class ValueObjectTests_GetHashCode
    {
        [Fact]
        public void EquivalentObjects_SameHashCodes()
        {
            // Arrange
            var baseObjectA = new ValueObjectScenarios.BaseObject();
            var baseObjectB = new ValueObjectScenarios.BaseObject();

            // Act
            var hashCodeA = baseObjectA.GetHashCode();
            var hashCodeB = baseObjectB.GetHashCode();

            // Assert
            hashCodeA.Should().Be(hashCodeB);
        }

        [Fact]
        public void DifferentObjects_DifferentHashCodes()
        {
            // Arrange
            var baseObject = new ValueObjectScenarios.BaseObject();
            var derivedObject = new ValueObjectScenarios.DerivedObject();

            // Act
            var baseObjectHashCode = baseObject.GetHashCode();
            var derivedObjectHashCode = derivedObject.GetHashCode();

            // Assert
            baseObjectHashCode.Should().NotBe(derivedObjectHashCode);
        }
    }
}
