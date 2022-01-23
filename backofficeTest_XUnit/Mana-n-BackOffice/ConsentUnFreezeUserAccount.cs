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
    public class ConsentUnFreezeUserAccount : TestBase
    {
        [Fact(DisplayName = "(Frozen) สามารถกดรับงานได้สำเร็จ")]
        [TestPriority(100)]
        public async Task TicketCanBeTaken2()
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

        [Fact(DisplayName = "(Frozen) ส่งคำขอยกเลิกการระงับบัญชี User ได้")]
        [TestPriority(200)]
        public async Task SentConsent4UnFreezeTicket()
        {
            var sut = new FrozenStep();
            var result = await sut.UnFreezeTicket();

            result.isUnFreeze.Should().BeTrue();
            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager ปฏิเสธการยกเลิกการระงับบัญชีได้")]
        [TestPriority(300)]
        public async Task ManagerRejectCancelSuspendAccount()
        {
            var sut = new Consent();
            var res = await sut.ManagerRejectCancelSuspendAccount();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "(Frozen) ทำการ Reopen บัญชีที่ถูกระงับที่สถานะยังไม่ผ่านได้")]
        [TestPriority(400)]
        public async Task ReOpenTicket()
        {
            var sut = new FrozenStep();
            var result = await sut.ReOpenTicket();

            var page = result.page;
            await page.GotoAsync(Pages.Frozen);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Frozen/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Frozen) ส่งคำขอยกเลิกการระงับบัญชี User ได้")]
        [TestPriority(500)]
        public async Task SentConsent4UnFreezeTicket2()
        {
            var sut = new FrozenStep();
            var result = await sut.UnFreezeTicket();

            result.isUnFreeze.Should().BeTrue();
            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager อนุมัติการยกเลิกการระงับบัญชีได้")]
        [TestPriority(600)]
        public async Task ManagerApproveCancelSuspendAccount()
        {
            var sut = new Consent();
            var res = await sut.ManagerApproveCancelSuspendAccount();
            res.Should().Be(true);
        }
    }
}
