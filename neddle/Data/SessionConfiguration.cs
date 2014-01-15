using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions.Helpers;
using Neddle.Extensions;

namespace Neddle.Data
{
    /// <summary>
    /// Domain entity auto-mapping configuration.
    /// </summary>
    internal sealed class SessionConfiguration : DefaultAutomappingConfiguration
    {
        /// <summary>
        /// Gets the auto-mapping persistence model.
        /// </summary>
        /// <value>The auto-mapping persistence model.</value>
        internal AutoPersistenceModel PersistenceModel
        {
            get
            {
                AutoPersistenceModel model = AutoMap.AssemblyOf<SessionConfiguration>(new SessionConfiguration()).UseOverridesFromAssemblyOf<SessionConfiguration>();
                model.Conventions.AddFromAssemblyOf<SessionConfiguration>();
                model.Conventions.Add(DefaultCascade.SaveUpdate());  
                model.OverrideAll(m => m.IgnoreProperty("IsValid"));
                model.OverrideAll(m => m.IgnoreProperty("Version"));

                return model;
            }
        }

        /// <summary>
        /// Determines if the specified type should be automatically mapped.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the type should be mapped; otherwise, <c>false</c>.</returns>
        public override bool ShouldMap(Type type)
        {
            return type.IsSubclassOfRawGeneric(typeof(NeddleObject<>));
        }
    }
}
