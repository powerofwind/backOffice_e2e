using backofficeTest;
using backofficeTest.Steps;
using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using manaTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Mana_n_BackOffice
{
    public class ConsentUserInfo : TestBase
    {
        //[Fact(DisplayName = "(Ticket) สามารถสร้าง Ticket ที่ยังไม่มีคนรับเรื่องได้")]
        //[TestPriority(100)]
        //public async Task InputAllValidThenCanCreateNewTicket2()
        //{
        //    var sut = new TicketStep();
        //    var desc = Guid.NewGuid().ToString();
        //    var result = await sut.CreateNewTicket("0910167715", "mana002kku@gmail.com", desc, "1234567890", "expected@gmail.com");

        //    result.isSuccess.Should().BeTrue();
        //    var page = result.page;
        //    await page.GotoAsync(Pages.Ticket);
        //    const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
        //    await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
        //    var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
        //    await page.WaitForSelectorAsync(targetTicketSelector);

        //    var ticketDetailApi = $"https://thman-test.onmana.space/api/Ticket/{result.ticketId}?page=-1";
        //    await page.RunAndWaitForResponseAsync(() => page.ClickAsync($"ion-card:has-text(\"{result.cardOwnerName}\")"), ticketDetailApi);
        //    await page.WaitForSelectorAsync($"text={desc}");
        //    await result.page.CloseAsync();
        //}

        //[Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้")]
        //[TestPriority(200)]
        //public async Task SentConsentInfo2User()
        //{
        //    var sut = new TicketStep();
        //    var result = await sut.SentConsentInfo2User();
        //    result.Should().BeTrue();
        //}

        //[Fact(DisplayName = "User ปฏิเสธการเข้าถึงข้อมูลได้")]
        //[TestPriority(300)]
        //public async Task UserRejectInfo()
        //{
        //    var sut = new Consent();
        //    var res = await sut.UserRejectInfo();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้2")]
        //[TestPriority(400)]
        //public async Task SentConsentInfo2User2()
        //{
        //    var sut = new TicketStep();
        //    var result = await sut.SentConsentInfo2User();
        //    result.Should().BeTrue();
        //}

        //[Fact(DisplayName = "User อนุมัติการเข้าถึงข้อมูลได้")]
        //[TestPriority(500)]
        //public async Task UserApproveInfo()
        //{
        //    var sut = new Consent();
        //    var res = await sut.UserApproveInfo();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้")]
        //[TestPriority(600)]
        //public async Task SentConsentInfo2Manager()
        //{
        //    var sut = new TicketStep();
        //    var result = await sut.SentConsentInfo2Manager();
        //    result.Should().BeTrue();
        //}

        //[Fact(DisplayName = "Manager ปฏิเสธการเข้าถึงข้อมูลได้")]
        //[TestPriority(700)]
        //public async Task ManagerRejectInfo()
        //{
        //    var sut = new Consent();
        //    var res = await sut.ManagerRejectInfo();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้2")]
        //[TestPriority(800)]
        //public async Task SentConsentInfo2Manager2()
        //{
        //    var sut = new TicketStep();
        //    var result = await sut.SentConsentInfo2Manager();
        //    result.Should().BeTrue();
        //}

        //[Fact(DisplayName = "Manager อนุมัติการเข้าถึงข้อมูลได้")]
        //[TestPriority(900)]
        //public async Task ManagerApproveInfo()
        //{
        //    var sut = new Consent();
        //    var res = await sut.ManagerApproveInfo();
        //    res.Should().Be(true);
        //}
    }
}
