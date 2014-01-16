using System;
using Neddle.Extensions;
using Xunit;

namespace Neddle.Tests.Extensions
{
    public class DateTimeExtensionsFixture
    {
        [Fact]
        public void AreApproximatelyEqual()
        {
            DateTime now = DateTime.Now;
            DateTime alsoNow = now;
            DateTime yesterday = DateTime.Now.AddDays(-1);

            Assert.True(now.ApproximatelyEqual(alsoNow));
            Assert.True(!now.ApproximatelyEqual(yesterday));
        }

        [Fact]
        public void AreApproximatelyEqualNullable()
        {
            DateTime? now = DateTime.Now;
            DateTime? alsoNow = now;
            DateTime? yesterday = DateTime.Now.AddDays(-1);

            Assert.True(now.ApproximatelyEqual(alsoNow));
            Assert.True(!now.ApproximatelyEqual(yesterday));

            now = null;
            Assert.True(!now.ApproximatelyEqual(yesterday));

            yesterday = null;
            Assert.True(now.ApproximatelyEqual(yesterday));
        }
    }
}
