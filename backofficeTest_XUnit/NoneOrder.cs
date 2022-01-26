using backofficeTest.Steps;
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
    public class NoneOrder
    {
        // TODO: มี 2 กรณี
        // 1.เราเป็นคนรับงานของผู้ใช้นี้อยู่แล้ว
        // 2.มีคนอื่นรับไปแล้ว
        //[Fact(DisplayName = "(Ticket) ไม่สามารถสร้าง Ticket ที่มีคนรับเรื่องอยู่แล้วได้")]
        //public async Task CanNotCreateNewTicketWhenItAlreadyTaken()
        //{
        //    var sut = new TicketStep();
        //    var desc = Guid.NewGuid().ToString();
        //    //var result = await sut.CreateNewTicket("0910167715", "mana002kku@gmail.com", desc, "1234567890", "expected@gmail.com");
        //    var result = await sut.CreateNewTicket("0632130913", "tachapong999@gmail.com", desc, "1234567890", "expected@gmail.com");
        //    result.isSuccess.Should().BeFalse();
        //    var isTicketExist = result.page.Url.Contains("detail");
        //    isTicketExist.Should().BeTrue();
        //}

        //[Fact(DisplayName = "(Fraud) ไม่สามารถสร้าง fraud โดยใช้เลขบัตรประชาชนที่ไม่เคยผ่านการทำ KYC ได้")]
        //public async Task InputPaIdWithNoKycThenCanNotCreate()
        //{
        //    var sut = new FraudStep();
        //    var result = await sut.CreateNewFraud("0000000000000", null);
        //    result.validatePaId.Should().BeFalse();
        //}

        //[Fact(DisplayName = "(Ticket) เบอร์โทรที่ไม่ได้ลงทะเบียนจะต้องไม่สามารถสร้าง Ticket ได้")]
        //public async Task InputUnknowPhoneNoThenCanNotCreateNewTicket()
        //{
        //    var sut = new TicketStep();
        //    var result = await sut.CreateNewTicket("0000000000", "invalid@email.com", null, null, null);
        //    result.isSuccess.Should().BeFalse();
        //    await result.page.CloseAsync();
        //}

        ///  Mana
        //[Fact(DisplayName = "แจ้งปัญหาไปยังทีม Support ได้")]
        //public async Task ReportIssue()
        //{
        //    var sut = new SetUpProject();
        //    var res = await sut.ReportIssue();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "สร้างร้านสำหรับ Business ได้")]
        //public async Task CreateBusinessShop()
        //{
        //    var sut = new BusinessShop();
        //    var res = await sut.CreateBusinessShop();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "สร้าง QR ร้าน Business ได้")]
        //public async Task CreatQRBusiness()
        //{
        //    var sut = new BusinessShop();
        //    var res = await sut.CreatQRBusiness();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "ถอนเงินออกจากร้าน Business เข้ากระเป๋าเงิน Mana ได้")]
        //public async Task withdrawBusinessShop()
        //{
        //    var sut = new BusinessShop();
        //    var res = await sut.withdrawBusinessShop();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "สร้าง QR เพื่อเติมเงินเข้ากระเป๋าเงิน Mana ได้")]
        //public async Task TopUpCreateQR()
        //{
        //    var sut = new Topup();
        //    var res = await sut.TopUpCreateQR();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "ถอนเงินจากพร้อมเพย์ที่ผูกไว้ได้")]
        //public async Task WithdrawPPaySuccess()
        //{
        //    var sut = new Withdraw();
        //    var res = await sut.WithdrawPPaySuccess();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "ถอนเงินจากบัญีธนาคารที่ผูกไว้ได้")]
        //public async Task WithdrawBankingSuccess()
        //{
        //    var sut = new Withdraw();
        //    var res = await sut.WithdrawBankingSuccess();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน Mana ผ่านบัญชีพร้อมเพย์ที่ผูกไว้ไม่ได้ เพราะเงินในบัญชีไม่พอ")]
        //public async Task NotWithdrawPPayMoneyNotEnough()
        //{
        //    var sut = new Withdraw();
        //    var res = await sut.NotWithdrawPPayMoneyNotEnough();
        //    res.Should().Be(true);
        //}

        //[Fact(DisplayName = "ถอนเงินออกจากกระเป๋าเงิน mana ผ่านบัญชีธนาคารที่ผูกไว้ไม่ได้ เพราะเงินไม่พอ")]
        //public async Task NotWithdrawBankingMoneyNotEnough()
        //{
        //    var sut = new Withdraw();
        //    var res = await sut.NotWithdrawBankingMoneyNotEnough();
        //    res.Should().Be(true);
        //}
    }
}
