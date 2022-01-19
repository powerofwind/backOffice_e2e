using backofficeTest;
using backofficeTest.Steps;
using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Tests
{
    public class FraudTests : TestBase
    {
        [Fact(DisplayName = "(Fraud) ไม่สามารถสร้าง fraud โดยใช้เลขบัตรประชาชนที่ไม่เคยผ่านการทำ KYC ได้")]
        public async Task InputPaIdWithNoKycThenCanNotCreate()
        {
            var sut = new FraudStep();
            var result = await sut.CreateNewFraud("0000000000000", null);
            result.validatePaId.Should().BeFalse();
        }
        
        [Fact(DisplayName = "(Fraud) สร้าง fraud โดยใช้เลขบัตรประชาชนที่ผ่านการ KYC ได้")]
        [TestPriority(100)]
        public async Task InputPaIdWithKycThenCanCreate()
        {
            var sut = new FraudStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewFraud("1100200407594", desc);

            result.validatePaId.Should().BeTrue();
            var content = await result.page.ContentAsync();
            content.Should().Contain(desc);
        }

        [Fact(DisplayName = "(Fraud) สามารถกดย้ายงานกลับได้")]
        [TestPriority(200)]
        public async Task TicketInMineCanBeRollback()
        {
            var sut = new FraudStep();
            var result = await sut.RollbackLastestTicket();

            var content = await result.page.ContentAsync();
            content.Should().Contain(result.cardOwnerName);
        }
    }
}
