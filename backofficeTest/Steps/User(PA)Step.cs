﻿using backofficeTest.Helpers;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class User_PA_Step
    {
        public async Task<(IPage page, string ticketId)> TakeLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.User);

            await page.ClickAsync("ion-card:last-child button");
            const string AcceptTicketApi = "https://thman-test.onmana.space/api/user/getoperatorinfo";
            var acceptTicketResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-card:last-child button"), AcceptTicketApi);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.WaitForURLAsync($"{Pages.User}/detail/{ticketId}");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> RollbackLastestTicket()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.User);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-card:last-child");

            await page.WaitForTimeoutAsync(300);
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "User(PA) ย้ายงานกลับ");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForURLAsync(Pages.User);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }

        public async Task<(IPage page, string ticketId)> ApproveUserKYC()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            await page.GotoAsync(Pages.User);

            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
            await page.ClickAsync("ion-card:last-child");
            var ticketId = page.Url.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            var ticketDetailApi = $"https://thman-test.onmana.space/api/Kyc/{ticketId}?page=-1";
            await page.WaitForResponseAsync(ticketDetailApi);
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.ClickAsync("text=อนุมัติ >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ข้อมูลถูกต้อง");
            await page.ClickAsync("button >> nth=-1");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (page, ticketId);
        }
    }
}
