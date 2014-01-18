namespace Neddle.Data
{
    /// <summary>
    /// A provider for managing persistent Neddle data.
    /// </summary>
    interface INeddleDataProvider
    {
        /// <summary>
        /// Gets or sets the course data provider.
        /// </summary>
        /// <value>
        /// The course data provider.
        /// </value>
        ICourseDataProvider CourseDataProvider { get; set; }
    }
}
