using Microsoft.Playwright;
using System.Threading.Tasks;

namespace backofficeTest.Helpers
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
        /// <returns>Testable page.</returns>
        public static async Task<IPage> CreatePage(float? slomotion = null)
        {
            playwright ??= await Playwright.CreateAsync();
            if (null == contextTask)
            {
                contextTask = new TaskCompletionSource<IBrowserContext>();
                IBrowserContext browserContext = null;
                try
                {
                    var browser = await playwright.Chromium
                                .LaunchAsync(new BrowserTypeLaunchOptions
                                {
                                    Headless = false,
                                    SlowMo = slomotion,
                                });
                    var contextOptions = new BrowserNewContextOptions
                    {
                        StorageStatePath = StorageStatePath
                    };
                    browserContext = await browser.NewContextAsync(contextOptions);
                }
                catch (System.Exception)
                {
                    browserContext = await playwright.Chromium
                        .LaunchPersistentContextAsync(nameof(PageFactory), new BrowserTypeLaunchPersistentContextOptions
                        {
                            Headless = false,
                            SlowMo = slomotion,
                        });
                    var firstPage = await browserContext.NewPageAsync();
                    var contextStateOptions = new BrowserContextStorageStateOptions
                    {
                        Path = PageFactory.StorageStatePath,
                    };
                    await firstPage.Context.StorageStateAsync(contextStateOptions);
                }
                contextTask.TrySetResult(browserContext);
            }

            var context = await contextTask.Task;
            return await context.NewPageAsync();
        }
    }
}
