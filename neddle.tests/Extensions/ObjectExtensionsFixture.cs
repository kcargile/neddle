using System;
using NUnit.Framework;
using Neddle.Extensions;

namespace Neddle.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsFixture
    {
        [Test]
        public void TestIsParameterNull()
        {
            new object().CheckNull();

            object o = null;
            Assert.Throws<ArgumentNullException>(() => o.CheckNull());
        }

        [Test]
        public void TestIsParameterNullWithNull()
        {
            object obj = null;
            Assert.That(() => obj.CheckNull(), Throws.TypeOf<ArgumentNullException>().With.Message.EqualTo("Value cannot be null."));
        }

        [Test]
        public void TestIsParameterNullWithName()
        {
            new object().CheckNull("parameter1");
        }

        [Test]
        public void TestIsParameterNullWithNullAndName()
        {
            object obj = null;
            Assert.That(() => obj.CheckNull("parameter1"), Throws.TypeOf<ArgumentNullException>().With.Message.EqualTo("Value cannot be null.\r\nParameter name: parameter1"));
        }

        [Test]
        public void GetDescriptionWithAttributeTest()
        {
            Assert.AreEqual("Test Description", TestEnum.ValueWithADescritpion.GetDescription());
        }

        [Test]
        public void GetDescriptionWithoutAttributeTest()
        {
            Assert.AreEqual("ValueWithoutADescription", TestEnum.ValueWithoutADescription.GetDescription());
        }

        private enum TestEnum
        {
            //dont get confused with Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute
            [System.ComponentModel.Description("Test Description")] ValueWithADescritpion = 1,
            ValueWithoutADescription
        }

        [Test]
        public void ValueTypeDbNullToNullableValueTest()
        {
            object o = DBNull.Value;
            Assert.That(() => o.ToNullableValueFromDb<int?>(), Is.EqualTo(null));
        }

        [Test]
        public void ValueTypeNotNullToNullableValueTest()
        {
            object o = 10;
            Assert.That(() => o.ToNullableValueFromDb<int?>(), Is.EqualTo(10));
        }

        [Test]
        public void RefTypeDbNullToNullableValueTest()
        {
            object o = DBNull.Value;
            Assert.That(() => o.ToNullableValueFromDb<string>(), Is.EqualTo(null));
        }

        [Test]
        public void RefTypeNotNullToNullableValueTest()
        {
            object o = "abs";
            Assert.That(() => o.ToNullableValueFromDb<string>(), Is.EqualTo("abs"));
        }

        [Test]
        public void ValueTypeDbNullTest()
        {
            object o = DBNull.Value;
            Assert.That(() => o.ToValueFromDb<int>(), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void ValueTypeNotNullTest()
        {
            object o = 10;
            Assert.That(() => o.ToValueFromDb<int>(), Is.EqualTo(10));
        }

        [Test]
        public void RefTypeDbNullTest()
        {
            object o = DBNull.Value;
            Assert.That(() => o.ToValueFromDb<string>(), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RefTypeNotNullTest()
        {
            object o = "abs";
            Assert.That(() => o.ToValueFromDb<string>(), Is.EqualTo("abs"));
        }
    }
}


