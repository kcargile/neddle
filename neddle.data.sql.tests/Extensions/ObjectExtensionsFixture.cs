//using System;
//using Neddle.Extensions;
//using Xunit;

//namespace Neddle.Tests.Extensions
//{
//    public class ObjectExtensionsFixture
//    {
//        [Fact]
//        public void TestIsParameterNull()
//        {
//            new object().CheckNull();

//            object o = null;
//            Assert.Throws<ArgumentNullException>(() => o.CheckNull());
//        }

//        [Fact]
//        public void TestIsParameterNullWithNull()
//        {
//            object obj = null;
//            Assert.That(() => obj.CheckNull(), Throws.TypeOf<ArgumentNullException>().With.Message.EqualTo("Value cannot be null."));
//        }

//        [Fact]
//        public void TestIsParameterNullWithName()
//        {
//            new object().CheckNull("parameter1");
//        }

//        [Fact]
//        public void TestIsParameterNullWithNullAndName()
//        {
//            object obj = null;
//            Assert.That(() => obj.CheckNull("parameter1"), Throws.TypeOf<ArgumentNullException>().With.Message.EqualTo("Value cannot be null.\r\nParameter name: parameter1"));
//        }

//        [Fact]
//        public void GetDescriptionWithAttributeTest()
//        {
//            Assert.Equal("Test Description", TestEnum.ValueWithADescritpion.GetDescription());
//        }

//        [Fact]
//        public void GetDescriptionWithoutAttributeTest()
//        {
//            Assert.Equal("ValueWithoutADescription", TestEnum.ValueWithoutADescription.GetDescription());
//        }

//        private enum TestEnum
//        {
//            //dont get confused with Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute
//            [System.ComponentModel.Description("Test Description")] ValueWithADescritpion = 1,
//            ValueWithoutADescription
//        }

//        [Fact]
//        public void ValueTypeDbNullToNullableValueTest()
//        {
//            object o = DBNull.Value;
//            Assert.That(() => o.ToNullableValueFromDb<int?>(), Is.EqualTo(null));
//        }

//        [Fact]
//        public void ValueTypeNotNullToNullableValueTest()
//        {
//            object o = 10;
//            Assert.That(() => o.ToNullableValueFromDb<int?>(), Is.EqualTo(10));
//        }

//        [Fact]
//        public void RefTypeDbNullToNullableValueTest()
//        {
//            object o = DBNull.Value;
//            Assert.That(() => o.ToNullableValueFromDb<string>(), Is.EqualTo(null));
//        }

//        [Fact]
//        public void RefTypeNotNullToNullableValueTest()
//        {
//            object o = "abs";
//            Assert.That(() => o.ToNullableValueFromDb<string>(), Is.EqualTo("abs"));
//        }

//        [Fact]
//        public void ValueTypeDbNullTest()
//        {
//            object o = DBNull.Value;
//            Assert.That(() => o.ToValueFromDb<int>(), Throws.TypeOf<ArgumentNullException>());
//        }

//        [Fact]
//        public void ValueTypeNotNullTest()
//        {
//            object o = 10;
//            Assert.That(() => o.ToValueFromDb<int>(), Is.EqualTo(10));
//        }

//        [Fact]
//        public void RefTypeDbNullTest()
//        {
//            object o = DBNull.Value;
//            Assert.That(() => o.ToValueFromDb<string>(), Throws.TypeOf<ArgumentNullException>());
//        }

//        [Fact]
//        public void RefTypeNotNullTest()
//        {
//            object o = "abs";
//            Assert.That(() => o.ToValueFromDb<string>(), Is.EqualTo("abs"));
//        }
//    }
//}


