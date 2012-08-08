using Neddle.Data;
using NUnit.Framework;

namespace Neddle.Tests.Data
{
    [TestFixture]
    public class SessionManagerFixture
    {
        [Test]
        public void GetSession()
        {
            Assert.IsNotNull(SessionManager.CurrentSession);
        }
    }
}
