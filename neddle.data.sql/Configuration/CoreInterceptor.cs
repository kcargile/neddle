using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Type;

namespace Neddle.Data
{
    /// <summary>
    /// Core <see cref="IInterceptor"/>.
    /// </summary>
    public class CoreInterceptor : EmptyInterceptor
    {
        /// <summary>
        /// Called when an entity is hydrated.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The id.</param>
        /// <param name="state">The state.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="types">The types.</param>
        /// <returns><c>true</c></returns>
        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            ConvertDatabaseDateTimeToUtc(state, types); // forces UTC datetime fields
            return true;
        }

        /// <summary>
        /// Converts the database date time to UTC.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="types">The types.</param>
        private void ConvertDatabaseDateTimeToUtc(object[] state, IList<IType> types)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].ReturnedClass != typeof(DateTime))
                {
                    continue;
                }

                DateTime? dateTime = state[i] as DateTime?;

                if (!dateTime.HasValue)
                {
                    continue;
                }

                if (dateTime.Value.Kind != DateTimeKind.Unspecified)
                {
                    continue;
                }

                state[i] = DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc);
            }
        }
    }
}
