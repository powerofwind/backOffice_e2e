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
    public class User_KYC_
    {
        [Fact(DisplayName = "(User (KYC)) สามารถกดย้ายงานกลับได้")]
        public async Task RollBack()
        {
            var sut = new SetUpProject();
            var res = await sut.RollBack("https://thman-test.onmana.space/app/index.html#/user");
            res.Should().Be(true);
        }
    }
}
