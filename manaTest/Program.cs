using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace manaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        //public static async Task Main()
        //{
        //    using var playwright = await Playwright.CreateAsync();
        //    await using var browser = await playwright.Chromium.LaunchAsync();
        //    var page = await browser.NewPageAsync();
        //    await page.GotoAsync("https://playwright.dev/dotnet");
        //    await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });
        //}
    }
}
