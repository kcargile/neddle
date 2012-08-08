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
    }
}
