using backofficeTest.Steps;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Tests
{
    public class FraudTests
    {
        [Fact]
        public async Task InputPaIdWithNoKycThenCanNotCreate()
        {
            var sut = new FraudStep();
            var result = await sut.CreateNewFraud("1100200407594");
            result.Should().BeFalse();
        }

        [Fact]
        public async Task InputPaIdWithKycThenCanCreate()
        {
            var sut = new FraudStep();
            var result = await sut.CreateNewFraud("xxxx");
            result.Should().BeTrue();
        }
    }
}
