using FluentAssertions;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backofficeTest
{
    public class SetUpProject
    {
        private static IPage page;
        private static IBrowser browser;
        private static IPlaywright playwright;

        public async Task<IBrowser> BeforeScenario()
        {
            playwright ??= await Playwright.CreateAsync();
            if (null == browser)
            {
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    SlowMo = 1000,
                });
                page = await browser.NewPageAsync();
                await page.GotoAsync("https://thman-test.onmana.space/");
                await page.ClickAsync("text=Login");
                await page.WaitForTimeoutAsync(10000);
            }
            return browser;
        }

        public async Task<string> Go2TicketPage()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
            var result = page.Url;
            return result;
        }

        public async Task<string> Go2TicketPage2()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/fraud");
            var result = page.Url;
            return result;
        }

        public async Task<string> CreateTicketSuccess()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
            await page.ClickAsync("text=เพิ่ม >> span");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "0914185400");
            await page.ClickAsync("text=ตรวจสอบข้อมูล");
            await page.ClickAsync("input[name=\"ion-input-3\"]");
            await page.FillAsync("input[name=\"ion-input-3\"]", "mana003kku@gmail.com");
            await page.ClickAsync(":nth-match(:text(\"ตรวจสอบข้อมูล\"), 5)");
            await page.ClickAsync("text=บันทึก >> span");

            //กรอกข้อมูล ticket
            await page.ClickAsync("text=ต้องระบุ, ประเภทปัญหา");
            await page.ClickAsync("button[role=\"radio\"]:has-text(\"อื่นๆ\")");
            await page.ClickAsync("button:has-text(\"ตกลง\")");
            await page.ClickAsync("textarea[name=\"ion-textarea-0\"]");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ติดต่อมานะต้องทำยังไง");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "0914185400");
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.FillAsync("input[name=\"ion-input-1\"]", "mana003kku@gmail.com");
            await page.ClickAsync("text=สร้าง >> button");
            await page.WaitForTimeoutAsync(3000);

            //เช็คว่าเพิ่ม ticket ได้ไหม
            var result = await page.InnerTextAsync("text=ติดต่อมานะต้องทำยังไง");
            return result;
        }

        public async Task<bool> CreateTicketFail()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
            await page.ClickAsync("text=เพิ่ม >> span");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "0914185400");
            await page.ClickAsync("text=ตรวจสอบข้อมูล");
            await page.ClickAsync("input[name=\"ion-input-3\"]");
            await page.FillAsync("input[name=\"ion-input-3\"]", "mana003kku@gmail.com");
            await page.ClickAsync(":nth-match(:text(\"ตรวจสอบข้อมูล\"), 5)");
            await page.ClickAsync("text=บันทึก >> span");

            //เช็คว่ามี ticket อยู่แล้วไหม
            var seccond = await page.QuerySelectorAllAsync("ion-item:nth-child");
            var countSeccon = seccond.Count();
            //var result = countSeccon - countFisrt;
            //var res = result == 1 ? true : false;
            return true;
        }


        public async Task<bool> CloseTicket()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");

            //เช็คว่ามี ticket อยู่แล้วไหม
            var seccond = await page.QuerySelectorAllAsync("ion-item:nth-child");
            var countSeccon = seccond.Count();
            //var result = countSeccon - countFisrt;
            //var res = result == 1 ? true : false;
            return true;
        }

        public async Task<int> move()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
            await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");

            //ย้ายงานกลับ
            await page.WaitForTimeoutAsync(3000);
            await page.ClickAsync("ion-card:nth-child(4)");
            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.ClickAsync("textarea[name=\"ion-textarea-0\"]");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "ย้ายงานกลับ");
            await page.WaitForTimeoutAsync(3000);
            await page.ClickAsync("text=บันทึก");

            //เช็คจำนวน ticket ที่แท็ป open ticket
            await page.ClickAsync("ion-segment-button:has-text(\"Open Ticket\")");
            var ticket = await page.QuerySelectorAllAsync("ion-card:nth-child");
            var ticketCount = ticket.Count();
            //var result = countSeccon - countFisrt;
            //var res = result == 1 ? true : false;

            var result = await page.InnerTextAsync("text=รายงานปัญหา");
            return ticketCount;
        }

        public async Task<(bool done, bool open)> ReOpenTicket()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
            await page.ClickAsync("ion-segment-button:has-text(\"Open Ticket\")");
            var openTicket = await page.QuerySelectorAllAsync("ion-card");
            var openTicketCount = openTicket.Count();
            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");
            var done = await page.QuerySelectorAllAsync("ion-card");
            var doneCount = done.Count();
            //await page.ClickAsync("ion-card:nth-child(1)");
            await page.ClickAsync("ion-card:first-child");
            await page.WaitForTimeoutAsync(3000);
            await page.ClickAsync("text=Pencil Reopen ticket >> button");
            await page.ClickAsync("textarea[name=\"ion-textarea-0\"]");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "reopen");
            await page.WaitForTimeoutAsync(3000);
            await page.ClickAsync("text=บันทึก >> span");

            //ย้ายงานกลับ
            await page.WaitForTimeoutAsync(3000);
            await page.ClickAsync("text=Return Up Back ย้ายกลับ >> button");
            await page.ClickAsync("textarea[name=\"ion-textarea-1\"]");
            await page.FillAsync("textarea[name=\"ion-textarea-1\"]", "ย้ายงานกลับ");
            await page.WaitForTimeoutAsync(3000);
            await page.ClickAsync("button >> nth=-1");

            //เช็คจำนวน ticket ที่แท็ป open ticket/done
            await page.ClickAsync("ion-segment-button:has-text(\"Open Ticket\")");
            var openTicket2 = await page.QuerySelectorAllAsync("ion-card");
            var openTicketCount2 = openTicket2.Count();
            await page.ClickAsync("ion-segment-button:has-text(\"Done\")");
            var done2 = await page.QuerySelectorAllAsync("ion-card");
            var doneCount2 = done2.Count();

            var doneTicket = doneCount - doneCount2;
            var doneResult = doneTicket == 1 ? true : false;

            var openTicketRes = openTicketCount2 - openTicketCount;
            var openTicketResult = openTicketRes == 1 ? true : false;

            return (doneResult, openTicketResult);
        }

        //public async Task<int> move()
        //{
        //    var browser = await BeforeScenario();
        //    await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
        //    await page.ClickAsync("ion-segment-button:has-text(\"Mine\")");
        //    var ticket = await page.QuerySelectorAllAsync("ion-card");
        //    var openTicketCount = ticket.Count();
        //    //var result = countSeccon - countFisrt;
        //    //var res = result == 1 ? true : false;

        public async Task<bool> CreateFraudNoKYC()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/fraud");
            await page.ClickAsync("text=เพิ่ม >> span");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "1100200407555");
            await page.ClickAsync("text=ตรวจสอบข้อมูล");
            await page.ClickAsync("text=ตกลง");
            return true;

        }

        public async Task<bool> CreateFraud()
        {
            var browser = await BeforeScenario();
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/fraud");
            await page.ClickAsync("text=เพิ่ม >> span");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "1100200407594");
            await page.ClickAsync("text=ตรวจสอบข้อมูล");
            await page.ClickAsync("text=บันทึก >> button");
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
