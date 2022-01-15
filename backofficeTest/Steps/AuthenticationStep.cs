using Microsoft.Playwright;
using System.Threading.Tasks;

namespace backofficeTest.Steps
{
    public class AuthenticationStep
    {
        public async Task<IPage> Login()
        {
            var page = await PageFactory.CreatePage().DoLogin();
            return page;
        }

        public async Task<IPage> GotoAfterLogin(string pageName)
        {
            var page = await Login();
            await page.GotoAsync(pageName);
            return page;
        }
    }
}
