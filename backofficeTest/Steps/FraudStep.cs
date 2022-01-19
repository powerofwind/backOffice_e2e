using E2E.Shared;
using Microsoft.Playwright;
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

            await page.ClickAsync("textarea");
            await page.FillAsync("textarea", desc);

            await page.ClickAsync("text=บันทึก >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            return (true, page);
        }

        public async Task<(IPage page, string cardOwnerName)> RollbackLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Fraud);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:last-child");

            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Fraud ย้ายงานกลับ");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForURLAsync(Pages.Fraud);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, cardOwnerName);
        }
    }
}
