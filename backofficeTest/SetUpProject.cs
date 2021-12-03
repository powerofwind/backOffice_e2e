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
                    SlowMo = 2000,
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
            //page = await browser.NewPageAsync();
            //await page.GotoAsync("https://thman-test.onmana.space/");
            //await page.ClickAsync("text=Login");
            //await page.WaitForTimeoutAsync(10000);
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/ticket");
            var result = page.Url;
            return result;
        }

        public async Task<string> Go2TicketPage2()
        {
            var browser = await BeforeScenario();
            //page = await browser.NewPageAsync();
            //await page.GotoAsync("https://thman-test.onmana.space/");
            //await page.ClickAsync("text=Login");
            //await page.WaitForTimeoutAsync(10000);
            await page.GotoAsync("https://thman-test.onmana.space/app/index.html#/fraud");
            var result = page.Url;
            return result;
        }


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
