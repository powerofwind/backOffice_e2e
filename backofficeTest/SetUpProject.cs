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
        private IPage page;

        public async Task<IBrowser> BeforeScenario()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 20000
            });
            return browser;
        }

        public async Task<bool> Go2TicketPage()
        {
            var browser = await BeforeScenario();
            page = await browser.NewPageAsync();
            await page.GotoAsync("https://thman-test.onmana.space/");
            await page.ClickAsync("text=Login");
            await page.WaitForTimeoutAsync(10000);
            var result = page.Url.Should().EndWith("ticket");
            //var res = result == "ticket" ? true : false;
            //return res;
        }

        //public async Task<bool> AdminCreateRiderFinanceSuccess()
        //{
        //    var browser = await BeforeScenario();
        //    page = await browser.NewPageAsync();
        //    await page.GotoAsync("https://delivery-3rd-admin.azurewebsites.net/#/finance");
        //    await page.WaitForTimeoutAsync(2000);
        //    var first = await page.QuerySelectorAllAsync("ion-card");
        //    var countFisrt = first.Count();
        //    await page.ClickAsync("text=เพิ่มรายการ >> span");
        //    await page.ClickAsync("text=1 637495843238686175 637496490865877251 637496493490656290 637498504432042681 63 >> button");
        //    await page.ClickAsync("button[role=\"radio\"]:has-text(\"1\")");
        //    await page.ClickAsync("button:has-text(\"OK\")");
        //    await page.FillAsync("input[name=\"ion-input-0\"]", "1500");
        //    await page.FillAsync("input[name=\"ion-input-1\"]", "รายเดือน");
        //    await page.ClickAsync("text=ยืนยัน >> button");
        //    await page.WaitForTimeoutAsync(2000);
        //    var seccond = await page.QuerySelectorAllAsync("ion-card");
        //    var countSeccond = seccond.Count();
        //    var result = countSeccond - countFisrt;
        //    var res = result == 1 ? true : false;
        //    return res;
        //}
    }
}
