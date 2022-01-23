﻿using Microsoft.Playwright;
using System.Threading.Tasks;

namespace backofficeTest.Helpers
{
    public static class IPageExtensions
    {
        private static volatile TaskCompletionSource<IPage> loginPageTask;

        public static async Task<IPage> DoLogin(this Task<IPage> targetPage)
        {
            if (null == loginPageTask)
            {
                loginPageTask ??= new TaskCompletionSource<IPage>();
                var loginPage = await handleLoginStep();
                loginPageTask.TrySetResult(loginPage);
                return loginPage;
            }
            await loginPageTask.Task;
            return await targetPage;

            async Task<IPage> handleLoginStep()
            {
                var loginPage = await targetPage;
                await loginPage.GotoAsync(Pages.HostUrl);
                const string LoginButtonSelector = "text=Login";
                await loginPage.ClickAsync(LoginButtonSelector);

                var options = new PageWaitForNavigationOptions { UrlString = Pages.Ticket };
                await loginPage.WaitForNavigationAsync(options);
                await loginPage.WaitForLoadStateAsync(LoadState.NetworkIdle);
                var contextStateOptions = new BrowserContextStorageStateOptions
                {
                    Path = PageFactory.StorageStatePath
                };
                await loginPage.Context.StorageStateAsync(contextStateOptions);
                return loginPage;
            }
        }

        private static volatile TaskCompletionSource<IPage> manaLoginPageTask;

        public static async Task<IPage> DoManaLogin(this Task<IPage> targetPage)
        {
            if (null == manaLoginPageTask)
            {
                manaLoginPageTask ??= new TaskCompletionSource<IPage>();
                var loginPage = await handleLoginStep();
                manaLoginPageTask.TrySetResult(loginPage);
                return loginPage;
            }
            await manaLoginPageTask.Task;
            return await targetPage;

            async Task<IPage> handleLoginStep()
            {
                var loginPage = await targetPage;
                await loginPage.GotoAsync("https://localhost:44364");
                await loginPage.WaitForTimeoutAsync(15000);
                //const string LoginButtonSelector = "text=Login";
                //await loginPage.ClickAsync(LoginButtonSelector);

                //await loginPage.WaitForTimeoutAsync(10000);
                //var options = new PageWaitForNavigationOptions { UrlString = "https://localhost:44364" };
                //await loginPage.WaitForNavigationAsync(options);
                //await loginPage.WaitForLoadStateAsync(LoadState.NetworkIdle);
                var contextStateOptions = new BrowserContextStorageStateOptions
                {
                    Path = PageFactory.StorageStatePath,
                };
                await loginPage.Context.StorageStateAsync(contextStateOptions);
                return loginPage;
            }
        }
    }
}
