using backofficeTest;
using backofficeTest.Steps;
using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Tests
{
    public class FraudTests : TestBase
    {
        [Fact(DisplayName = "(Fraud) ไม่สามารถสร้าง fraud โดยใช้เลขบัตรประชาชนที่ไม่เคยผ่านการทำ KYC ได้")]
        public async Task InputPaIdWithNoKycThenCanNotCreate()
        {
            var sut = new FraudStep();
            var result = await sut.CreateNewFraud("0000000000000", null);
            result.validatePaId.Should().BeFalse();
        }
        
        [Fact(DisplayName = "(Fraud) สร้าง fraud โดยใช้เลขบัตรประชาชนที่ผ่านการ KYC ได้")]
        [TestPriority(100)]
        public async Task InputPaIdWithKycThenCanCreate()
        {
            var sut = new FraudStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewFraud("1100200407594", desc);

            result.validatePaId.Should().BeTrue();
            var content = await result.page.ContentAsync();
            content.Should().Contain(desc);
        }

        [Fact(DisplayName = "(Fraud) สามารถกดย้ายงานกลับได้")]
        [TestPriority(200)]
        public async Task TicketInMineCanBeRollback()
        {
            var sut = new FraudStep();
            var result = await sut.RollbackLastestTicket();

            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Fraud) สามารถกดรับงานได้สำเร็จ")]
        [TestPriority(300)]
        public async Task TicketCanBeTaken()
        {
            var sut = new FraudStep();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.Fraud);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Fraud/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);

            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Fraud) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้")]
        [TestPriority(400)]
        public async Task SentConsentInfo2User()
        {
            var sut = new FraudStep();
            var result = await sut.SentConsentInfo2User();
            result.Should().BeTrue();
        }

        //TODO: MANA APP manager DENY CONSENT
        //TODO: MANA APP manager APPROVE CONSENT**

        [Fact(DisplayName = "(Fraud) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้")]
        [TestPriority(600)]
        public async Task SentConsentInfo2Manager()
        {
            var sut = new FraudStep();
            var result = await sut.SentConsentInfo2Manager();
            result.Should().BeTrue();
        }

        //TODO: MANA APP manager DENY CONSENT**
        //TODO: MANA APP manager APPROVE CONSENT

        [Fact(DisplayName = "(Fraud) ส่งคำขอการระงับบัญชี User ได้")]
        [TestPriority(800)]
        public async Task SentConsent4FreezeTicket()
        {
            var sut = new FraudStep();
            var result = await sut.FreezeTicket();
            result.Should().BeTrue();
        }

        //TODO: MANA APP manager DENY CONSENT
        //TODO: MANA APP manager APPROVE CONSENT
        //Wait FreezonTest

        [Fact(DisplayName = "(Fraud) สั่ง Logout user ได้")]
        [TestPriority(1000)]
        public async Task ForceUserLogout()
        {
            var sut = new FraudStep();
            var result = await sut.LogOutTicket();
            result.Should().BeTrue();
        }

        //ปิดงาน

    }
}
