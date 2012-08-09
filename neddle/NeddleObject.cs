using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Neddle.Extensions;
using NHibernate.Classic;
using System.ComponentModel.DataAnnotations;

namespace Neddle
{
    /// <summary>
    /// The base object for all databound Neddle entities.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public abstract class NeddleObject<T> : IValidatable where T : NeddleObject<T>
    {
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
        [XmlAttribute(AttributeName = "id")]
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        [Required]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        [Required]
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NeddleObject&lt;T&gt;"/> class.
        /// </summary>
        protected NeddleObject()
        {
            CreateDate = ModifiedDate = DateTime.Now;
        }

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
            if (null == obj || !(obj is T))
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
                CreateDate.ApproximatelyEqual(obj.CreateDate) &&
                ModifiedDate.ApproximatelyEqual(obj.ModifiedDate);
        }

        /// <summary>
        /// Determines whether the specified lists are equal.
        /// </summary>
        /// <typeparam name="TU">The concrete type of the entities contained in the lists.</typeparam>
        /// <param name="list1">A list to compare.</param>
        /// <param name="list2">Another list to compare.</param>
        /// <returns>
        ///   <c>true</c> if the specified lists are equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public static bool Equals<TU>(IList<TU> list1, IList<TU> list2) where TU : NeddleObject<TU>
        {
            return ((null == list1 || null == list2) ? list1 == list2 : ((list1.Equals(list2)) | (list1.OrderBy(x => x.Id).SequenceEqual(list2.OrderBy(x => x.Id)))));
        }

        /// <summary>
        /// Validate the state of the object before persisting it. If a violation occurs,
        /// throw a <see cref="T:NHibernate.Classic.ValidationFailure"/>. This method must not change the state of the object
        /// by side-effect.
        /// </summary>
        public virtual void Validate()
        {
            ValidationContext validationContext = new ValidationContext(this, null, null);
            Validator.ValidateObject(this, validationContext, true);
        }
    }
}