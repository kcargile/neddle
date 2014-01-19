using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Neddle.Extensions;

namespace Neddle
{
    /// <summary>
    /// A single slide within a <see cref="Chapter"/>
    /// </summary>
    [Serializable]
    [XmlRoot("slide")]
    [DataContract(Namespace = DefaultNamespace)]
    public class Slide : NeddleObject<Slide>, ICloneable
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
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [DataMember]
        [XmlText]
        public string Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public Slide(string title) : this(Guid.NewGuid(), title)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        public Slide(Guid id, string title) : base(id)
        {
            title.CheckNullOrEmpty("title");

            Title = title;
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
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Slide obj)
        {
            return
                base.Equals(obj) &&
                Title == obj.Title &&
                Content == obj.Content;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            Slide clone = new Slide(Title)
            {
                Content = Content
            };

            return Clone(this, clone);
        }
    }
}
