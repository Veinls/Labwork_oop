using Xunit;
using labvork_1_csharp.Models;
using labvork_1_csharp.Data;
using labvork_1_csharp.Services.Implementations;
using labvork_1_csharp.Services.Implementations.Courses;

namespace labvork_1_tests_csharp
{
    public class DataContextTest
    {
        [Fact]
        public void DataContext_ShouldBeSingleton()
        {
            var instance1 = InMemoryDataContext.Instance;
            var instance2 = InMemoryDataContext.Instance;

            Assert.Same(instance1, instance2);
        }
    }
}