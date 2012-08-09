using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Neddle.Web.Services;

namespace Neddle.Taxonomy
{
    /// <summary>
    /// A tag.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "tag")]
    [DataContract(Namespace = Service.DefaultNamespace)]
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
            unchecked
            {
                return Id.GetHashCode();
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Tag))
            {
                return false;
            }

            return Equals(obj as Tag);
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
