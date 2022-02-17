using backofficeTest.Helpers;
using mana_Test.Models;
using Microsoft.Playwright;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class BusinessShop
    {
   
        // สร้างร้านสำหรับ Business ได้
        public async Task<(bool isSuccess, IPage page)> CreateBusinessShop()
        {

            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-create$shop");

            await page.GotoAsync("http://localhost:8100/#/merchant-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "testE2Eshop889900");
            page.Dialog += page_Dialog1_EventHandler;
            await page.ClickAsync("button");
            await page.WaitForTimeoutAsync(5000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return (true, page);
            }
            return (false, page);

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }

        }

        // ถอนเงินออกจากร้าน Business เข้ากระเป๋าเงิน Mana ได้
        public async Task<(bool isSuccess, IPage page)> withdrawBusinessShop()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-637623474056077116$basic$shop");

            await page.GotoAsync("http://localhost:8100/#/merchant-home-basic");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            await page.ClickAsync("text=ถอนเงิน");
            await page.GotoAsync("http://localhost:8100/#/merchant-withdraw");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            page.Dialog += InputMoneyDlg;
            await page.ClickAsync("input[name=\"ion-input-1\"]");

            const string WithdrawAmountBusinessApi = "https://localhost:44364/mcontent/Submit/";
            var WithdrawAmountBusinessApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), WithdrawAmountBusinessApi);
            if (!WithdrawAmountBusinessApiResponse.Ok)
            {
                return (false, page);
            }

            var confirmTask = new TaskCompletionSource<IDialog>();
            var resultTask = new TaskCompletionSource<string>();

            await page.GotoAsync("http://localhost:8100/#/merchant-withdraw-confirm");
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
                dialog.AcceptAsync("1.00");
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

        // สร้าง QR ร้าน Business ได้
        public async Task<(bool isSuccess, IPage page)> CreatQRBusiness()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nbizdtl-637623474056077116$basic$shop");

            await page.GotoAsync("http://localhost:8100/#/merchant-home-basic");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("text=คิวอาร์รับเงิน");
            await page.GotoAsync("http://localhost:8100/#/merchant-qr-receive-money");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            return (true, page);
        }
    }
}
