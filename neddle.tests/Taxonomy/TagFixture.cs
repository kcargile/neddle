using Neddle.Taxonomy;
using Xunit;

namespace Neddle.Tests.Taxonomy
{
    public class TagFixture
    {
        [Fact]
        public void TagsAreEqual()
        {
            Tag tag1 = new Tag("Tag1");
            Tag tag2 = (Tag)tag1.Clone();

            Assert.Equal(tag1, tag2);
        }

        [Fact]
        public void TagsAreNotEqual()
        {
            Tag tag1 = new Tag("Tag1");
            Tag tag2 = new Tag("Tag2");

            Assert.NotEqual(tag1, tag2);
        }
    }
}
