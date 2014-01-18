using System;
using System.Diagnostics.Contracts;
using log4net;
using Neddle.Data;

// TODO: add logging

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
            Contract.Requires<ArgumentNullException>(course.Id != null);

            course.Validate();
            Exists(course, true);
            
            int affected = _dataProvider.DeleteCourse(course);
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

            bool exists = _dataProvider.Exists(course);
            if (throwIfNotFound && !exists)
            {
                throw new NeddleException(Resources.Courses.CourseDoesNotExist, course.Id);
            }

            return exists;
        }
    }
}