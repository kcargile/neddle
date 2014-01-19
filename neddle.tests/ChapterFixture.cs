using System.Collections.Generic;
using System.Linq;
using Neddle.Extensions;
using Xunit;

namespace Neddle.Tests
{
    public class ChapterFixture
    {
        [Fact]
        public void ChaptersAreEqual()
        {
            Chapter chapter1 = new Chapter("Chapter One")
            {
                Slides = new List<Slide>
                {
                    new Slide("Slide One")
                }
            };

            Chapter chapter2 = new Chapter(chapter1.Id, "Chapter One")
            {
                Slides = chapter1.Slides.NullSafeClone().ToList()
            };

            Assert.Equal(chapter1, chapter2);
        }

        [Fact]
        public void ChaptersWithNullSlidesAreEqual()
        {
            Chapter chapter1 = new Chapter("Chapter One")
            {
                Slides = null
            };

            Chapter chapter2 = new Chapter(chapter1.Id, "Chapter One")
            {
                Slides = null
            };

            Assert.Equal(chapter1, chapter2);
        }

        [Fact]
        public void ChaptersAreNotEqual()
        {
            Chapter chapter1 = new Chapter("Chapter One")
            {
                Slides = new List<Slide>
                {
                    new Slide("Slide One")
                }
            };

            Chapter chapter2 = new Chapter("Chapter Two")
            {
                Slides = new List<Slide>
                {
                    new Slide("Slide One")
                }
            };

            Assert.NotEqual(chapter1, chapter2);
        }
    }
}
