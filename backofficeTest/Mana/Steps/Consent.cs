using backofficeTest.Helpers;
using mana_Test.Models;
using Microsoft.Playwright;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class Consent
    {
        // User อนุมัติการเข้าถึงข้อมูลได้
        public async Task<(bool isSuccess, IPage page)> UserApproveInfo()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-user");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var ConfirmConsentResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!ConfirmConsentResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }

        // User ปฏิเสธการเข้าถึงข้อมูลได้
        public async Task<(bool isSuccess, IPage page)> UserRejectInfo()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-user");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var ConfirmConsentResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!ConfirmConsentResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }

        // Manager อนุมัติการเข้าถึงข้อมูลได้
        public async Task<(bool isSuccess, IPage page)> ManagerApproveInfo()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }

        // Manager ปฏิเสธการเข้าถึงข้อมูลได้
        public async Task<(bool isSuccess, IPage page)> ManagerRejectInfo()
        {

            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขออนุญาติเข้าถึงข้อมูลผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-userinfo-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }

        // Manager อนุมัติการระงับบัญชีได้
        public async Task<(bool isSuccess, IPage page)> ManagerApproveSuspendAccount()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");
            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-freezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }

        // Manager ปฏิเสธการระงับบัญชีได้
        public async Task<(bool isSuccess, IPage page)> ManagerRejectSuspendAccount()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-freezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }

        // Manager อนุมัติการยกเลิกการระงับบัญชีได้
        public async Task<(bool isSuccess, IPage page)> ManagerApproveCancelSuspendAccount()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอยกเลิกระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-unfreezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"อนุญาต\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }

        // Manager ปฏิเสธการยกเลิกการระงับบัญชีได้
        public async Task<(bool isSuccess, IPage page)> ManagerRejectCancelSuspendAccount()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=test-home-feed");

            await page.GotoAsync("http://localhost:8100/#/home-feed");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("ion-row:has-text(\"mana official 3 daขอยกเลิกระงับบัญชีของผู้ใช้งาน\")");
            await page.GotoAsync("http://localhost:8100/#/consent-unfreezing-by-manager");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("label:has-text(\"ปฏิเสธ\")");

            var resultTask = new TaskCompletionSource<string>();

            page.Dialog += ResultDlg;
            const string ConfirmConsentApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ConfirmConsentApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var dialogMessage = await resultTask.Task;
            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                dialog.AcceptAsync();
                page.Dialog -= ResultDlg;
            }
        }
    }
}
