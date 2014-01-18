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
            dataProvider.Setup(o => o.Save(expected)).Returns(expected);

            CourseManager manager = new CourseManager(dataProvider.Object);
            Course actual = manager.SaveCourse(expected);

            Assert.Equal(expected, actual);
            dataProvider.Verify(o => o.Save(expected), Times.Once);
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
            Course valid = new Course(Guid.NewGuid(), "Test Course", "TST101", "This is a test course.")
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
            };

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            dataProvider.Setup(o => o.Exists(valid)).Returns(true);
            dataProvider.Setup(o => o.Delete(valid)).Returns(1);

            CourseManager manager = new CourseManager(dataProvider.Object);
            int affected = manager.DeleteCourse(valid);

            Assert.Equal(1, affected);
            dataProvider.Verify(o => o.Exists(valid), Times.Once);
            dataProvider.Verify(o => o.Delete(valid), Times.Once);
        }

        [Fact]
        public void DeleteCourseWithNonExistentCourseThrows()
        {
            Course missing = new Course(Guid.NewGuid(), "Test Course", "TST101", "This is a test course.")
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
            };

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            dataProvider.Setup(o => o.Exists(missing)).Returns(false);

            CourseManager manager = new CourseManager(dataProvider.Object);
            Assert.Throws<NeddleException>(() => manager.DeleteCourse(missing));

            dataProvider.Verify(o => o.Exists(missing), Times.Once);
            dataProvider.Verify(o => o.Delete(missing), Times.Never);
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

        [Fact]
        public void DeleteCourseWithValidCourseFailureThrowsException()
        {
            Course valid = new Course(Guid.NewGuid(), "Test Course", "TST101", "This is a test course.")
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
            };

            Mock<ICourseDataProvider> dataProvider = new Mock<ICourseDataProvider>();
            dataProvider.Setup(o => o.Exists(valid)).Returns(true);
            dataProvider.Setup(o => o.Delete(valid)).Returns(0);

            CourseManager manager = new CourseManager(dataProvider.Object);
            Assert.Throws<NeddleException>(() => manager.DeleteCourse(valid));

            dataProvider.Verify(o => o.Exists(valid), Times.Once);
            dataProvider.Verify(o => o.Delete(valid), Times.Once);
        }
    }
}