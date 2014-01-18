using System;

namespace Neddle.Data
{
    /// <summary>
    /// A provider for managing persistent <see cref="Course"/> data.
    /// </summary>
    public interface ICourseDataProvider
    {
        /// <summary>
        /// Loads the specified <see cref="Course"/>.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The specified course or null.</returns>
        Course Load(Guid id);

        /// <summary>
        /// Loads the specified <see cref="Course" />.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The specified course or null.</returns>
        Course Load(string name);

        /// <summary>
        /// Saves the specified <see cref="Course"/>.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns>A reference to the updated course entity.</returns>
        Course Save(Course course);

        /// <summary>
        /// Deletes the specified <see cref="Course" />.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns>Total number of records affected. This may aggregate counts from <see cref="Chapter"/> and <see cref="Slide"/> members.</returns>
        int Delete(Course course);

        /// <summary>
        /// Determines if the specified course exists in persistent storage.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns><c>true</c> if the course exists; otherwise, <c>false</c>.</returns>
        bool Exists(Course course);
    }
}