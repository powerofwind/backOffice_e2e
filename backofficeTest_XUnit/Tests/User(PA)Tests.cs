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
        [Fact(DisplayName = "(Ticket) สามารถกดรับงานที่ยังไม่มีคนรับได้")]
        [TestPriority(100)]
        public async Task TicketCanBeTaken()
        {
            var sut = new User_PA_Step();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.User);

            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Kyc/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var content = await page.ContentAsync();
            content.Should().Contain(result.cardOwnerName);
        }
    }
}
