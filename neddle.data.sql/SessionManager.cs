using System;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Bytecode;
using Neddle.Data.Sql.Configuration;

namespace Neddle.Data.Sql
{
    /// <summary>
    /// Class for managing connections to persistent storage.
    /// </summary>
    internal static class SessionManager
    {
        private static readonly object Lock = new object();

        private static ISession _session;
        /// <summary>
        /// Gets the current session.
        /// </summary>
        public static ISession CurrentSession
        {
            get
            {
                if (null == _session)
                {
                    lock (Lock)
                    {
                        if (null == _session)
                        {
                            _session = BuildNewSession();
                        }
                    }
                }
                else
                {
                    try
                    {
                        _session.Flush();
                    }
                    catch (ObjectDisposedException)
                    {
                        _session = BuildNewSession();
                    }
                }

                return _session;
            }
        }

        /// <summary>
        /// Gets a new session.
        /// </summary>
        public static ISession NewSession
        {
            get
            {
               return BuildNewSession();
            }
        }

        private static NHibernate.Cfg.Configuration _configuration;
        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        public static NHibernate.Cfg.Configuration CurrentConfiguration
        {
            get
            {
                if (null == _configuration)
                {
                    lock (Lock)
                    {
                        if (null == _configuration)
                        {
                            _configuration = new NHibernate.Cfg.Configuration();
                            _configuration.Configure();
                        }
                    }
                }

                return _configuration;
            }
        }

        /// <summary>
        /// Builds a new session.
        /// </summary>
        /// <returns>An <see cref="ISession"/> representing a connection to the current persistence mechanism.</returns>
        private static ISession BuildNewSession()
        {          
            return
                Fluently
                    .Configure(CurrentConfiguration)
                    .ProxyFactoryFactory(typeof(DefaultProxyFactoryFactory))
                    .Mappings(m => m.AutoMappings.Add(new SessionConfiguration().PersistenceModel))
                    .BuildSessionFactory()
                    .OpenSession(new CoreInterceptor());
        }
    }
}
