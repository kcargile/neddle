using System;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Neddle.Taxonomy
{
    /// <summary>
    /// A tag.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "tag")]
    [DataContract(Namespace = DefaultNamespace)]
    public class Tag : NeddleObject<Tag>, ICloneable
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [DataMember]
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide" /> class.
        /// </summary>
        /// <param name="title">The value.</param>
        public Tag(string title) : this(Guid.NewGuid(), title)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public Tag(Guid id, string value) : base(id)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(value));

            Value = value;
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
        public override bool Equals(Tag obj)
        {
            return
                base.Equals(obj) &&
                Value == obj.Value;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return Clone(this, new Tag(Value));
        }
    }
}
