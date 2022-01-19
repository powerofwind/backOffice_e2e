using Microsoft.Playwright;
using System.Threading.Tasks;

namespace E2E.Shared
{
    public static class PageFactory
    {
        public static string StorageStatePath = "state.json";
        private volatile static IPlaywright playwright;
        private volatile static TaskCompletionSource<IBrowserContext> contextTask;

        /// <summary>
        /// Creates a new page.
        /// </summary>
        /// <param name="slomotion">Slows down Playwright operations by the specified amount of milliseconds. Useful so that you can see what is going on.</param>
        /// <param name="headless">Whether to run browser in headless mode.</param>
        /// <returns>Testable page.</returns>
        public static async Task<IPage> CreatePage(float? slomotion = null, bool headless = false)
        {
            playwright ??= await Playwright.CreateAsync();
            if (null == contextTask)
            {
                contextTask = new TaskCompletionSource<IBrowserContext>();
                var browser = await playwright.Chromium
                    .LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = headless,
                        SlowMo = slomotion,
                    });
                var contextOptions = new BrowserNewContextOptions
                {
                    StorageStatePath = StorageStatePath
                };
                var browserContext = await browser.NewContextAsync(contextOptions);
                contextTask.TrySetResult(browserContext);
            }

            var context = await contextTask.Task;
            return await context.NewPageAsync();
        }
    }
}
