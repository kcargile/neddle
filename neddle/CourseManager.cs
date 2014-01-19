using System;
using System.Diagnostics.Contracts;
using Castle.Core.Logging;
using Neddle.Data;

namespace Neddle
{
    /// <summary>
    /// Contains methods for working with <see cref="Course"/> entities.
    /// </summary>
    public class CourseManager
    {
        private readonly ICourseDataProvider _dataProvider;
        private ILogger _logger = NullLogger.Instance;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseManager"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public CourseManager(ICourseDataProvider dataProvider)
        {
            Contract.Requires<ArgumentNullException>(dataProvider != null);

            Logger.DebugFormat(Resources.Courses.CourseManagerStartup, dataProvider.GetType().FullName);
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Loads the course having the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The specified course or null.</returns>
        public Course LoadCourse(Guid id)
        {
            Logger.DebugFormat(Resources.Courses.CourseLoad, id);
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

            Logger.DebugFormat(Resources.Courses.CourseLoad, course.Id);
            course.Validate();
            return _dataProvider.Save(course);
        }

        /// <summary>
        /// Deletes the specified <see cref="Course" />.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns>
        /// Total number of records affected. This may aggregate counts from <see cref="Chapter" /> and <see cref="Slide" /> members.
        /// </returns>
        /// <exception cref="Neddle.NeddleException">
        /// </exception>
        public int DeleteCourse(Course course)
        {
            // TODO: KLC add option here to allow preservation of slides, e.g. for reuse in other courses

            Contract.Requires<ArgumentNullException>(course != null);

            Logger.DebugFormat(Resources.Courses.CourseDelete, course.Id);
            course.Validate();
            Exists(course, true);
            
            int affected = _dataProvider.Delete(course);
            if (affected == 0)
            {
                throw new NeddleException(Resources.Courses.CourseCouldNotBeDeleted, course.Id);
            }

            return affected;
        }

        /// <summary>
        /// Determines if the specified course exists in persistent storage.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <param name="throwIfNotFound">if set to <c>true</c> throws a <see cref="NeddleException"/> if not found.</param>
        /// <returns>
        ///   <c>true</c> if the course exists; otherwise, <c>false</c>.
        /// </returns>
        public bool Exists(Course course, bool throwIfNotFound = false)
        {
            Contract.Requires<ArgumentNullException>(course != null);

            Logger.DebugFormat(Resources.Courses.CourseLoad, course.Id);
            bool exists = _dataProvider.Exists(course);
            Logger.DebugFormat(Resources.Courses.CourseExistsResult, course.Id, exists);

            if (throwIfNotFound && !exists)
            {
                throw new NeddleException(Resources.Courses.CourseDoesNotExist, course.Id);
            }

            return exists;
        }
    }
}