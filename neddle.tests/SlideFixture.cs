using System;
using NUnit.Framework;

namespace Neddle.Tests
{
    [TestFixture]
    public class SlideFixture : NeddleObjectFixture<Slide>
    {
        internal override Slide MockEntity
        {
            get { throw new NotImplementedException(); }
        }

        [Test]
        public override void Equals()
        {
            Assert.Fail();
        }

        public override void Validate()
        {
            Assert.Fail();
        }
    }
}
