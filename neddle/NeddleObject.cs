using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Neddle.Extensions;
using System.ComponentModel.DataAnnotations;
using Neddle.Validation;

namespace Neddle
{
    /// <summary>
    /// The base object for all databound Neddle entities.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class NeddleObject<T> where T : NeddleObject<T>
    {
        /// <summary>
        /// Default service namespace.
        /// </summary>
        public const string DefaultNamespace = "http://www.neddle.org/2012/08";

        /// <summary>
        /// Default created by username used for non-interactive operations.
        /// </summary>
        [XmlIgnore]
        internal const string DefaultCreatedByUserName = "SYSTEM";

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        [Required]
        [CustomValidation(typeof(GuidValidator), "IsNotEmpty")]
        [XmlAttribute(AttributeName = "id")]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        [Required]
        [XmlAttribute(AttributeName = "createdDate")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets username of the user who created this instance.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        [StringLength(50)]
        [DataMember]
        [XmlAttribute(AttributeName = "createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        [Required]
        [DataMember]
        [XmlAttribute(AttributeName = "modifedDate")]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets username of the user who last modified this instance.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]
        [StringLength(50)]
        [DataMember]
        [XmlAttribute(AttributeName = "modifedBy")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleObject{T}"/> class.
        /// </summary>
        protected NeddleObject() : this(Guid.NewGuid())
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleObject{T}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        protected NeddleObject(Guid id)
        {
            Id = id;
            CreatedDate = ModifiedDate = DateTime.Now;
            CreatedBy = ModifiedBy = DefaultCreatedByUserName;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.CalculateHash();
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
            if (obj == null || !(obj is T))
            {
                return false;
            }

            return Equals(obj as T);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Equals(T obj)
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
                Id == obj.Id &&
                CreatedDate.ApproximatelyEqual(obj.CreatedDate) &&
                ModifiedDate.ApproximatelyEqual(obj.ModifiedDate);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public virtual void Validate()
        {
            ValidationContext validationContext = new ValidationContext(this, null, null);
            Validator.ValidateObject(this, validationContext, true);
        }
    }
}