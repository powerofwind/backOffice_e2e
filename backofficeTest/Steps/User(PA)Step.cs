using backofficeTest.Helpers;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class User_PA_Step
    {
        public async Task<(IPage page, string cardOwnerName)> TakeLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.User);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:last-child button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.WaitForURLAsync($"{Pages.User}/detail/{ticketId}");
            return (page, cardOwnerName);
        }
    }
}
