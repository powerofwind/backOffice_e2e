using Microsoft.Playwright;
using System.Linq;
using System.Threading.Tasks;

namespace backofficeTest
{
    public class PageBuilder
    {
        private IPlaywright playwright;
        private IBrowserContext browserContext;
        private TaskCompletionSource<IPage> loginTask;
        private const string LaunchContextName = "backoffice";

        private static PageBuilder instance;
        public static PageBuilder Instance => instance ??= new PageBuilder();

        public async Task<IPage> Build(float? slowmotion = null, bool forceLogin = false)
        {
            playwright ??= await Playwright.CreateAsync();
            browserContext ??= await playwright.Chromium
                    .LaunchPersistentContextAsync(LaunchContextName, new BrowserTypeLaunchPersistentContextOptions
                    {
                        Headless = false,
                        SlowMo = slowmotion,
                    });
            return await CreateLoginPage(forceLogin);
        }

        protected async Task<IPage> CreateLoginPage(bool forceLogin)
        {
            if (null == loginTask || forceLogin)
            {
                loginTask ??= new TaskCompletionSource<IPage>();
                if (forceLogin || false == await hasAuthentication())
                {
                    await handleLoginStep();
                }
                loginTask.TrySetResult(null);
            }

            await loginTask.Task;
            return await browserContext.NewPageAsync();

            async Task<bool> hasAuthentication()
            {
                const string SaveCookiePath = "backoffice.json";
                var storageOptions = new BrowserContextStorageStateOptions { Path = SaveCookiePath };
                var cookieJson = await browserContext.StorageStateAsync(storageOptions);
                return cookieJson != "{\"cookies\":[],\"origins\":[]}" && cookieJson.Contains(".AspNetCore.OpenIdConnect");
            }
            async Task handleLoginStep()
            {
                var page = browserContext.Pages.FirstOrDefault() ?? await browserContext.NewPageAsync();
                await page.GotoAsync(Pages.HostUrl);
                var waitFor = new PageRunAndWaitForNavigationOptions { UrlString = Pages.Ticket };
                await page.RunAndWaitForNavigationAsync(() => page.ClickAsync("text=Login"), waitFor);
                await hasAuthentication();
            }
        }
    }
}
