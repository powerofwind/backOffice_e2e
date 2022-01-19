using E2E.Shared;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace manaTest
{
    public static class IPageExtensions
    {
        private static volatile TaskCompletionSource<IPage> manaLoginPageTask;

        public static async Task<IPage> DoLogin(this Task<IPage> targetPage)
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
                await loginPage.GotoAsync(Pages.HostUrl);
                await loginPage.WaitForTimeoutAsync(15000);
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
