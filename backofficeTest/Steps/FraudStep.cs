using backofficeTest.Helpers;
using Microsoft.Playwright;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class FraudStep
    {
        public async Task<(bool validatePaId, IPage page)> CreateNewFraud(string paId, string desc)
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Fraud);

            await page.ClickAsync("text=เพิ่ม >> span");
            await page.FillAsync("input[name=\"ion-input-0\"]", paId);

            const string ValidatePaIdApi = "https://thman-test.onmana.space/api/User";
            var validatePaIdResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=ตรวจสอบข้อมูล"), ValidatePaIdApi);
            if (false == validatePaIdResponse.Ok)
            {
                return (false, page);
            }

            const string SaveFraudApi = "https://thman-test.onmana.space/api/user/getoperatorinfo";
            var submitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=บันทึก >> button"), SaveFraudApi);
            if (false == submitResponse.Ok)
            {
                return (false, page);
            }

            await page.ClickAsync("textarea");
            await page.FillAsync("textarea", desc);

            await page.ClickAsync("text=บันทึก >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            return (true, page);
        }

        public async Task<(IPage page, string ticketId)> RollbackLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Fraud);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            await page.WaitForTimeoutAsync(300);
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Fraud ย้ายงานกลับ");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForURLAsync(Pages.Fraud);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> TakeLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Fraud);

            const string AcceptTicketApi = "https://thman-test.onmana.space/api/user/getoperatorinfo";
            var acceptTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-card:last-child button"), AcceptTicketApi);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.WaitForURLAsync($"{Pages.Fraud}/detail/{ticketId}");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<bool> SentConsentInfo2User()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Fraud);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
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
            await page.GotoAsync(Pages.Fraud);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
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
