using backofficeTest.Helpers;
using Microsoft.Playwright;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class TicketStep
    {
        public async Task<(bool isSuccess, IPage page, string cardOwnerName, string ticketId)> CreateNewTicket(string manaPhoneNo, string manaEmail, string description, string contactPhoneNo, string contactEmail)
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("text=เพิ่ม >> span");
            await page.FillAsync("input[name=\"ion-input-0\"]", manaPhoneNo);
            var validatePaIdUrl = $"https://thman-test.onmana.space/api/User/verify/{manaPhoneNo}";
            var validatePaIdResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=ตรวจสอบข้อมูล"), validatePaIdUrl);
            if (false == validatePaIdResponse.Ok || false == validatePaIdResponse.JsonAsync().Result.Value.GetProperty("isSuccess").GetBoolean())
            {
                return (false, page, null, null);
            }

            await page.WaitForSelectorAsync("input[name=\"ion-input-3\"]");
            await page.FillAsync("input[name=\"ion-input-3\"]", manaEmail);
            const string CreateTicketStatusApi = "https://thman-test.onmana.space/api/Ticket/status";
            var createTicketStatusResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=บันทึก >> span"), CreateTicketStatusApi);
            if (false == createTicketStatusResponse.Ok)
            {
                return (false, page, null, null);
            }

            await page.WaitForSelectorAsync("text=ต้องระบุ, ประเภทปัญหา");
            await page.ClickAsync("text=ต้องระบุ, ประเภทปัญหา");
            await page.ClickAsync("button[role=\"radio\"]:has-text(\"อื่นๆ\")");
            await page.ClickAsync("button:has-text(\"ตกลง\")");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", description);
            await page.FillAsync("input[name=\"ion-input-4\"]", contactPhoneNo);
            await page.FillAsync("input[name=\"ion-input-5\"]", contactEmail);
            const string CreateTicketApi = "https://thman-test.onmana.space/api/Ticket/operator/create";
            var createTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=สร้าง >> button"), CreateTicketApi);
            if (false == createTicketResponse.Ok)
            {
                return (false, page, null, null);
            }

            var redirectUrl = (await createTicketResponse.JsonAsync())?.GetProperty("redirectUrl").ToString() ?? string.Empty;
            var ticketId = redirectUrl.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            var cardOwnerName = await page.InnerTextAsync("ion-card:last-child h2:first-child");
            await page.WaitForURLAsync($"{Pages.Ticket}/detail/{ticketId}");
            await page.WaitForTimeoutAsync(500);

            return (true, page, cardOwnerName, ticketId);
        }

        public async Task<(IPage page, string ticketId)> RollbackLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            await page.WaitForTimeoutAsync(300);
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Ticket ย้ายงานกลับ");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForURLAsync(Pages.Ticket);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> TakeLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-card:last-child button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.WaitForSelectorAsync("h1:has-text(\"Operator\")");

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.WaitForURLAsync($"{Pages.Ticket}/detail/{ticketId}");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> CloseTicketWithIncompleteStatus()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.ClickAsync("ion-card:last-child");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=ปิดงาน >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=ตกลง");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ดำเนินการแก้ไขเรียบร้อยแล้ว");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForURLAsync(Pages.Ticket);
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> ReOpenTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");
            await page.WaitForSelectorAsync("ion-card");
            await page.ClickAsync("ion-card:first-child");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=Pencil Reopen ticket >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Ticket reopen");

            var ticketDetailApi = $"https://thman-test.onmana.space/api/Ticket/{ticketId}?page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=บันทึก >> span"), ticketDetailApi);
            await page.WaitForURLAsync($"{Pages.Ticket}/detail/{ticketId}");
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> CloseTicketAllCompleteStatus()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.ClickAsync("ion-card:last-child");
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            var ticketDetailApi = $"https://thman-test.onmana.space/api/Ticket/{ticketId}?page=-1";
            await page.WaitForResponseAsync(ticketDetailApi);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            do
            {
                try
                {
                    //await page.WaitForSelectorAsync($"text=วันที่", new PageWaitForSelectorOptions { Timeout = 5000 });
                    await page.WaitForSelectorAsync("label:has-text(\"ยังไม่ถูกแก้\")", new PageWaitForSelectorOptions { Timeout = 5000 });
                }
                catch (Exception)
                {
                    break;
                }
                await page.ClickAsync("label:has-text(\"ยังไม่ถูกแก้\")");
                await page.ClickAsync("button[role=\"radio\"]:has-text(\"แก้สำเร็จแล้ว\")");
                await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button:has-text(\"OK\")"), ticketDetailApi);
            } while (true);

            await page.ClickAsync("text=ปิดงาน >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ดำเนินการแก้ไขเรียบร้อยแล้ว");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<bool> SentConsentInfo2User()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

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
            await page.GotoAsync(Pages.Ticket);

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

        public async Task<bool> LogOutTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            await page.ClickAsync("text=จัดการ >> span");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.ClickAsync("text=สั่ง Logout บัญชีนี้ออกจากระบบ manaดำเนินการ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Ticket Logout");
            const string LogOutTicketApi = "https://thman-test.onmana.space/api/User/logout";
            var LogOutTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button >> nth=-1"), LogOutTicketApi);

            if (false == LogOutTicketResponse.Ok)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> FreezeTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.Ticket);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            await page.ClickAsync("text=จัดการ >> span");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=ดำเนินการ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "Ticket Freeze");
            const string FreezeTicketApi = "https://thman-test.onmana.space/api/User/freeze";
            var FreezeTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button >> nth=-1"), FreezeTicketApi);

            if (false == FreezeTicketResponse.Ok)
            {
                return false;
            }
            return true;
        }
    }
}
