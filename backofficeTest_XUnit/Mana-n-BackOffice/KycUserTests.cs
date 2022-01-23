using backofficeTest_XUnit.Helpers;
using FluentAssertions;
using manaTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit.Mana_n_BackOffice
{
    public class KycUserTests : TestBase
    {
        [Fact(DisplayName = "ส่งคำขอ KYC basic ได้")]
        [TestPriority(100)]
        public async Task SendRequestKYCBasic()
        {
            var sut = new SetUpProject();
            var res = await sut.SendRequestKYCBasic();
            res.Should().Be(true);
        }
    }
}
