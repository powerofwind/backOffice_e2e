using backofficeTest.Helpers;
using mana_Test.Models;
using Microsoft.Playwright;
using System.Text.Json;
using System.Threading.Tasks;

namespace manaTest
{
    public class SetUpProject
    {
        // แจ้งปัญหาไปยังทีม Support ได้
        public async Task<bool> ReportIssue()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/np/nrptdtl-create");
            await page.GotoAsync("http://localhost:8100/#/report-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var dialogMessage = string.Empty;
            await page.ClickAsync("text=ประเภทของปัญหา, เลือกประเภท");
            await page.ClickAsync("button[role=\"radio\"]:has-text(\"การเงิน\")");
            await page.ClickAsync("button:has-text(\"OK\")");
            await page.ClickAsync("textarea[name=\"ion-textarea-0\"]");
            await page.FillAsync("textarea[name=\"ion-textarea-0\"]", "test E2E แจ้งปัญหาการเงิน2");
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "0632132623");
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.FillAsync("input[name=\"ion-input-1\"]", "tatae@gmail.com");
            page.Dialog += page_Dialog1_EventHandler;
            await page.ClickAsync("text=OK >> button");
            await page.WaitForTimeoutAsync(5000);

            var result = JsonSerializer.Deserialize<ResultDlg>(dialogMessage);
            if (result.status == "Success")
            {
                return true;
            }
            return false;

            void page_Dialog1_EventHandler(object sender, IDialog dialog)
            {
                dialogMessage = dialog.Message;
                dialog.DismissAsync();
                page.Dialog -= page_Dialog1_EventHandler;
            }
        }

        // ส่งคำขอ KYC basic
        public async Task<(bool isSuccess, IPage page)> SendRequestKYCBasic()
        {
            var page = await PageFactory.CreatePage().DoManaLogin();
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/kyc/basic/visit/nkycbsc-180056522489857");
            var dialogMessage = string.Empty;
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            //await page.GotoAsync("http://localhost:8100/#/kyc-agreement");
            //await page.ClickAsync("button");
            await page.GotoAsync("https://localhost:44364/dev/visit?url=https://s.manal.ink/kyc/basic/create/nkycbsc-180056522489857 ");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.GotoAsync("http://localhost:8100/#/kyc-basic-create");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("input[name=\"ion-input-0\"]");
            await page.FillAsync("input[name=\"ion-input-0\"]", "เตชะพงศ์");
            await page.ClickAsync("input[name=\"ion-input-1\"]");
            await page.FillAsync("input[name=\"ion-input-1\"]", "ขำคม");
            await page.ClickAsync("input[name=\"ion-input-2\"]");
            await page.FillAsync("input[name=\"ion-input-2\"]", "1349900417203");
            await page.ClickAsync("button");
            ////// วันเกิด
            await page.ClickAsync("text=14");
            await page.ClickAsync("text=13");
            await page.ClickAsync("text=11");
            await page.ClickAsync("text=09");
            await page.ClickAsync("text=07");
            await page.ClickAsync("text=05");
            await page.ClickAsync("text=03");
            await page.ClickAsync("text=02");
            ////////เดือนเกิด
            await page.ClickAsync(":nth-match(ion-picker-column:has-text(\"010203040506070809101112\"), 2)");
            await page.ClickAsync(":nth-match(:text(\"02\"), 2)");
            await page.ClickAsync(":nth-match(:text(\"03\"), 2)");
            await page.ClickAsync(":nth-match(:text(\"05\"), 2)");
            await page.ClickAsync(":nth-match(:text(\"06\"), 2)");
            ////////ปีเกิด
            await page.ClickAsync("text=2020");
            await page.ClickAsync("text=2019");
            await page.ClickAsync("text=2018");
            await page.ClickAsync("text=2017");
            await page.ClickAsync("text=2016");
            await page.ClickAsync("text=2015");
            await page.ClickAsync("text=2014");
            await page.ClickAsync("text=2013");
            await page.ClickAsync("text=2012");
            await page.ClickAsync("text=2011");
            await page.ClickAsync("text=2010");
            await page.ClickAsync("text=2009");
            await page.ClickAsync("text=2008");
            await page.ClickAsync("text=2007");
            await page.ClickAsync("text=2006");
            await page.ClickAsync("text=2005");
            await page.ClickAsync("text=2004");
            await page.ClickAsync("text=2003");
            await page.ClickAsync("text=2002");
            await page.ClickAsync("text=2001");
            await page.ClickAsync("text=2000");
            await page.ClickAsync("text=1999");
            await page.ClickAsync("text=1998");
            await page.ClickAsync("text=1997");
            await page.ClickAsync("text=1996");
            await page.ClickAsync("text=1995");
            await page.ClickAsync("text=1994");
            await page.ClickAsync("text=1993");
            await page.ClickAsync("text=1992");
            await page.ClickAsync("text=1991");
            await page.ClickAsync("text=Done");
            await page.ClickAsync("input[name=\"ion-input-3\"]");
            await page.FillAsync("input[name=\"ion-input-3\"]", "ME1123387863");
            await page.ClickAsync("text=ระบุที่อยู่ตามบัตร ปชช.");
            var page1 = await PageFactory.CreatePage().DoManaLogin();
            await page1.GotoAsync("http://localhost:8100/#/kyc-add-address");
            await page1.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page1.ClickAsync("input[name=\"ion-input-0\"]");
            await page1.FillAsync("input[name=\"ion-input-0\"]", "8/2");
            await page1.ClickAsync("input[name=\"ion-input-1\"]");
            await page1.FillAsync("input[name=\"ion-input-1\"]", "ในเมือง");
            await page1.ClickAsync("input[name=\"ion-input-2\"]");
            await page1.FillAsync("input[name=\"ion-input-2\"]", "เมือง");
            await page1.ClickAsync("input[name=\"ion-input-3\"]");
            await page1.FillAsync("input[name=\"ion-input-3\"]", "อุบลราชธานี");
            await page1.ClickAsync("input[name=\"ion-input-4\"]");
            await page1.FillAsync("input[name=\"ion-input-4\"]", "34000");
            await page1.ClickAsync("input[name=\"ion-input-5\"]");
            await page1.FillAsync("input[name=\"ion-input-5\"]", "0632130558");
            await page1.ClickAsync("button");

            await page.ClickAsync("text=ตรวจสอบเบอร์โทรศัพท์ของคุณ");
            var page2 = await PageFactory.CreatePage().DoManaLogin();
            await page2.GotoAsync("http://localhost:8100/#/kyc-tel-confirm");
            await page2.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page2.ClickAsync("input[name=\"ion-input-1\"]");
            await page2.FillAsync("input[name=\"ion-input-1\"]", "0910167715");
            await page2.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page2.ClickAsync("button");
            await page2.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page.ClickAsync("text=OK >> button");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await page2.ClickAsync("button");

            const string CreateKYCApi = "https://localhost:44364/mcontent/Submit/";
            var CreateKYCApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("text=OK >> button"), CreateKYCApi);
            if (!CreateKYCApiResponse.Ok)
            {
                return (false, page);
            }

            await page.GotoAsync("http://localhost:8100/#/kyc-basic-confirm");
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            const string ComfirmCreateKYCApi = "https://localhost:44364/mcontent/CallTrigger/%7B%22mcid%22:%22kyc-basic-confirm%22,%22triggerName%22:%22Button1%22%7D";
            var ComfirmCreateKYCApiResponse = await page.RunAndWaitForResponseAsync(() => page.ClickAsync("button"), ComfirmCreateKYCApi);
            if (ComfirmCreateKYCApiResponse.Ok)
            {
                await page.WaitForTimeoutAsync(20000);
                return (true, page);
            }
            return (false, page);
        }
    }
}
