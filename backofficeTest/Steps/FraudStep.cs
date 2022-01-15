using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var validatePaIdResponse = await page.RunAndWaitForResponseAsync(
                () => page.ClickAsync("text=ตรวจสอบข้อมูล"), "https://thman-test.onmana.space/api/User");
            //var xxx = await validatePaIdResponse.TextAsync();
            if (false == validatePaIdResponse.Ok)
            {
                return false;
            }

            var submitResponse = await page.RunAndWaitForResponseAsync(
                () => page.ClickAsync("text=บันทึก >> button"), "https://thman-test.onmana.space/api/Ticket/status");
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
