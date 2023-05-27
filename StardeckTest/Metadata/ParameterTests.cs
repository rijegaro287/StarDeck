using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stardeck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stardeck.Models.Tests
{
    [TestClass()]
    public class ParameterTests
    {

        [TestMethod()]
        public void getAsStringTest()
        {
            Parameter parameterstr = new Parameter();
            parameterstr.Value = "str";
            Assert.AreEqual(parameterstr.getAsString(), "str");

            Parameter parameterint = new Parameter();
            parameterint.Value = "1";
            Assert.AreEqual(parameterint.getAsString(), "1");
        }
        [TestMethod()]
        public void getAsIntTest()
        {
            Parameter parameterint = new Parameter();
            parameterint.Value = "1";
            Assert.AreEqual(parameterint.getAsInt(), 1);
        }
        [TestMethod()]
        public void setAsIntTest()
        {
            Parameter parameterint = new Parameter();
            parameterint.Value = "1";
            parameterint.set_Value(3);
            Assert.AreEqual(parameterint.getAsInt(), 3);
            Assert.AreEqual(parameterint.getAsString(), "3");
        }
    }
}