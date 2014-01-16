using System;
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
    public class Tag : NeddleObject<Tag>
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
    }
}
