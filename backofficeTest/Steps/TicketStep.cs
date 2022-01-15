using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class TicketStep
    {
        public async Task<(bool isSuccess, IPage page)> CreateNewTicket(string manaPhoneNo, string manaEmail, string description, string contactPhoneNo, string contactEmail)
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("text=เพิ่ม >> span");
            await page.FillAsync("input[name=\"ion-input-0\"]", manaPhoneNo);

            var validatePaIdResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=ตรวจสอบข้อมูล"), $"https://thman-test.onmana.space/api/User/verify/{manaPhoneNo}");
            var manaPhoneMsg = await validatePaIdResponse.TextAsync();
            var xxxx = JsonDocument.Parse(manaPhoneMsg).RootElement.GetProperty("isSuccess").ToString();
            bool.TryParse(xxxx, out bool result);
            if (false == result)
            {
                return (false, page);
            }

            await page.FillAsync("input[name=\"ion-input-3\"]", manaEmail);

            var saveResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=บันทึก >> span"), "https://thman-test.onmana.space/api/Ticket/status");
            if (false == saveResponse.Ok)
            {
                return (false, page);
            }

            //กรอกข้อมูล ticket
            await page.ClickAsync("text=ต้องระบุ, ประเภทปัญหา");
            await page.ClickAsync("button[role=\"radio\"]:has-text(\"อื่นๆ\")");
            await page.ClickAsync("button:has-text(\"ตกลง\")");
            //await page.ClickAsync("textarea[name=\"ion-textarea-0\"]");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", description);
            //await page.ClickAsync("input[name=\"ion-input-4\"]");
            await page.FillAsync("input[name=\"ion-input-4\"]", contactPhoneNo);
            //await page.ClickAsync("input[name=\"ion-input-5\"]");
            await page.FillAsync("input[name=\"ion-input-5\"]", contactEmail);

            //await page.ClickAsync("text=สร้าง >> button");

            var submitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=สร้าง >> button"), "https://thman-test.onmana.space/api/Ticket/operator/create");
            if (false == submitResponse.Ok)
            {
                return (false, page);
            }

            //await page.WaitForTimeoutAsync(3000);

            return (true, page);
            //เช็คว่าเพิ่ม ticket ได้ไหม
            //var resultx = await page.InnerTextAsync(":nth-match(:text(\"ติดต่อมานะต้องทำยังไง\"), 2)");
            //await page.ClickAsync(":nth-match(:text(\"ติดต่อมานะต้องทำยังไง\"), 2)");
            //return result;
            //return true;
        }
    }
}
