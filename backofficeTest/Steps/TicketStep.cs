using backofficeTest.Helpers;
using Microsoft.Playwright;
using System;
using System.Linq;
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
            if (false == validatePaIdResponse.Ok || false == validatePaIdResponse.JsonAsync().Result.Value.GetProperty("isSuccess").GetBoolean())
            {
                return (false, page);
            }

            await page.FillAsync("input[name=\"ion-input-3\"]", manaEmail);
            await page.ClickAsync("text=บันทึก >> span");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.ClickAsync("text=ต้องระบุ, ประเภทปัญหา");
            await page.ClickAsync("button[role=\"radio\"]:has-text(\"อื่นๆ\")");
            await page.ClickAsync("button:has-text(\"ตกลง\")");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", description);
            await page.FillAsync("input[name=\"ion-input-4\"]", contactPhoneNo);
            await page.FillAsync("input[name=\"ion-input-5\"]", contactEmail);

            await page.ClickAsync("text=สร้าง >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (true, page);
        }

        public async Task<(IPage page, string cardOwnerName)> RollbackLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:last-child");

            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ย้ายงานกลับ");

            await page.ClickAsync("button >> nth=-1");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, cardOwnerName);
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

        public async Task<(IPage page, string cardOwnerName)> CloseTicketWithIncompleteStatus()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);
            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:last-child");

            await page.ClickAsync("text=ปิดงาน >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.ClickAsync("text=ตกลง");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ดำเนินการแก้ไขเรียบร้อยแล้ว");

            await page.ClickAsync("button >> nth=-1");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, cardOwnerName);
        }

        public async Task<(IPage page, string cardOwnerName)> ReOpenTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);
            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.ClickAsync("ion-card:first-child");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=Pencil Reopen ticket >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "test reopen");

            await page.ClickAsync("text=บันทึก >> span");
            await page.WaitForURLAsync($"{Pages.Ticket}/detail/{ticketId}");
            return (page, cardOwnerName);
        }

        public async Task<(IPage page, string cardOwnerName)> CloseTicketAllCompleteStatus()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);
            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");

            await page.ClickAsync("ion-card:last-child");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            do
            {
                try
                {
                    await page.ClickAsync("label:has-text(\"ยังไม่ถูกแก้\")", new PageClickOptions { Timeout = 1500 });
                }
                catch (Exception)
                {
                    break;
                }
                await page.ClickAsync("button[role=\"radio\"]:has-text(\"แก้สำเร็จแล้ว\")");
                await page.ClickAsync("button:has-text(\"OK\")");
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            } while (true);

            await page.ClickAsync("text=ปิดงาน >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ดำเนินการแก้ไขเรียบร้อยแล้ว");

            await page.ClickAsync("button >> nth=-1");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, cardOwnerName);
        }
    }
}
