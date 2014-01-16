using System;
using System.Linq;
using Neddle.Data;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Neddle.Tests
{
    [TestFixture]
    public abstract class NeddleObjectFixture<T> where T : NeddleObject<T>
    {
        protected ISession DataSession;

        internal abstract T MockEntity { get; }

        public abstract void Equals();

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

        [Test]
        public abstract void Validate();

        [Test]
        public virtual void Add()
        {
            T entity = MockEntity;

            DataSession.SaveOrUpdate(entity);
            DataSession.Flush();
            DataSession.Evict(entity);

            T saved = (
                from c in DataSession.Query<T>()
                where c.Id == entity.Id
                select c).FirstOrDefault();

            Assert.IsNotNull(saved);
            Assert.AreNotEqual(default(int), saved.Id);
            Assert.AreEqual(entity, saved);
        }

        [Test]
        public virtual void Update()
        {
            const string newCreated = "MODIFIED";
            T entity = MockEntity;

            DataSession.SaveOrUpdate(entity);
            DataSession.Flush();
            DataSession.Evict(entity);

            T saved = (
                from c in DataSession.Query<T>()
                where c.Id == entity.Id
                select c).FirstOrDefault();

            Assert.IsNotNull(saved);
            Assert.AreNotEqual(default(int), saved.Id);
            Assert.AreEqual(entity, saved);

            saved.CreatedBy = newCreated;

            DataSession.Update(saved);
            DataSession.Flush();

            saved = (
                from c in DataSession.Query<T>()
                where c.Id == entity.Id
                select c).FirstOrDefault();

            Assert.IsNotNull(saved);
            Assert.AreEqual(newCreated, saved.CreatedBy);
        }

        [Test]
        public virtual void Delete()
        {
            T entity = MockEntity;

            DataSession.SaveOrUpdate(entity);
            DataSession.Flush();
            DataSession.Evict(entity);

            T saved = (
                from c in DataSession.Query<T>()
                where c.Id == entity.Id
                select c).FirstOrDefault();

            Assert.IsNotNull(saved);
            Assert.AreNotEqual(default(int), saved.Id);

            DataSession.Delete(saved);
            DataSession.Flush();

            saved = (
                from c in DataSession.Query<T>()
                where c.Id == entity.Id
                select c).FirstOrDefault();

            Assert.IsNull(saved);
        }

        [Test]
        public virtual void GetHash()
        {
            T entity = MockEntity;
            Assert.AreEqual(entity.Id, entity.GetHashCode());
        }
    }
}
