using Xunit;

namespace Neddle.Tests
{
    public class SlideFixture
    {
        [Fact]
        public void SlidesAreEqual()
        {
            Slide slide1 = new Slide("Slide One")
            {
                Content = "Slide content"
            };

            Slide slide2 = new Slide(slide1.Id, "Slide One")
            {
                Content = "Slide content"
            };

            Assert.Equal(slide1, slide2);
        }

        [Fact]
        public void SlidesAreNotEqual()
        {
            Slide slide1 = new Slide("Slide One")
            {
                Content = "Slide content"
            };

            Slide slide2 = new Slide("Slide Two")
            {
                Content = "Slide content"
            };

            Assert.NotEqual(slide1, slide2);
        }
    }
}
