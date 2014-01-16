using System;
using Neddle.Extensions;
using Xunit;

namespace Neddle.Tests.Extensions
{
    public class TypeExtensionsFixture
    {
        [Fact]
        public void SubclassOfRawGeneric()
        {
            Assert.True(typeof(Course).IsSubclassOfRawGeneric(typeof(NeddleObject<>)));
            Assert.False(typeof(Int32).IsSubclassOfRawGeneric(typeof(NeddleObject<>)));
            Assert.False(typeof(IAppDomainSetup).IsSubclassOfRawGeneric(typeof(NeddleObject<>)));
            Assert.Throws<ArgumentNullException>(() => typeof(int).IsSubclassOfRawGeneric(null));
        }
    }
}