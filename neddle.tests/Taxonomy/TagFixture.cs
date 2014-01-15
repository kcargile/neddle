using System;
using Neddle.Taxonomy;
using NUnit.Framework;

namespace Neddle.Tests.Taxonomy
{
    [TestFixture]
    public class TagFixture : NeddleObjectFixture<Tag>
    {
        internal override Tag MockEntity
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
