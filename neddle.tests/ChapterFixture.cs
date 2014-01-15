using System;
using NUnit.Framework;

namespace Neddle.Tests
{
    [TestFixture]
    public class ChapterFixture : NeddleObjectFixture<Chapter>
    {
        internal override Chapter MockEntity
        {
            get { throw new NotImplementedException(); }
        }

        [Test]
        public override void Equals()
        {
            Assert.Fail();
        }

        [Test]
        public override void Validate()
        {
            Assert.Fail();
        }
    }
}
