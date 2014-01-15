using System;
using NUnit.Framework;
using Neddle.Extensions;

namespace Neddle.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsFixture
    {
        [Test]
        public void SubclassOfRawGeneric()
        {
            Assert.True(typeof(Course).IsSubclassOfRawGeneric(typeof(NeddleObject<>)));
            Assert.False(typeof(Int32).IsSubclassOfRawGeneric(typeof(NeddleObject<>)));
            Assert.False(typeof(IAppDomainSetup).IsSubclassOfRawGeneric(typeof(NeddleObject<>)));
            Assert.Throws<ArgumentNullException>(() => typeof(int).IsSubclassOfRawGeneric(null));
        }
    }
}
