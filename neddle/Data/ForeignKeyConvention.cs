using System;
using FluentNHibernate;

namespace Neddle.Data
{
    /// <summary>
    /// Represents the foreign key convention used in persistent storage.
    /// </summary>
    internal class ForeignKeyConvention : FluentNHibernate.Conventions.ForeignKeyConvention
    {
        /// <summary>
        /// Builds the key name.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="type">The type.</param>
        /// <returns>A properly formatted foreign key name.</returns>
        protected override string GetKeyName(Member property, Type type)
        {
            if (property == null)
            {
                return string.Concat(type.Name, "Id"); // many-to-many, one-to-many, join
            }

            return string.Concat(property.Name, "Id"); // many-to-one
        }
    }
}
