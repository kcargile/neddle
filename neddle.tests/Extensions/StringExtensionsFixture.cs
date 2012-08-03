using System;
using Neddle.Extensions;
using NUnit.Framework;

namespace Neddle.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsFixture
    {
        [Test]
        public void CheckNullOrEmpty()
        {
            string s1 = string.Empty;
            Assert.Throws<ArgumentException>(() => s1.CheckNullOrEmpty("s1"));
            Assert.Throws<ArgumentException>(() => s1.CheckNullOrEmpty());

            s1 = null;
            Assert.Throws<ArgumentNullException>(() => s1.CheckNullOrEmpty("s1"));
            Assert.Throws<ArgumentNullException>(() => s1.CheckNullOrEmpty());
            
            const string s2 = "not empty";
            Assert.DoesNotThrow(() => s2.CheckNullOrEmpty("s2"));
            Assert.DoesNotThrow(() => s2.CheckNullOrEmpty());
        }

        [Test]
        public void Md5Hash()
        {
            // TODO: KLC fix this test

            const string stringToHash = "someimportantstring";
            Assert.AreNotEqual(stringToHash, stringToHash.Md5Hash());
        }
    }
}
