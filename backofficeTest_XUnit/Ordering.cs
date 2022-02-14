﻿using backofficeTest;
using backofficeTest.Steps;
using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using manaTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit
{
    public class Ordering : TestBase
    {
        /// <summary>
        /// Mana
        /// </summary>
        [Fact(DisplayName = "สร้างบัญชีพร้อมเพย์แบบหมายเลขบัตรประชาชนได้")]
        [TestPriority(10)]
        public async Task AddPPayAccountByPID()
        {
            var sut = new Account();
            var res = await sut.AddPPayAccountByPID();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้างบัญชีพร้อมเพย์แบบหมายเลขโทรศัพท์ได้")]
        [TestPriority(15)]
        public async Task AddPPayAccountByPhoneNumber()
        {
            var sut = new Account();
            var res = await sut.AddPPayAccountByPhoneNumber();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้างบัญชีธนาคารได้")]
        [TestPriority(20)]
        public async Task AddBankingAccount()
        {
            var sut = new Account();
            var res = await sut.AddBankingAccount();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ไม่เคยเติมเงินไม่ได้")]
        [TestPriority(25)]
        public async Task CannotWithdrawPPayNeverTopup()
        {
            var sut = new Withdraw();
            var res = await sut.CannotWithdrawPPayNeverTopup();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ไม่สามารถถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีธนาคารไม่เคยเติมเงินไม่ได้")]
        [TestPriority(30)]
        public async Task CannotWithdrawBankingNeverTopup()
        {
            var sut = new Withdraw();
            var res = await sut.CannotWithdrawBankingNeverTopup();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ส่ง RTP เพื่อขอเติมเงินไปยังพร้อมเพย์ที่ผูกไว้ได้")]
        [TestPriority(35)]
        public async Task TopUpPPay()
        {
            var sut = new Topup();
            var res = await sut.TopUpPPay();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ส่ง RTP เพื่อขอเติมเงินไปยังบัญชีธนาคารที่ผูกไว้ได้")]
        [TestPriority(40)]
        public async Task TopUpbanking()
        {
            var sut = new Topup();
            var res = await sut.TopUpbanking();
            res.Should().Be(true);
        }
        [Fact(DisplayName = "สร้างร้านสำหรับ Business ได้")]
        [TestPriority(45)]
        public async Task CreateBusinessShop()
        {
            var sut = new BusinessShop();
            var res = await sut.CreateBusinessShop();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้าง QR ร้าน Business ได้")]
        [TestPriority(50)]
        public async Task CreatQRBusiness()
        {
            var sut = new BusinessShop();
            var res = await sut.CreatQRBusiness();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินออกจากร้าน Business เข้ากระเป๋าเงิน Mana ได้")]
        [TestPriority(55)]
        public async Task withdrawBusinessShop()
        {
            var sut = new BusinessShop();
            var res = await sut.withdrawBusinessShop();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้")]
        [TestPriority(60)]
        public async Task TopUpCreateQR()
        {
            var sut = new Topup();
            var res = await sut.TopUpCreateQR();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้")]
        [TestPriority(65)]
        public async Task WithdrawPPaySuccess()
        {
            var sut = new Withdraw();
            var res = await sut.WithdrawPPaySuccess();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินจากบัญชีธนาคารที่ผูกไว้ได้")]
        [TestPriority(70)]
        public async Task WithdrawBankingSuccess()
        {
            var sut = new Withdraw();
            var res = await sut.WithdrawBankingSuccess();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ")]
        [TestPriority(75)]
        public async Task NotWithdrawPPayMoneyNotEnough()
        {
            var sut = new Withdraw();
            var res = await sut.NotWithdrawPPayMoneyNotEnough();
            res.Should().Be(true);
        }

        [Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ")]
        [TestPriority(80)]
        public async Task NotWithdrawBankingMoneyNotEnough()
        {
            var sut = new Withdraw();
            var res = await sut.NotWithdrawBankingMoneyNotEnough();
            res.Should().Be(true);
        }

        /// <summary>
        /// BackOffice
        /// </summary>
        [Fact(DisplayName = "(Ticket) เบอร์โทรที่ไม่ได้ลงทะเบียนจะต้องไม่สามารถสร้าง Ticket ได้")]
        [TestPriority(100)]
        public async Task InputUnknowPhoneNoThenCanNotCreateNewTicket()
        {
            var sut = new TicketStep();
            var result = await sut.CreateNewTicket("0000000000", "invalid@email.com", null, null, null);
            result.isSuccess.Should().BeFalse();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) สามารถสร้าง Ticket ที่ยังไม่มีคนรับเรื่องได้")]
        [TestPriority(150)]
        public async Task InputAllValidThenCanCreateNewTicket()
        {
            var sut = new TicketStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewTicket("0910167715", "mana002kku@gmail.com", desc, "1234567890", "expected@gmail.com");

            result.isSuccess.Should().BeTrue();
            var page = result.page;
            await page.GotoAsync(Pages.Ticket);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);

            var ticketDetailApi = $"https://thman-test.onmana.space/api/Ticket/{result.ticketId}?page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync($"ion-card:has-text(\"{result.cardOwnerName}\")"), ticketDetailApi);
            await page.WaitForSelectorAsync($"text={desc}");
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) สามารถกดย้ายงานกลับได้")]
        [TestPriority(200)]
        public async Task TicketInMineCanBeRollback()
        {
            var sut = new TicketStep();
            var result = await sut.RollbackLastestTicket();

            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) สามารถกดรับงานที่ยังไม่มีคนรับได้")]
        [TestPriority(300)]
        public async Task TicketCanBeTaken()
        {
            var sut = new TicketStep();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.Ticket);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ปิด Ticket ที่มี Issue ที่ยังแก้ไม่เสร็จได้")]
        [TestPriority(400)]
        public async Task CloseTicketWithIncompleteStatus()
        {
            var sut = new TicketStep();
            var result = await sut.CloseTicketWithIncompleteStatus();

            var page = result.page;
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Done?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Done\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ทำการ Reopen เพื่อกลับมาแก้ไขปัญหาของงานที่ถูกปิดไปแล้วได้")]
        [TestPriority(500)]
        public async Task ReOpenTicket()
        {
            var sut = new TicketStep();
            var result = await sut.ReOpenTicket();

            var page = result.page;
            await page.GotoAsync(Pages.Ticket);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ปิดงานเมื่อดำเนินการแก้ไขงานสำเร็จได้")]
        [TestPriority(600)]
        public async Task CloseTicketAllCompleteStatus()
        {
            var sut = new TicketStep();
            var result = await sut.CloseTicketAllCompleteStatus();

            var page = result.page;
            const string GetDoneTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Done?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Done\")"), GetDoneTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้")]
        [TestPriority(700)]
        public async Task SentConsentInfo2User()
        {
            var sut = new TicketStep();
            var resultReOpenTicket = await sut.ReOpenTicket();
            var page = resultReOpenTicket.page;
            await page.GotoAsync(Pages.Ticket);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{resultReOpenTicket.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();

            var result = await sut.SentConsentInfo2User();
            result.isSuccess.Should().BeTrue();
            await page.CloseAsync();
        }

        [Fact(DisplayName = "User ปฏิเสธการเข้าถึงข้อมูลได้")]
        [TestPriority(800)]
        public async Task UserRejectInfo()
        {
            var sut = new Consent();
            var result = await sut.UserRejectInfo();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง User ได้")]
        [TestPriority(900)]
        public async Task SentConsentInfo2User2()
        {
            var sut = new TicketStep();
            var result = await sut.SentConsentInfo2User();
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "User อนุมัติการเข้าถึงข้อมูลได้")]
        [TestPriority(1000)]
        public async Task UserApproveInfo()
        {
            var sut = new Consent();
            var result = await sut.UserApproveInfo();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้")]
        [TestPriority(1100)]
        public async Task SentConsentInfo2Manager()
        {
            var sut = new TicketStep();
            var result = await sut.SentConsentInfo2Manager();
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager ปฏิเสธการเข้าถึงข้อมูลได้")]
        [TestPriority(1200)]
        public async Task ManagerRejectInfo()
        {
            var sut = new Consent();
            var result = await sut.ManagerRejectInfo();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ขอ Consent ข้อมูลธุรกรรมไปยัง Manager ได้")]
        [TestPriority(1300)]
        public async Task SentConsentInfo2Manager2()
        {
            var sut = new TicketStep();
            var result = await sut.SentConsentInfo2Manager();
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager อนุมัติการเข้าถึงข้อมูลได้")]
        [TestPriority(1400)]
        public async Task ManagerApproveInfo()
        {
            var sut = new Consent();
            var result = await sut.ManagerApproveInfo();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ไม่สามารถสร้าง Ticket ที่มีคนรับเรื่องอยู่แล้วได้")]
        [TestPriority(1450)]
        public async Task CanNotCreateNewTicketWhenItAlreadyTaken()
        {
            var sut = new TicketStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewTicket("0910167715", "mana002kku@gmail.com", desc, "1234567890", "expected@gmail.com");
            result.isSuccess.Should().BeFalse();
            var isTicketExist = result.page.Url.Contains("detail");
            isTicketExist.Should().BeTrue();
        }

        [Fact(DisplayName = "(Ticket) ส่งคำขอการระงับบัญชี User ได้")]
        [TestPriority(1500)]
        public async Task SentConsent4FreezeTicket()
        {
            var sut = new TicketStep();
            var desc = Guid.NewGuid().ToString();
            var resultCreateNewTicket = await sut.CreateNewTicket("0914185400", "mana003kku@gmail.com", desc, "1234567890", "expected@gmail.com");
            resultCreateNewTicket.isSuccess.Should().BeTrue();
            var page = resultCreateNewTicket.page;
            await page.GotoAsync(Pages.Ticket);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Ticket/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{resultCreateNewTicket.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);

            var ticketDetailApi = $"https://thman-test.onmana.space/api/Ticket/{resultCreateNewTicket.ticketId}?page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync($"ion-card:has-text(\"{resultCreateNewTicket.cardOwnerName}\")"), ticketDetailApi);
            await page.WaitForSelectorAsync($"text={desc}");
            await resultCreateNewTicket.page.CloseAsync();

            var result = await sut.FreezeTicket();
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager ปฏิเสธการระงับบัญชีได้")]
        [TestPriority(1600)]
        public async Task ManagerRejectSuspendAccount()
        {
            var sut = new Consent();
            var result = await sut.ManagerRejectSuspendAccount();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Ticket) ส่งคำขอการระงับบัญชี User ได้")]
        [TestPriority(1700)]
        public async Task SentConsent4FreezeTicket2()
        {
            var sut = new TicketStep();
            var result = await sut.FreezeTicket();
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager อนุมัติการระงับบัญชีได้")]
        [TestPriority(1800)]
        public async Task ManagerApproveSuspendAccount()
        {
            var sut = new Consent();
            var result = await sut.ManagerApproveSuspendAccount();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Frozen) สามารถกดรับงานได้สำเร็จ")]
        [TestPriority(1900)]
        public async Task TicketCanBeTakenInFrozen()
        {
            var sut = new FrozenStep();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.Frozen);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Frozen/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Frozen) สามารถกดย้ายงานกลับได้")]
        [TestPriority(2000)]
        public async Task TicketInMineCanBeRollbackInFrozen()
        {
            var sut = new FrozenStep();
            var result = await sut.RollbackLastestTicket();

            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Frozen) ส่งคำขอยกเลิกการระงับบัญชี User ได้")]
        [TestPriority(2100)]
        public async Task SentConsent4UnFreezeTicket()
        {
            var sut = new FrozenStep();
            var resultTakeLastestTicket = await sut.TakeLastestTicket();
            var pageTakeLastestTicket = resultTakeLastestTicket.page;
            await pageTakeLastestTicket.GotoAsync(Pages.Frozen);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Frozen/list/Mine?search=&page=-1";
            await pageTakeLastestTicket.RunAndWaitForResponseAsync(() => pageTakeLastestTicket.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelectorTakeLastestTicket = $"ion-card > a[href*=\"{resultTakeLastestTicket.ticketId}\"]";
            await pageTakeLastestTicket.WaitForSelectorAsync(targetTicketSelectorTakeLastestTicket);
            await pageTakeLastestTicket.CloseAsync();

            var result = await sut.UnFreezeTicket();
            result.isUnFreeze.Should().BeTrue();
            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager ปฏิเสธการยกเลิกการระงับบัญชีได้")]
        [TestPriority(2200)]
        public async Task ManagerRejectCancelSuspendAccount()
        {
            var sut = new Consent();
            var result = await sut.ManagerRejectCancelSuspendAccount();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Frozen) ทำการ Reopen บัญชีที่ถูกระงับที่สถานะยังไม่ผ่านได้")]
        [TestPriority(2300)]
        public async Task ReOpenTicketInFrozen()
        {
            var sut = new FrozenStep();
            var result = await sut.ReOpenTicket();

            var page = result.page;
            await page.GotoAsync(Pages.Frozen);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Frozen/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Frozen) ส่งคำขอยกเลิกการระงับบัญชี User ได้")]
        [TestPriority(2400)]
        public async Task SentConsent4UnFreezeTicket2()
        {
            var sut = new FrozenStep();
            var result = await sut.UnFreezeTicket();

            result.isUnFreeze.Should().BeTrue();
            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "Manager อนุมัติการยกเลิกการระงับบัญชีได้")]
        [TestPriority(2500)]
        public async Task ManagerApproveCancelSuspendAccount()
        {
            var sut = new Consent();
            var result = await sut.ManagerApproveCancelSuspendAccount();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        // Mana
        [Fact(DisplayName = "ส่งคำขอ KYC basic ได้")]
        [TestPriority(2600)]
        public async Task SendRequestKYCBasic()
        {
            var sut = new SetUpProject();
            var result = await sut.SendRequestKYCBasic();
            result.isSuccess.Should().Be(true);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(User(PA)) สามารถกดรับงานได้สำเร็จ")]
        [TestPriority(2700)]
        public async Task TicketCanBeTakenInUserPA()
        {
            var sut = new User_PA_Step();
            var result = await sut.TakeLastestTicket();

            var page = result.page;
            await page.GotoAsync(Pages.User);
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Kyc/list/Mine?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(User(PA)) สามารถกดย้ายงานกลับได้")]
        [TestPriority(2800)]
        public async Task TicketInMineCanBeRollbackInUserPA()
        {
            var sut = new User_PA_Step();
            var result = await sut.RollbackLastestTicket();

            var page = result.page;
            await page.WaitForSelectorAsync("ion-card");
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            var qry = await page.QuerySelectorAllAsync(targetTicketSelector);
            qry.Count.Should().Be(0);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(User(PA)) อนุมัติคำขอ KYC ได้")]
        [TestPriority(2900)]
        public async Task CloseTicketWithIncompleteStatusInUserPA()
        {
            var sut = new User_PA_Step();
            var resultTakeLastestTicket = await sut.TakeLastestTicket();
            var pageTakeLastestTicket = resultTakeLastestTicket.page;
            await pageTakeLastestTicket.GotoAsync(Pages.User);
            const string GetMineTicketApiTakeLastestTicket = "https://thman-test.onmana.space/api/Kyc/list/Mine?search=&page=-1";
            await pageTakeLastestTicket.RunAndWaitForResponseAsync(() => pageTakeLastestTicket.ClickAsync("ion-segment-button:has-text(\"Mine\")"), GetMineTicketApiTakeLastestTicket);
            var targetTicketSelectorTakeLastestTicket = $"ion-card > a[href*=\"{resultTakeLastestTicket.ticketId}\"]";
            await pageTakeLastestTicket.WaitForSelectorAsync(targetTicketSelectorTakeLastestTicket);
            await pageTakeLastestTicket.CloseAsync();

            var result = await sut.ApproveUserKYC();
            var page = result.page;
            const string GetMineTicketApi = "https://thman-test.onmana.space/api/Kyc/list/Done?search=&page=-1";
            await page.RunAndWaitForResponseAsync(() => page.ClickAsync("ion-segment-button:has-text(\"Done\")"), GetMineTicketApi);
            var targetTicketSelector = $"ion-card > a[href*=\"{result.ticketId}\"]";
            await page.WaitForSelectorAsync(targetTicketSelector);
            await page.CloseAsync();
        }

        [Fact(DisplayName = "(Fraud) ไม่สามารถสร้าง fraud โดยใช้เลขบัตรประชาชนที่ไม่เคยผ่านการทำ KYC ได้")]
        [TestPriority(3000)]
        public async Task InputPaIdWithNoKycThenCanNotCreate()
        {
            var sut = new FraudStep();
            var result = await sut.CreateNewFraud("0000000000000", null);
            result.validatePaId.Should().BeFalse();
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Fraud) สร้าง fraud โดยใช้เลขบัตรประชาชนที่ผ่านการ KYC ได้")]
        [TestPriority(3100)]
        public async Task InputPaIdWithKycThenCanCreate()
        {
            var sut = new FraudStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewFraud("1349900417203", desc);

            result.validatePaId.Should().BeTrue();
            var content = await result.page.ContentAsync();
            content.Should().Contain(desc);
            await result.page.CloseAsync();
        }

        [Fact(DisplayName = "(Fraud) สามารถกดย้ายงานกลับได้")]
        [TestPriority(3200)]
        public async Task TicketInMineCanBeRollbackInFraud()
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
        [TestPriority(3300)]
        public async Task TicketCanBeTakenInFraud()
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

        [Fact(DisplayName = "(Fraud) สั่ง Logout user ได้")]
        [TestPriority(3400)]
        public async Task ForceUserLogout()
        {
            var sut = new FraudStep();
            var result = await sut.LogOutTicket();
            result.isSuccess.Should().BeTrue();
            await result.page.CloseAsync();
        }
    }
}
