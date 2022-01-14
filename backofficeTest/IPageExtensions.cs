using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace backofficeTest
{
    public static class IPageExtensions
    {
        public static async Task Test(this Task<IPage> pageTask, Func<IPage, Task> action)
        {
            var page = await pageTask;
            await action(page);
            await page.CloseAsync();
        }
    }
}
