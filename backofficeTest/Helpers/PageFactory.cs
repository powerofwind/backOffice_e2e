using Microsoft.Playwright;
using System.Linq;
using System.Threading.Tasks;

namespace backofficeTest.Helpers
{
    public static class PageFactory
    {
        private volatile static IPlaywright playwright;
        private volatile static IBrowserContext browserContext;

        /// <summary>
        /// Creates a new page.
        /// </summary>
        /// <param name="slomotion">Slows down Playwright operations by the specified amount of milliseconds. Useful so that you can see what is going on.</param>
        /// <returns>Testable page.</returns>
        public static async Task<IPage> CreatePage(float? slomotion = null)
        {
            playwright ??= await Playwright.CreateAsync();
            browserContext ??= await playwright.Chromium
                .LaunchPersistentContextAsync(nameof(PageFactory),
                new BrowserTypeLaunchPersistentContextOptions
                {
                    Headless = false,
                    SlowMo = slomotion,
                });
            return browserContext.Pages.FirstOrDefault();
        }
    }
}
