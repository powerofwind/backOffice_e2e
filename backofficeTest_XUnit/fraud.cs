﻿using backofficeTest;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backofficeTest_XUnit
{
    public class fraud
    {
        [Fact(DisplayName = "สร้าง fraud ไม่เคย kyc")]
        public async Task CreateFraudNoKYC()
        {
            var sut = new SetUpProject();
            var res = await sut.CreateFraudNoKYC();
            res.Should().Be(true);
        }


        [Fact(DisplayName = "สร้าง fraud เคย kyc")]
        public async Task CreateFraud()
        {
            var sut = new SetUpProject();
            var res = await sut.CreateFraud();
            res.Should().Be(true);
        }

    }
}
