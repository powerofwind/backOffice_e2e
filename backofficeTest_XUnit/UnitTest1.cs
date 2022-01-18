using System;
using Xunit;
using backofficeTest;
using System.Threading.Tasks;
using FluentAssertions;

namespace backofficeTest_XUnit
{
    public class UnitTest1
    {
        [Fact(DisplayName = "เข้าหน้า Ticket ได้")]
        public async Task Test1()
        {
            var sut = new SetUpProject();
            var res = await sut.Go2TicketPage();
            res.Should().EndWith("ticket");
        }

        [Fact(DisplayName = "เข้าหน้า Fraud ได้")]
        public async Task Test2()
        {
            var sut = new SetUpProject();
            var res = await sut.Go2TicketPage2();
            res.Should().EndWith("fraud");
        }

        [Fact(DisplayName = "สามารถสร้าง Ticket ที่ยังไม่มีคนรับเรื่องได้")]
        public async Task CreateTicketSuccess()
        {
            var sut = new SetUpProject();
            var res = await sut.CreateTicketSuccess();
            Assert.Equal("ติดต่อมานะต้องทำยังไง", res);
        }

        //[Fact(DisplayName = "ไม่สามารถสร้าง Ticket ที่มีคนรับเรื่องอยู่แล้วได้")]
        //public async Task CreateTicketFail()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.CreateTicketFail();
        //    Assert.Equal(true, res);
        //}

        [Fact(DisplayName = "(Ticket) ปิดงานเมื่อดำเนินการแก้ไขงานสำเร็จได้")]
        public async Task CloseTicket()
        {
            var sut = new SetUpProject();
            var res = await sut.CloseTicket();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Ticket) สามารถกดย้ายงานกลับได้")]
        public async Task RollBack()
        {
            var sut = new SetUpProject();
            var res = await sut.RollBack();
            Assert.Equal(true, res);
        }

        //[Fact(DisplayName = "(Ticket) ทำการ Reopen เพื่อกลับมาแก้ไขปัญหาของงานที่ถูกปิดไปแล้วได้")]
        //public async Task ReOpenTicket()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.ReOpenTicket();
        //    Assert.Equal(true, res);
        //}

        [Fact(DisplayName = "(Ticket) Admin - สั่ง Logout user")]
        public async Task TicketForceLogout()
        {
            var sut = new SetUpProject();
            var res = await sut.TicketForceLogout();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Ticket) ส่งคำขอการระงับบัญชี User ได้")]
        public async Task TicketForceFrozen()
        {
            var sut = new SetUpProject();
            var res = await sut.TicketForceFrozen();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้")]
        public async Task TicketUserConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.TicketUserConsent();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้")]
        public async Task TicketManagerConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.TicketManagerConsent();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Frozen) ส่งคำขอยกเลิกการระงับบัญชี User ได้")]
        public async Task UnFrozen()
        {
            var sut = new SetUpProject();
            var res = await sut.UnFrozen();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Frozen) ขอ Consent ข้อมูลผู้ใช้ไปยัง User ได้")]
        public async Task FrozenUserConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.FrozenUserConsent();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Frozen) ขอ Consent ข้อมูลผู้ใช้ไปยัง Manager ได้")]
        public async Task FrozenManagerConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.FrozenManagerConsent();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Fraud) สั่ง Logout user ได้")]
        public async Task FraudForceLogout()
        {
            var sut = new SetUpProject();
            var res = await sut.FraudForceLogout();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Fraud) ส่งคำขอการระงับบัญชี User ได้")]
        public async Task FraudForceFrozen()
        {
            var sut = new SetUpProject();
            var res = await sut.FraudForceFrozen();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Fraud) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้")]
        public async Task FraudUserConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.FraudUserConsent();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(Fraud) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้")]
        public async Task FraudManagerConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.FraudUserConsent();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(User(KYC)) สั่ง Logout user ได้")]
        public async Task UserKYCForceLogout()
        {
            var sut = new SetUpProject();
            var res = await sut.UserKYCForceLogout();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(User(KYC)) ส่งคำขอการระงับบัญชี User ได้")]
        public async Task UserKYCForceFrozen()
        {
            var sut = new SetUpProject();
            var res = await sut.UserKYCForceFrozen();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(User(KYC)) ขอ Consent ข้อมูล KYC ไปยัง User ได้")]
        public async Task UserKYCUserConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.UserKYCUserConsent();
            Assert.Equal(true, res);
        }

        [Fact(DisplayName = "(User(KYC)) ขอ Consent ข้อมูล KYC ไปยัง Manager ได้")]
        public async Task UserKYCManagerConsent()
        {
            var sut = new SetUpProject();
            var res = await sut.UserKYCManagerConsent();
            Assert.Equal(true, res);
        }
    }
