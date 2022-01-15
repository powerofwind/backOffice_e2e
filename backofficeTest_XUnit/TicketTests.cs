using backofficeTest.Steps;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit
{
    public class TicketTests
    {
        [Fact(DisplayName = "(Ticket) เบอร์โทรที่ไม่ได้ลงทะเบียนจะต้องไม่สามารถสร้าง Ticket ได้")]
        public async Task InputUnknowPhoneNoThenCanNotCreateNewTicket()
        {
            var sut = new TicketStep();
            var result = await sut.CreateNewTicket("0000000000", "invalid@email.com", null, null, null);
            result.isSuccess.Should().BeFalse();
        }

        [Fact(DisplayName = "(Ticket) สามารถสร้าง Ticket ที่ยังไม่มีคนรับเรื่องได้")]
        public async Task InputAllValidThenCanCreateNewTicket()
        {
            var sut = new TicketStep();
            var desc = Guid.NewGuid().ToString();
            var result = await sut.CreateNewTicket("0914185400", "mana003kku@gmail.com", desc, "1234567890", "expected@gmail.com");
            result.isSuccess.Should().BeTrue();
            var content = await result.page.ContentAsync();
            content.Should().Contain(desc);
        }
    }
}
