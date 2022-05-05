using backofficeTest.Helpers;
using mana_Test.Models;
using Microsoft.Playwright;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class Account
    {
        // สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้
        public async Task<(bool isSuccess, IPage page)> AddPPayAccountByPhoneNumber()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string AccountApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fneaclst-home%22%7D";
            var AccountApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(3) ion-col:nth-child(3) img"), AccountApi);
            if (!AccountApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=พร้อมเพย์");
            await page.GotoAsync("http://localhost:8100/#/account-create-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("input[name=\"ion-input-0\"]", "ppay4PW");
            await page.ClickAsync("label:has-text(\"เบอร์โทรศัพท์\")");
            await page.ClickAsync("button:has-text(\"เบอร์โทรศัพท์\")");
            await page.FillAsync("input[name=\"ion-input-1\"]", "0910167715");

            const string CreateAccountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var CreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateAccountPPayApi);
            if (!CreateAccountPPayApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/account-confirm-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string ComfirmCreateAccountPPayApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22account-confirm-ppay%22,%22triggerName%22:%22Button1%22%7D";
            var ComfirmCreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), ComfirmCreateAccountPPayApi);
            if (!ComfirmCreateAccountPPayApiResponse.Ok)
            {
                return (false, page);
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (true, page);

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
        }

        // สร้างการผูกบัญชีพร้อมเพย์แบบหมายเลขบัตรประชาชนได้
        public async Task<(bool isSuccess, IPage page)> AddPPayAccountByPID()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;

            const string AccountApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fneaclst-home%22%7D";
            var AccountApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(3) ion-col:nth-child(3) img"), AccountApi);
            if (!AccountApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=พร้อมเพย์");
            await page.GotoAsync("http://localhost:8100/#/account-create-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.FillAsync("input[name=\"ion-input-0\"]", "ppay4PW");
            await page.ClickAsync("label:has-text(\"เบอร์โทรศัพท์\")");
            await page.ClickAsync("button:has-text(\"เลขบัตรประชาชน\")");
            await page.FillAsync("input[name=\"ion-input-1\"]", "0910167715123");

            const string CreateAccountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var CreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateAccountPPayApi);
            if (!CreateAccountPPayApiResponse.Ok)
            {
                return (false,page);
            }

            await page.GotoAsync("http://localhost:8100/#/account-confirm-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string ComfirmCreateAccountPPayApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22account-confirm-ppay%22,%22triggerName%22:%22Button1%22%7D";
            var ComfirmCreateAccountPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), ComfirmCreateAccountPPayApi);
            if (!ComfirmCreateAccountPPayApiResponse.Ok)
            {
                return (false, page);
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (true, page);

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
        }

        // สร้างบัญชีธนาคาร
        public async Task<(bool isSuccess, IPage page)> AddBankingAccount(string accName, string accNumber)
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string AccountApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fneaclst-home%22%7D";
            var AccountApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(3) ion-col:nth-child(3) img"), AccountApi);
            if (!AccountApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=บัญชีธนาคาร");
            await page.GotoAsync("http://localhost:8100/#/account-create-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", accName);
            await page.ClickAsync("input[name=\"ion-input-2\"]");
            await page.FillAsync("input[name=\"ion-input-2\"]", accNumber);

            const string CreateAccountBankApi = "https://localhost:44364/mcontent/Submit/";
            var CreateAccountBankApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateAccountBankApi);
            if (!CreateAccountBankApiResponse.Ok)
            {
                return (false, page);
            }

            var resultTask = new TaskCompletionSource<string>();
            page.Dialog += ResultCanUseAccNumberDlg;
            var dialogMessage = await resultTask.Task;

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Fail")
            {
                return (true, page);
            }
            return (false, page);

            void ResultCanUseAccNumberDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                page.Dialog -= ResultCanUseAccNumberDlg;
            }
        }

        public async Task<(bool isSuccess, IPage page)> AddBankingAccountSuccess(string accName, string accNumber)
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string AccountApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fneaclst-home%22%7D";
            var AccountApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(3) ion-col:nth-child(3) img"), AccountApi);
            if (!AccountApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/account-main");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/externalaccount/add/typelist/neaclst-home");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/account-create-select");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=บัญชีธนาคาร");
            await page.GotoAsync("http://localhost:8100/#/account-create-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", accName);
            await page.ClickAsync("input[name=\"ion-input-2\"]");
            await page.FillAsync("input[name=\"ion-input-2\"]", accNumber);

            const string CreateAccountBankApi = "https://localhost:44364/mcontent/Submit/";
            var CreateAccountBankApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateAccountBankApi);
            if (!CreateAccountBankApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/account-confirm-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            //var confirmTask1 = new TaskCompletionSource<IDialog>();
            //var confirmTask2 = new TaskCompletionSource<IDialog>();
            ////await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            //page.Dialog += ConfirmDlg;
            //await page.WaitForTimeoutAsync(2000);
            //page.Dialog += ResultCreateAccountDlg;

            const string ComfirmCreateAccountbamkApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22account-confirm-bankaccount%22,%22triggerName%22:%22Button1%22%7D";
            var ComfirmCreateAccountbamkApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), ComfirmCreateAccountbamkApi);
            if (!ComfirmCreateAccountbamkApiResponse.Ok)
            {
                return (false, page);
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += page_Dialog3_EventHandler;
            await page.WaitForTimeoutAsync(2000);
            page.Dialog += page_Dialog3_EventHandler;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (true, page);

            void page_Dialog3_EventHandler(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= page_Dialog3_EventHandler;
            }
            //return (true, page);

            //void ConfirmDlg(object sender, IDialog dialog)
            //{
            //    dialog.AcceptAsync();
            //    page.Dialog -= ConfirmDlg;
            //    confirmTask1.TrySetResult(dialog);
            //}

            //void ResultCreateAccountDlg(object sender, IDialog dialog)
            //{
            //    dialog.AcceptAsync();
            //    page.Dialog -= ResultCreateAccountDlg;
            //    confirmTask2.TrySetResult(dialog);
            //}
        }
    }
}
