using backofficeTest.Helpers;
using mana_Test.Models;
using Microsoft.Playwright;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class Topup
    {
        
        // ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้
        public async Task<(bool isSuccess, IPage page)> TopUpPPay()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string TopupApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltdep-home%22%7D";
            var TopupApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img"), TopupApi);
            if (!TopupApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=0910167715");
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string TopupPPayApi = "https://localhost:44364/mcontent/Submit/";
            var TopupPPayApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), TopupPPayApi);
            if (!TopupPPayApiResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();
            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/wallet-topup-ppay-confirm");
            page.Dialog += ConfirmDlg;
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
                dialog.AcceptAsync("25.00");
                page.Dialog -= InputMoneyDlg;
            }

            void ConfirmDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= ConfirmDlg;
                confirmTask.TrySetResult(dialog);
            }

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                page.Dialog -= ResultDlg;
            }
        }

        // สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้
        public async Task<(bool isSuccess, IPage page)> TopUpCreateQR()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string TopupApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltdep-home%22%7D";
            var TopupApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img"), TopupApi);
            if (!TopupApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=สร้างคิวอาร์โค้ดเพื่อเติมเงิน");
            await page.GotoAsync("http://localhost:8100/#/wallet-topup-qr-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string TopupBankApi = "https://localhost:44364/mcontent/Submit/";
            var TopupBankApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), TopupBankApi);
            if (!TopupBankApiResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();
            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/wallet-topup-qr-confirm");
            page.Dialog += ConfirmDlg;
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
                dialog.AcceptAsync("20.00");
                page.Dialog -= InputMoneyDlg;
            }

            void ConfirmDlg(object sender, IDialog dialog)
            {
                dialog.AcceptAsync();
                page.Dialog -= ConfirmDlg;
                confirmTask.TrySetResult(dialog);
            }

            void ResultDlg(object sender, IDialog dialog)
            {
                resultTask.TrySetResult(dialog.Message);
                page.Dialog -= ResultDlg;
            }
        }

        // ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้
        public async Task<(bool isSuccess, IPage page)> TopUpbanking()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nfinanc-home");

            await page.GotoAsync("http://localhost:8100/#/financial-menu");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string TopupApi = "https://localhost:44364/mcontent/VisitEndpoint/%7B%22mcid%22:%22financial-menu%22,%22url%22:%22https%3A%2F%2Fs.manal.ink%2Fnp%2Fnwltdep-home%22%7D";
            var TopupApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-row:nth-child(2) ion-col:nth-child(1) img"), TopupApi);
            if (!TopupApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/wallet-topup");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=0123456789123");


            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string TopupBankApi = "https://localhost:44364/mcontent/Submit/";
            var TopupBankApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), TopupBankApi);
            if (!TopupBankApiResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();
            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/wallet-topup-bankaccount-confirm");
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
                dialog.AcceptAsync("30.00");
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
    }
}
