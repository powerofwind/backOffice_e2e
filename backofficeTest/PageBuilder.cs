using Microsoft.Playwright;
using System.Threading.Tasks;

namespace backofficeTest
{
    public class PageBuilder
    {
        private IBrowser browser;
        private IPlaywright playwright;
        private IBrowserContext browserContext;
        private TaskCompletionSource<IPage> loginTask;

        private static PageBuilder instance;

        public static PageBuilder Instance => instance ??= new PageBuilder();

        public async Task<IPage> Build(float? slowmotion = 1000, float? loginTimeout = 60000)
        {
            await Initialize(slowmotion);
            return await CreateLoginPage(loginTimeout);
        }

        protected async Task<IBrowserContext> Initialize(float? slowmotion)
        {
            playwright ??= await Playwright.CreateAsync();
            browser ??= await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = slowmotion,
            });
            return browserContext ??= await browser.NewContextAsync();
        }

        protected async Task<IPage> CreateLoginPage(float? loginTimeout)
        {
            if (null == loginTask)
            {
                loginTask = new TaskCompletionSource<IPage>();
                var page = await browserContext.NewPageAsync();
                await page.GotoAsync(Pages.HostUrl);
                const string LoginButtonSelector = "text=Login";
                await page.ClickAsync(LoginButtonSelector);
                await page.WaitForNavigationAsync(new PageWaitForNavigationOptions
                {
                    Timeout = loginTimeout,
                    UrlString = Pages.Ticket,
                });
                loginTask.TrySetResult(page);
                await page.CloseAsync();
            }

            await loginTask.Task;
            return await browserContext.NewPageAsync();
        }
    }
}
