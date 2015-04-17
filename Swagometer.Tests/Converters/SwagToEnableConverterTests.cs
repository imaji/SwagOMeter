using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swagometer.Converters;
using System;
using Swagometer.Objects;

namespace Swagometer.Tests.Converters
{
    [TestClass]
    public class SwagToEnableConverterTests
    {
        private readonly SwagToEnableConverter _converter = new SwagToEnableConverter();

        [TestMethod]
        public void GivenNullValueThenExpectFalse()
        {
            Assert.IsFalse(Convert(null));
        }

        [TestMethod]
        public void GivenISwagValueThenExpectTrue()
        {
            var swag = new Swag();
            Assert.IsTrue(Convert(swag));
        }

        [TestMethod]
        public void GivenValueNotSwagThenExpectFalse()
        {
            Assert.IsFalse(Convert(new DateTime()));
        }

        private bool Convert(object param)
        {
            return (bool)_converter.Convert(param, typeof(bool), "", null);
        }
    }
}
