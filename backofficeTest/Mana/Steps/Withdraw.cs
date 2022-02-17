using backofficeTest.Helpers;
using mana_Test.Models;
using Microsoft.Playwright;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class Withdraw
    {        
        // ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้
        public async Task<(bool isSuccess, IPage page)> WithdrawPPaySuccess()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawPPayByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476419455827%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=pprora0910167715"), WithdrawPPayByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountPPayApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();
            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            page.Dialog += ConfrimDlg;
            await page.ClickAsync("button");
            await confirmTask.Task;
            page.Dialog += ResultDlg;
            var dialogMessage = await resultTask.Task;

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void InputMoneyDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("1.00");
                page.Dialog -= InputMoneyDlg;
            }

            void ConfrimDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= ConfrimDlg;
                confirmTask.TrySetResult(dialog);

            }

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                page.Dialog -= ResultDlg;
            }
        }
      

        // ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้ 
        public async Task<(bool isSuccess, IPage page)> WithdrawBankingSuccess()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476860532877%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=bank0123456789123"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountbankApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountbankApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();
            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            page.Dialog += ConfrimDlg;
            await page.ClickAsync("button");
            await confirmTask.Task;
            page.Dialog += ResultDlg;
            var dialogMessage = await resultTask.Task;

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void InputMoneyDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("1.00");
                page.Dialog -= InputMoneyDlg;
            }

            void ConfrimDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= ConfrimDlg;
                confirmTask.TrySetResult(dialog);
            }

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                page.Dialog -= ResultDlg;
            }
        }

        // ถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ
        public async Task<(bool isSuccess, IPage page)> NotWithdrawPPayMoneyNotEnough()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawPPayByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476419455827%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=pprora0910167715"), WithdrawPPayByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return (false, page);
            }


            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountPPayApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += ResultDlg;
            await page.ClickAsync("button");
            var dialogMessage = await resultTask.Task;

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Fail")
            {
                return (true, page);
            }
            return (false, page);
            ;

            void InputMoneyDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("4500.00");
                page.Dialog -= InputMoneyDlg;
            }

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                page.Dialog -= ResultDlg;

            }
        }

        // ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ
        public async Task<(bool isSuccess, IPage page)> NotWithdrawBankingMoneyNotEnough()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fwallet%2Fwithdraw%2Fhome%2Fnwltwit-637623476860532877%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=bank0123456789123"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountPPayApi = "https://localhost:44364/mcontent/Submit/";
            var AmountSubmitResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), WithdrawAmountPPayApi);
            if (!AmountSubmitResponse.Ok)
            {
                return (false, page);
            }

            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw-bankaccount-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += ResultDlg;
            await page.ClickAsync("button");
            var dialogMessage = await resultTask.Task;

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Fail")
            {
                return (true, page);
            }
            return (false, page);
            ;

            void InputMoneyDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync("4500.00");
                page.Dialog -= InputMoneyDlg;
            }

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                page.Dialog -= ResultDlg;

            }
        }


        // ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้
        public async Task<(bool isSuccess, IPage page)> CannotWithdrawPPayNeverTopup()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += ConfirmDlg;
            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fdlg%2Fconditional%2Finfo%2Fnwltwit-637777394584866804%3Faptx%3DGoToActivateExternalAccountFlow%26nextnp%3Dwallet%2Fdeposit%2Fppay%2Frequest%2Fnwltdep-637777394584866804%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=ppteste220632138965"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (true, page);

            void ConfirmDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= ConfirmDlg;
                confirmTask.TrySetResult(dialog);
            }
        }

        // ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีธนาคารไม่เคยเติมเงินไม่ได้
        public async Task<(bool isSuccess, IPage page)> CannotWithdrawBankingNeverTopup()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string CreateWithdrawApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltwit-home%22%7D";
            var WithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(3) img"), CreateWithdrawApi);
            if (!WithdrawResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            const string WithdrawBankByIDApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22wallet-withdraw%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fdlg%2Fconditional%2Finfo%2Fnwltwit-637777395161877564%3Faptx%3DGoToActivateExternalAccountFlow%26nextnp%3Dwallet%2Fdepositqr%2Facc%2Frequest%2Fnwltdep-637777395161877564%22%7D";
            var WalletWithdrawResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=bankteste224520342438"), WithdrawBankByIDApi);
            if (!WalletWithdrawResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();

            page.Dialog += ConfirmDlg;
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (true, page);

            void ConfirmDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= ConfirmDlg;
                confirmTask.TrySetResult(dialog);
            }
        }

    }
}

