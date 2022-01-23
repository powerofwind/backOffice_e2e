using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace backofficeTest_XUnit.Helpers
{
    public class CollectionOrder : ITestCollectionOrderer
    {
        public IEnumerable<ITestCollection> OrderTestCollections(
            IEnumerable<ITestCollection> testCollections)
        {
            return testCollections.OrderBy(it =>
            {
                var i = it.DisplayName.LastIndexOf(' ');
                if (i <= -1) return 0;

                var className = it.DisplayName.Substring(i + 1);
                var type = Type.GetType(className);
                if (type == null) return 0;

                var attr = type.GetCustomAttribute<TestPriorityAttribute>();
                return attr?.Priority ?? 0;
            });
        }
    }
}
