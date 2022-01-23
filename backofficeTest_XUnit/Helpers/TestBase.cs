using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer("backofficeTest_XUnit.Helpers.CollectionOrder", "backofficeTest_XUnit")]

namespace backofficeTest_XUnit.Helpers
{
    [TestCaseOrderer("backofficeTest_XUnit.Helpers.PriorityOrderer", "backofficeTest_XUnit")]
    public class TestBase
    {
    }
}
