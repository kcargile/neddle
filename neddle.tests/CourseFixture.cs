using System.Collections.Generic;
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

            Course course2 = (Course)course1.Clone();

            Assert.Equal(course1, course2);
        }

        [Fact]
        public void CoursesWithNullCollectionsAreEqual()
        {
            Course course1 = new Course("Test Course", "TST101", "This is a test course.")
            {
                Chapters = null
            };

            Course course2 = (Course)course1.Clone();

            Assert.Equal(course1, course2);
        }

        [Fact]
        public void CoursesAreNotEqual()
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

            Course course2 = new Course("Test Course 2", "TST102", "This is a test course.")
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

            Course course3 = new Course("Test Course 2", "TST102", "This is a test course.")
            {
                Chapters = null
            };
            
            Assert.NotEqual(course1, course2);
            Assert.NotEqual(course2, course3);
        }
    }
}
