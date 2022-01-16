using backofficeTest;
using backofficeTest.Steps;
using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Tests
{
    public class TicketTests : TestBase
    {
        [Fact(DisplayName = "(Ticket) เบอร์โทรที่ไม่ได้ลงทะเบียนจะต้องไม่สามารถสร้าง Ticket ได้")]
        public async Task InputUnknowPhoneNoThenCanNotCreateNewTicket()
        {
            var sut = new TicketStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewTicket("0000000000", "invalid@email.com", desc, null, null);

            result.isSuccess.Should().BeFalse();
            var content = await result.page.ContentAsync();
            content.Should().NotContain(desc);
        }

        [Fact(DisplayName = "(Ticket) สามารถสร้าง Ticket ที่ยังไม่มีคนรับเรื่องได้")]
        [TestPriority(100)]
        public async Task InputAllValidThenCanCreateNewTicket()
        {
            var sut = new TicketStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewTicket("0914185400", "mana003kku@gmail.com", desc, "1234567890", "expected@gmail.com");

            result.isSuccess.Should().BeTrue();
            var content = await result.page.ContentAsync();
            content.Should().Contain(desc);
        }

        [Fact(DisplayName = "(Ticket) สามารถกดย้ายงานกลับได้")]
        [TestPriority(200)]
        public async Task TicketInMineCanBeRollback()
        {
            var sut = new TicketStep();
            var result = await sut.RollbackLastestTicket();

            var content = await result.page.ContentAsync();
            content.Should().Contain(result.cardOwnerName);
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
            var content = await page.ContentAsync();
            content.Should().Contain(result.cardOwnerName);
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
            var content = await page.ContentAsync();
            content.Should().Contain(result.cardOwnerName);
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
            var content = await page.ContentAsync();
            content.Should().Contain(result.cardOwnerName);
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
            var content = await page.ContentAsync();
            content.Should().Contain(result.cardOwnerName);
        }

        // TODO: มี 2 กรณี
        // 1.เราเป็นคนรับงานของผู้ใช้นี้อยู่แล้ว
        // 2.มีคนอื่นรับไปแล้ว
        //[Fact(DisplayName = "(Ticket) ไม่สามารถสร้าง Ticket ที่มีคนรับเรื่องอยู่แล้วได้")]
        //[TestPriority(2)]
        //public async Task CanNotCreateNewTicketWhenItAlreadyTaken()
        //{
        //    var sut = new TicketStep();
        //    var desc = Guid.NewGuid().ToString();
        //    var result = await sut.CreateNewTicket("0914185400", "mana003kku@gmail.com", desc, "1234567890", "expected@gmail.com");
        //    result.isSuccess.Should().BeFalse();
        //    var content = await result.page.ContentAsync();
        //    content.Should().NotContain(desc);
        //}
    }
}
