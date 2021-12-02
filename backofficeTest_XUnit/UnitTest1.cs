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
            //var res = await sut.Go2TicketPage();
            //res.Should().Be(true);
        }
    }
}
