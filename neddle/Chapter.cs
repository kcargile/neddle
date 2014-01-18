using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Neddle.Extensions;

namespace Neddle
{
    /// <summary>
    /// A section within a <see cref="Course" />.
    /// </summary>
    public class Chapter : NeddleObject<Chapter>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [Required]
        [DataMember]
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the slides.
        /// </summary>
        /// <value>
        /// The slides.
        /// </value>
        [DataMember]
        [XmlArray(ElementName = "slides")]
        [XmlArrayItem(ElementName = "slide", Type = typeof(Slide))]
        public List<Slide> Slides { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Neddle.Slide"/> at the specified index.
        /// </summary>
        [XmlIgnore]
        public Slide this[int index]
        {
            get
            {
                if (null != Slides && Slides.Count >= index)
                {
                    return Slides[index];
                }

                return null;
            }
            set
            {
                if (null == Slides)
                {
                    Slides = new List<Slide>();
                }

                Slides[index] = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chapter"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public Chapter(Guid id, string title) : base(id)
        {
            title.CheckNullOrEmpty("title");

            Title = title;
            Slides = new List<Slide>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chapter" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public Chapter(string title) : this(Guid.NewGuid(), title)
        {

        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Chapter obj)
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
                Title == obj.Title &&
                Slides.NullSafeSequenceEquals(obj.Slides);
        }
    }
}
