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
    public class FrozenTests : TestBase
    {
        //ต้องทำ TicketTest TestPriority(1100) ให้สำเร็จก่อน
        [Fact(DisplayName = "(Frozen) สามารถกดรับงานได้สำเร็จ")]
        [TestPriority(100)]
        public async Task TicketCanBeTaken()
        {
            var sut = new FrozenStep();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.Frozen);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Frozen/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Frozen) สามารถกดย้ายงานกลับได้")]
        [TestPriority(200)]
        public async Task TicketInMineCanBeRollback()
        {
            var sut = new FrozenStep();
            var result = await sut.RollbackLastestTicket();

            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        //[Fact(DisplayName = "(Frozen) สามารถกดรับงานได้สำเร็จ")]
        //[TestPriority(300)]
        //public async Task TicketCanBeTaken2()
        //{
        //    var sut = new FrozenStep();
        //    var result = await sut.TakeLastestTicket();

        //    var page = result.page;
        //    await page.GotoAsync(Pages.Frozen);
        //    const string GetMineTicketApi = "https://thman-test.onmana.space/api/Frozen/list/Mine?search=&page=-1";
        //    await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
        //    var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
        //    await page.WaitForSelectorAsync(targetTicketSelector);
        //    await page.CloseAsync();
        //}

        //[Fact(DisplayName = "(Frozen) ส่งคำขอยกเลิกการระงับบัญชี User ได้")]
        //[TestPriority(400)]
        //public async Task SentConsent4UnFreezeTicket()
        //{
        //    var sut = new FrozenStep();
        //    var result = await sut.UnFreezeTicket();

        //    result.isUnFreeze.Should().BeTrue();
        //    var page = result.page;
        //    await page.WaitForSelectorAsync("ion-card");
        //    var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
        //    var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
        //    qry.Count.Should().Be(0);
        //    await result.page.CloseAsync();
        //}

        ////TODO: MANA APP user DENY CONSENT

        //[Fact(DisplayName = "(Frozen) ทำการ Reopen บัญชีที่ถูกระงับที่สถานะยังไม่ผ่านได้")]
        //[TestPriority(500)]
        //public async Task ReOpenTicket()
        //{
        //    var sut = new FrozenStep();
        //    var result = await sut.ReOpenTicket();

        //    var page = result.page;
        //    await page.GotoAsync(Pages.Frozen);
        //    const string GetMineTicketApi = "https://thman-test.onmana.space/api/Frozen/list/Mine?search=&page=-1";
        //    await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
        //    var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
        //    await page.WaitForSelectorAsync(targetTicketSelector);
        //    await page.CloseAsync();
        //}

        //[Fact(DisplayName = "(Frozen) ส่งคำขอยกเลิกการระงับบัญชี User ได้")]
        //[TestPriority(600)]
        //public async Task SentConsent4UnFreezeTicket2()
        //{
        //    var sut = new FrozenStep();
        //    var result = await sut.UnFreezeTicket();

        //    result.isUnFreeze.Should().BeTrue();
        //    var page = result.page;
        //    await page.WaitForSelectorAsync("ion-card");
        //    var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
        //    var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
        //    qry.Count.Should().Be(0);
        //    await result.page.CloseAsync();
        //}

        ////TODO: MANA APP user APPROVE CONSENT

        [Fact(DisplayName = "(Frozen) ขอ Consent ข้อมูลผู้ใช้ไปยัง User ได้")]
        [TestPriority(900)]
        public async Task SentConsentInfo2User()
        {
            var sut = new FrozenStep();
            var result = await sut.SentConsentInfo2User();
            result.Should().BeTrue();
        }

        //TODO: MANA APP user DENY CONSENT
        //TODO: MANA APP user APPROVE CONSENT**

        [Fact(DisplayName = "(Frozen) ขอ Consent ข้อมูลผู้ใช้ไปยัง Manager ได้")]
        [TestPriority(1000)]
        public async Task SentConsentInfo2Manager()
        {
            var sut = new FrozenStep();
            var result = await sut.SentConsentInfo2Manager();
            result.Should().BeTrue();
        }

        //TODO: MANA APP manager DENY CONSENT**
        //TODO: MANA APP manager APPROVE CONSENT**
    }
}
