using Xunit;

namespace Neddle.Tests.Data
{
    public class SessionManagerFixture
    {
        [Fact]
        public void GetSession()
        {
            Assert.NotNull(SessionManager.CurrentSession);
        }
    }
}