using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Neddle.Extensions;
using Neddle.Taxonomy;
using Neddle.Web.Services;

namespace Neddle
{
    /// <summary>
    /// A user's status within a course.
    /// </summary>
    [Flags]
    public enum CourseStatus
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Not started, never visited.
        /// </summary>
        NotStarted = 1,

        /// <summary>
        /// Started.
        /// </summary>
        Started = 2,

        /// <summary>
        /// Completed.
        /// </summary>
        Completed = 4,

        /// <summary>
        /// Submitted (e.g. for CEU credits).
        /// </summary>
        Submitted = 8,

        /// <summary>
        /// All statuses that make a course available to the user.
        /// </summary>
        AllAvailable = 1 | 2 | 4 | 8
    }

    /// <summary>
    /// A basic course.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "course")]
    [DataContract(Namespace = Service.DefaultNamespace)]
    public class Course : NeddleObject<Course>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [DataMember]
        [XmlAttribute(AttributeName = "name")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the short name, e.g. "ENG101".
        /// </summary>
        /// <value>
        /// The short name.
        /// </value>
        [Required]
        [DataMember]
        [XmlAttribute(AttributeName = "shortname")]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [DataMember]
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [Required]
        [DataMember]
        [XmlAttribute(AttributeName = "version")]
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the course language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        [Required]
        [DataMember]
        [XmlAttribute(AttributeName = "language")]
        public CultureInfo Language { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail image.
        /// </summary>
        /// <value>
        /// The thumbnail image.
        /// </value>
        [DataMember]
        [XmlElement(ElementName = "thumbnailImage")]
        public Uri ThumbnailImage { get; set; }

        /// <summary>
        /// Gets or sets the tags associated to the course.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [DataMember]
        [XmlArray(ElementName = "tags")]
        [XmlArrayItem(ElementName = "tag", Type = typeof(Tag))]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the chapters.
        /// </summary>
        /// <value>
        /// The chapters.
        /// </value>
        [DataMember]
        [XmlArray(ElementName = "chapters")]
        [XmlArrayItem(ElementName = "chapter", Type = typeof(Chapter))]
        public List<Chapter> Chapters { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Neddle.Chapter"/> at the specified index.
        /// </summary>
        [XmlIgnore]
        public Chapter this[int index]
        {
            get
            {
                if (null != Chapters && Chapters.Count >= index)
                {
                    return Chapters[index];
                }

                return null;
            }
            set
            {
                if (null == Chapters)
                {
                    Chapters = new List<Chapter>();
                }

                Chapters[index] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Course"/> class.
        /// </summary>
        public Course()
        {
            Tags = new List<Tag>();
            Chapters = new List<Chapter>();
            Language = CultureInfo.CurrentCulture;
            Version = 1;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Course)
            {
                return Equals(obj as Course);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Course obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return
                base.Equals(obj) &&
                Chapters.Equals(obj.Chapters) &&
                Description == obj.Description &&
                Language.NullSafeEquals(obj.Language) &&
                Name == obj.Name &&
                ShortName == obj.ShortName &&
                Tags.Equals(obj.Tags) &&
                ThumbnailImage.NullSafeEquals(obj.ThumbnailImage) &&
                Version == obj.Version;
        }
    }
}
