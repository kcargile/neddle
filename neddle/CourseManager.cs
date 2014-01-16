using System;
using System.Diagnostics.Contracts;
using log4net;
using Neddle.Data;

namespace Neddle
{
    /// <summary>
    /// Contains methods for working with <see cref="Course"/> entities.
    /// </summary>
    public class CourseManager
    {
        private readonly ICourseDataProvider _dataProvider;

        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        /// <value>
        /// The log.
        /// </value>
        public ILog Log { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseManager"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public CourseManager(ICourseDataProvider dataProvider)
        {
            Contract.Requires<ArgumentNullException>(dataProvider != null);

            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Loads the course having the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The specified course or null.</returns>
        public Course LoadCourse(Guid id)
        {
            return _dataProvider.Load(id);
        }

        /// <summary>
        /// Save the course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns>A reference to the updated course entity.</returns>
        public Course SaveCourse(Course course)
        {
            Contract.Requires<ArgumentNullException>(course != null);

            course.Validate();
            return _dataProvider.SaveCourse(course);
        }

        /// <summary>
        /// Deletes the specified <see cref="Course"/>.
        /// </summary>
        /// <param name="course">The course.</param>
        public void DeleteCourse(Course course)
        {
            Contract.Requires<ArgumentNullException>(course != null);

            _dataProvider.DeleteCourse(course);
        }
    }
}