using backofficeTest;
using backofficeTest.Steps;
using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Tests
{
    public class User_PA_Tests : TestBase
    {
        [Fact(DisplayName = "(User(PA)) สามารถกดรับงานได้สำเร็จ")]
        [TestPriority(100)]
        public async Task TicketCanBeTaken()
        {
            var sut = new User_PA_Step();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.User);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Kyc/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(User(PA)) สามารถกดย้ายงานกลับได้")]
        [TestPriority(200)]
        public async Task TicketInMineCanBeRollback()
        {
            var sut = new User_PA_Step();
            var result = await sut.RollbackLastestTicket();

            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(User(PA)) สามารถกดรับงานได้สำเร็จ")]
        [TestPriority(300)]
        public async Task TicketCanBeTaken2()
        {
            var sut = new User_PA_Step();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.User);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Kyc/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้")]
        [TestPriority(400)]
        public async Task SentConsentInfo2User()
        {
            var sut = new User_PA_Step();
            var result = await sut.SentConsentInfo2User();
            result.Should().BeTrue();
        }

        //TODO: MANA APP user DENY CONSENT
        //TODO: MANA APP user APPROVE CONSENT**

        //[Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้")]
        //[TestPriority(600)]
        //public async Task SentConsentInfo2Manager()
        //{
        //    var sut = new User_PA_Step();
        //    var result = await sut.SentConsentInfo2Manager();
        //    result.Should().BeTrue();
        //}

        //TODO: MANA APP manager DENY CONSENT**
        //TODO: MANA APP manager APPROVE CONSENT

        [Fact(DisplayName = "(User (KYC)) ส่งคำขอการระงับบัญชี User ได้")]
        [TestPriority(800)]
        public async Task SentConsent4FreezeTicket()
        {
            var sut = new User_PA_Step();
            var result = await sut.FreezeTicket();
            result.Should().BeTrue();
        }

        //TODO: MANA APP manager DENY CONSENT**
        //TODO: MANA APP manager APPROVE CONSENT
        //Wait FreezonTest

        [Fact(DisplayName = "(User(KYC)) สั่ง Logout user ได้")]
        [TestPriority(1000)]
        public async Task ForceUserLogout()
        {
            var sut = new User_PA_Step();
            var result = await sut.LogOutTicket();
            result.Should().BeTrue();
        }

        //ปิดงาน
    }
}
