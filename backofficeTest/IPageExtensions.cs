using Microsoft.Playwright;
using System.Threading.Tasks;

namespace backofficeTest
{
    public static class IPageExtensions
    {
        private static volatile TaskCompletionSource<IPage> loginPageTask;

        public static async Task<IPage> Login(this Task<IPage> targetPage)
        {
            if (null == loginPageTask)
            {
                loginPageTask ??= new TaskCompletionSource<IPage>();
                //var loginPage = await hasAuthentication() ? await targetPage : await handleLoginStep();
                var loginPage = await handleLoginStep();
                loginPageTask.TrySetResult(loginPage);
                return loginPage;
            }

            var page = await loginPageTask.Task;
            return await page.Context.NewPageAsync();

            //async Task<bool> hasAuthentication()
            //{
            //    var page = await targetPage;
            //    var SaveCookiePath = $"{nameof(PageFactory)}.json";
            //    var storageOptions = new BrowserContextStorageStateOptions { Path = SaveCookiePath };
            //    var cookieJson = await page.Context.StorageStateAsync(storageOptions);
            //    return cookieJson != "{\"cookies\":[],\"origins\":[]}" && cookieJson.Contains(".AspNetCore.OpenIdConnect");
            //}
            async Task<IPage> handleLoginStep()
            {
                var loginPage = await targetPage;
                await loginPage.GotoAsync(Pages.HostUrl);

                const string LoginButtonSelector = "text=Login";
                await loginPage.ClickAsync(LoginButtonSelector);

                var options = new PageWaitForNavigationOptions { UrlString = Pages.Ticket };
                await loginPage.WaitForNavigationAsync(options);
                await loginPage.WaitForLoadStateAsync(LoadState.NetworkIdle);
                return loginPage;
            }
        }
    }
}
