using NUnit.Framework;

namespace Neddle.Tests
{
    [TestFixture]
    public class CourseFixture : NeddleObjectFixture<Course>
    {
        [Test]
        public override void Equals()
        {
            Assert.Fail();
        }
    }
}
