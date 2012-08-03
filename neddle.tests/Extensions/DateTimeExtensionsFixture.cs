using System;
using Neddle.Extensions;
using NUnit.Framework;

namespace Neddle.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsFixture
    {
        [Test]
        public void AreApproximatelyEqual()
        {
            DateTime now = DateTime.Now;
            DateTime alsoNow = now;
            DateTime yesterday = DateTime.Now.AddDays(-1);

            Assert.That(now.ApproximatelyEqual(alsoNow));
            Assert.That(!now.ApproximatelyEqual(yesterday));
        }

        [Test]
        public void AreApproximatelyEqualNullable()
        {
            DateTime? now = DateTime.Now;
            DateTime? alsoNow = now;
            DateTime? yesterday = DateTime.Now.AddDays(-1);

            Assert.That(now.ApproximatelyEqual(alsoNow));
            Assert.That(!now.ApproximatelyEqual(yesterday));

            now = null;
            Assert.That(!now.ApproximatelyEqual(yesterday));

            yesterday = null;
            Assert.That(now.ApproximatelyEqual(yesterday));
        }
    }
}
