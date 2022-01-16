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

        public async Task<(bool isSuccess, IPage page, string cardOwnerName)> RollbackLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:last-child");

            const string GetCardInfoApi = "https://thman-test.onmana.space/api/user/getoperatorinfo";
            var getCardInfoReponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=Return Up Back ย้ายกลับ >> button"), GetCardInfoApi);
            if (false == getCardInfoReponse.Ok)
            {
                return (false, page, cardOwnerName);
            }

            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ย้ายงานกลับ");

            const string RollbackCardApi = "https://thman-test.onmana.space/api/Ticket/rollback";
            var rollbackResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button >> nth=-1"), RollbackCardApi);
            return (rollbackResponse.Ok, page, cardOwnerName);
        }

        public async Task<(IPage page, string cardOwnerName)> TakeLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:last-child button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, cardOwnerName);
        }

        public async Task<(bool isSuccess, IPage page, string cardOwnerName)> CloseTicketWithIncompleteStatus()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);
            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");

            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
            await page.WaitForResponseAsync(GetMineTicketApi);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:last-child");

            await page.ClickAsync("text=ปิดงาน >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.ClickAsync("text=ตกลง");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ดำเนินการแก้ไขเรียบร้อยแล้ว");

            const string CloseTicketApi = "https://thman-test.onmana.space/api/Ticket/close";
            var closeTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button >> nth=-1"), CloseTicketApi);
            return (closeTicketResponse.Ok, page, cardOwnerName);
        }
    }
}
