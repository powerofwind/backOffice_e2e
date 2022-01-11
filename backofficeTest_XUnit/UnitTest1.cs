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
    }
}
