using System;
using NUnit.Framework;

namespace Neddle.Tests
{
    [TestFixture]
    public class CourseFixture : NeddleObjectFixture<Course>
    {
        internal override Course MockEntity
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
