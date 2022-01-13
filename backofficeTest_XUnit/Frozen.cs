using backofficeTest;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit
{
    public class Frozen
    {
        [Fact(DisplayName = "(Frozen) สามารถกดย้ายงานกลับได้")]
        public async Task RollBack()
        {
            var sut = new SetUpProject();
            var res = await sut.RollBack("https://thman-test.onmana.space/app/index.html#/frozen");
            res.Should().Be(true);
        }

        [Fact(DisplayName = "(Frozen) ทำการ Reopen บัญชีที่ถูกระงับ ที่สถานะยังไม่ผ่านได้")]
        public async Task ReOpenTicket()
        {
            var sut = new SetUpProject();
            var res = await sut.ReOpenTicket("https://thman-test.onmana.space/app/index.html#/frozen", "frozen");
            Assert.Equal(true, res);
        }
    }
}
