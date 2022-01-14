using backofficeTest;
using FluentAssertions;
using Xunit;

namespace backofficeTest_XUnit
{
    public class AllPagesTests
    {
        [Fact]
        public async void FrozenPageCanBeOpenProperly()
        {
            var page = await PageBuilder.Instance.Build();
            await page.GotoAsync(Pages.Frozen);
            page.Url.Should().Be(Pages.Frozen);
        }

        [Fact]
        public async void FraudPageCanBeOpenProperly()
        {
            var page = await PageBuilder.Instance.Build();
            await page.GotoAsync(Pages.Fraud);
            page.Url.Should().Be(Pages.Fraud);
        }

        [Fact]
        public async void TicketPageCanBeOpenProperly()
        {
            var page = await PageBuilder.Instance.Build();
            await page.GotoAsync(Pages.Ticket);
            page.Url.Should().Be(Pages.Ticket);
        }
    }
}
