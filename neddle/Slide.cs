using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Neddle.Web.Services;

namespace Neddle
{
    /// <summary>
    /// A single slide within a <see cref="Chapter"/>
    /// </summary>
    [Serializable]
    [XmlRoot("slide")]
    [DataContract(Namespace = Service.DefaultNamespace)]
    public class Slide : NeddleObject<Slide>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
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
    }
}
