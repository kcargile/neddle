using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Neddle.Tests
{
    public class CourseFixture
    {
        [Fact]
        public void CoursesAreEqual()
        {
            Course course1 = new Course("Test Course", "TST101", "This is a test course.")
            {
                Chapters = new List<Chapter>
                {
                    new Chapter("Test Chapter")
                    {
                        Slides = new List<Slide>
                        {
                            new Slide("Test Slide")
                        }
                    }
                }
            };

            Course course2 = new Course(course1.Id, "Test Course", "TST101", "This is a test course.")
            {
                Chapters = new List<Chapter>
                {
                    course1.Chapters.First()
                }
            };

            Assert.Equal(course1, course2);
        }

        [Fact]
        public void CoursesWithNullCollectionsAreEqual()
        {
            Course course1 = new Course("Test Course", "TST101", "This is a test course.")
            {
                Chapters = null
            };

            Course course2 = new Course(course1.Id, "Test Course", "TST101", "This is a test course.")
            {
                Chapters = null
            };

            Assert.Equal(course1, course2);
        }
    }
}
