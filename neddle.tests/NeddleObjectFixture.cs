using System;
using Neddle.Data;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Neddle.Tests
{
    [TestFixture]
    public abstract class NeddleObjectFixture<T> where T : NeddleObject<T>
    {
        protected ISession DataSession;

        [TestFixtureSetUp]
        public virtual void FixtureSetup()
        {
            DataSession = SessionManager.CurrentSession;
            new SchemaExport(SessionManager.CurrentConfiguration).Execute(true, true, false, DataSession.Connection, Console.Out);
        }

        [TestFixtureTearDown]
        public virtual void FixtureTeardown()
        {
            if (null != DataSession)
            {
                DataSession.Dispose();
            }
        }

        public abstract void Equals();
    }
}
