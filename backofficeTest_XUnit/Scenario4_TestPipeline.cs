using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using manaTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit
{
    public class Scenario4_TestPipeline : TestBase
    {
        //[Fact(DisplayName = "แจ้งปัญหาไปยัง backoffice ได้")]
        //public async Task ReportIssue()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.ReportIssue();
        //    res.Should().Be(true);
        //}

        [Fact(DisplayName = "สร้างบัญชีธนาคารที่ไม่มีอยู่จริงไม่ได้")]
        [TestPriority(100)]
        public async Task CreateBankAccountFail_NotRealAccountNumber()
        {
            var sut = new Account();
            var result = await sut.AddBankingAccount("E2ETest","0123456789");
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "สร้างบัญชีธนาคารได้สำเร็จ")]
        [TestPriority(200)]
        public async Task CreateBankAccountSuccess()
        {
            var sut = new Account();
            var result = await sut.AddBankingAccountSuccess("E2ETest", "4520342473");
            result.isSuccess.Should().BeTrue();
            var isTopupPage = result.page.Url.Contains("wallet-topup-ppay");
            isTopupPage.Should().BeTrue();
        }

        [Fact(DisplayName = "สร้างบัญชีธนาคารซ้ำไม่ได้")]
        [TestPriority(300)]
        public async Task CreateBankAccountFail_BankAccounAlreadyExists()
        {
            var sut = new Account();
            var result = await sut.AddBankingAccount("E2ETest", "4520342473");
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }
    }
}
