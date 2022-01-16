using backofficeTest.Helpers;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class FraudStep
    {
        public async Task<bool> CreateNewFraud(string paId)
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Fraud);

            await page.ClickAsync("text=เพิ่ม >> span");
            await page.FillAsync("input[name=\"ion-input-0\"]", paId);

            const string ValidatePaIdApi = "https://thman-test.onmana.space/api/User";
            var validatePaIdResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=ตรวจสอบข้อมูล"), ValidatePaIdApi);
            if (false == validatePaIdResponse.Ok)
            {
                return false;
            }

            const string SaveFraudApi = "https://thman-test.onmana.space/api/Ticket/status";
            var submitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=บันทึก >> button"), SaveFraudApi);
            if (false == submitResponse.Ok)
            {
                return false;
            }

            await page.ClickAsync("textarea");
            await page.FillAsync("textarea", "มีเงินมากกว่า 1,000,000 ใน 1 วัน");
            await page.ClickAsync("text=บันทึก >> button");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("text=ย้ายกลับ >> button");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("textarea");
            await page.FillAsync("textarea", "คืนงานนะ");
            await page.WaitForTimeoutAsync(1000);
            await page.ClickAsync("button >> nth=-1");
            return true;
        }
    }
}
