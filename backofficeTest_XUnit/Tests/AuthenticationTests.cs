﻿using backofficeTest;
using backofficeTest.Steps;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Tests
{
    public class AuthenticationTests
    {
        [Fact(DisplayName = "สามารถเข้าสู่ระบบได้")]
        public async Task LoginMustBeWorkCorrecly()
        {
            var sut = new AuthenticationStep();
            var page = await sut.Login();
            page.Url.Should().Be(Pages.Ticket);
            var content = await page.ContentAsync();
            content.Should().NotContain("กรุณา login ใหม่อีกครั้ง");
        }

        [Fact(DisplayName = "หลังจากเข้าสู่ระบบแล้วต้องสามารถเข้าหน้า Home ได้")]
        public async Task AfterLoggedInCanEnterToHomePage()
            => await validate(Pages.Home);

        [Fact(DisplayName = "หลังจากเข้าสู่ระบบแล้วต้องสามารถเข้าหน้า Ticket ได้")]
        public async Task AfterLoggedInCanEnterToTicketPage()
            => await validate(Pages.Ticket);

        [Fact(DisplayName = "หลังจากเข้าสู่ระบบแล้วต้องสามารถเข้าหน้า Fraud ได้")]
        public async Task AfterLoggedInCanEnterToFraudPage()
            => await validate(Pages.Fraud);

        [Fact(DisplayName = "หลังจากเข้าสู่ระบบแล้วต้องสามารถเข้าหน้า Frozen ได้")]
        public async Task AfterLoggedInCanEnterToFrozenPage()
            => await validate(Pages.Frozen);

        [Fact(DisplayName = "หลังจากเข้าสู่ระบบแล้วต้องสามารถเข้าหน้า User ได้")]
        public async Task AfterLoggedInCanEnterToUserPage()
            => await validate(Pages.User);

        private async Task validate(string gotoUrl)
        {
            var sut = new AuthenticationStep();
            var page = await sut.GotoAfterLogin(gotoUrl);
            page.Url.Should().Be(gotoUrl);
            var content = await page.ContentAsync();
            content.Should().NotContain("กรุณา login ใหม่อีกครั้ง");
        }
    }
}