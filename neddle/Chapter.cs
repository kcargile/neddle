using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Neddle
{
    /// <summary>
    /// A section within a <see cref="Course"/>.
    /// </summary>
    public class Chapter : NeddleObject<Chapter>
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
        public Chapter()
        {
            Slides = new List<Slide>();
        }
    }
}
