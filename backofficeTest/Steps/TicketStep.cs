using backofficeTest.Helpers;
using Microsoft.Playwright;
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

            var validatePaIdUrl = $"https://thman-test.onmana.space/api/User/verify/{manaPhoneNo}";
            var validatePaIdResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=ตรวจสอบข้อมูล"), validatePaIdUrl);
            var validatePaIdJson = await validatePaIdResponse.TextAsync();
            var validatePaIdResultTxt = JsonDocument.Parse(validatePaIdJson).RootElement.GetProperty("isSuccess").ToString();
            bool.TryParse(validatePaIdResultTxt, out bool validatePaIdResult);
            if (false == validatePaIdResult)
            {
                return (false, page);
            }

            await page.FillAsync("input[name=\"ion-input-3\"]", manaEmail);

            const string SaveTicketApi = "https://thman-test.onmana.space/api/Ticket/status";
            var saveTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=บันทึก >> span"), SaveTicketApi);
            if (false == saveTicketResponse.Ok)
            {
                return (false, page);
            }

            //กรอกข้อมูล ticket
            await page.ClickAsync("text=ต้องระบุ, ประเภทปัญหา");
            await page.ClickAsync("button[role=\"radio\"]:has-text(\"อื่นๆ\")");
            await page.ClickAsync("button:has-text(\"ตกลง\")");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", description);
            await page.FillAsync("input[name=\"ion-input-4\"]", contactPhoneNo);
            await page.FillAsync("input[name=\"ion-input-5\"]", contactEmail);

            const string CreateTicketApi = "https://thman-test.onmana.space/api/Ticket/operator/create";
            var submitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=สร้าง >> button"), CreateTicketApi);
            return (submitResponse.Ok, page);
        }
    }
}
