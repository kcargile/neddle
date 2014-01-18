using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moq;
using Neddle.Data;
using Xunit;

namespace Neddle.Tests
{
    public class CourseManagerFixture
    {
        [Fact]
        public void InstantiateWithNullDataProviderThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new CourseManager(null));
        }

        [Fact]
        public void LoadCourseSucceeds()
        {
            Course expected = new Course("Test Course", "TST101", "This is a test course.")
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

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            dataProvider.Setup(o => o.Load(expected.Id)).Returns(expected);

            CourseManager manager = new CourseManager(dataProvider.Object);
            Course actual = manager.LoadCourse(expected.Id);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            dataProvider.Verify(o => o.Load(expected.Id), Times.Once);
        }

        [Fact]
        public void LoadCourseNoMatchReturnsNull()
        {
            Guid courseId = Guid.NewGuid();

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            dataProvider.Setup(o => o.Load(courseId)).Returns(null as Course);

            CourseManager manager = new CourseManager(dataProvider.Object);
            Course actual = manager.LoadCourse(courseId);

            Assert.Null(actual);
            dataProvider.Verify(o => o.Load(courseId), Times.Once);
        }

        [Fact]
        public void SaveCourseWithNullCourseThrows()
        {
            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            CourseManager manager = new CourseManager(dataProvider.Object);

            Assert.Throws<ArgumentNullException>(() => manager.SaveCourse(null));
        }

        [Fact]
        public void SaveCourseWithValidCourseSucceeds()
        {
            Course expected = new Course("Test Course", "TST101", "This is a test course.")
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

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            dataProvider.Setup(o => o.SaveCourse(expected)).Returns(expected);

            CourseManager manager = new CourseManager(dataProvider.Object);
            Course actual = manager.SaveCourse(expected);

            Assert.Equal(expected, actual);
            dataProvider.Verify(o => o.SaveCourse(expected), Times.Once());
        }

        [Fact]
        public void SaveCourseWithInvalidCourseThrows()
        {
            Course invalid = new Course("Test Course", "TST101", "This is a test course.")
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
                },
                Name = string.Empty // make invalid
            };

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            CourseManager manager = new CourseManager(dataProvider.Object);

            Assert.Throws<ValidationException>(() => manager.SaveCourse(invalid));
        }

        [Fact]
        public void DeleteCourseWithValidCourseIdSucceeds()
        {
            Assert.True(false);
        }

        [Fact]
        public void DeleteCourseWithNonExistentCourseIdThrows()
        {
            Assert.True(false);
        }

        [Fact]
        public void DeleteCourseWithInvalidCourseIdThrows()
        {
            Course invalid = new Course(Guid.Empty, "Test Course", "TST101", "This is a test course.")
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
                },
                Name = string.Empty // make invalid
            };

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            CourseManager manager = new CourseManager(dataProvider.Object);

            Assert.Throws<ValidationException>(() => manager.DeleteCourse(invalid));
        }
    }
}