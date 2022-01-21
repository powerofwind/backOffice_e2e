using backofficeTest.Helpers;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class FrozenStep
    {
        public async Task<(IPage page, string ticketId)> TakeLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Frozen);

            await page.ClickAsync("ion-card:last-child button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.WaitForSelectorAsync("h1:has-text(\"Operator\")");

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.WaitForURLAsync($"{Pages.Frozen}/detail/{ticketId}");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> RollbackLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Frozen);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            await page.WaitForTimeoutAsync(300);
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Frozen ย้ายงานกลับ");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForURLAsync(Pages.Frozen);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<(bool isUnFreeze, string ticketId, IPage page)> UnFreezeTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Frozen);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            await page.WaitForTimeoutAsync(300);
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=ยกเลิกการระงับบัญชีชั่คราว >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ยกเลิกการ Freeze");

            const string UnFreezeTicketApi = "https://thman-test.onmana.space/api/Frozen/unfreeze";
            var UnFreezeTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button >> nth=-1"), UnFreezeTicketApi);

            if (false == UnFreezeTicketResponse.Ok)
            {
                return (false, ticketId, page);
            }
            return (true, ticketId, page);
        }

        public async Task<(IPage page, string ticketId)> ReOpenTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Frozen);

            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");
            await page.WaitForSelectorAsync("ion-card");
            await page.ClickAsync("ion-card:first-child");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=Pencil Reopen ticket >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Frozen reopen");

            var reopenTicketApi = $"https://thman-test.onmana.space/api/Frozen/reopen";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=บันทึก >> span"), reopenTicketApi);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<bool> SentConsentInfo2User()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Frozen);

            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            const string sentConsentApi = "https://thman-test.onmana.space/api/User/userinfo/consent";
            var sentConsentResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text= ขอ User  >> span"), sentConsentApi);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            if (false == sentConsentResponse.Ok)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SentConsentInfo2Manager()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Frozen);

            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            const string sentConsentApi = "https://thman-test.onmana.space/api/User/userinfo/consent";
            var sentConsentResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text= ขอ Manager  >> span"), sentConsentApi);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            if (false == sentConsentResponse.Ok)
            {
                return false;
            }
            return true;
        }
    }
}

