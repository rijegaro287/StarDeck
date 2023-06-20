using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Stardeck.Models.Tests
{
    [TestClass()]
    public class AccountTests
    {
        private Account acc;

        [TestInitialize]
        public void poblateData()
        {
            acc = new("{}")
            {
                Id = "U-123456789012"
            };
        }

        [TestMethod()]
        public void accountServerConfigParseEmpty()
        {

            var account = new Account("{}");

            Assert.IsNotNull(account);
            Assert.AreEqual(account.Serverconfig.Count, 0);

        }
        [TestMethod()]
        public void accountServerConfigParseinvalid()
        {

            var account = new Account("a{Rol:User}");
            account.Serverconfig["0"] = "0";
            account.Serverconfig.Clear();
            Assert.IsNotNull(account);
            Assert.IsNotNull(account.Serverconfig);
            Assert.AreEqual(account.Serverconfig.Count, 0);
            Assert.AreEqual(account.Config, "{}");

        }
        [TestMethod()]
        public void accountServerConfigParseValid()
        {

            var account = new Account("{'Rol':'User'}");


            Assert.IsNotNull(account);
            Assert.AreEqual(account.Serverconfig.Count, 1);
        }
        [TestMethod()]
        public void accountServerConfigAutoSave()
        {

            var account = new Account("{'Rol':'User'}");
            account.Serverconfig["Prueba"] = "1";
            Assert.AreEqual(account.Serverconfig.Count, 2);
            Assert.AreEqual(account.Config, JsonConvert.SerializeObject(account.Serverconfig));

        }


        [TestMethod()]
        public void generateIDTest()
        {
            var AccAux = new Account("{}") { Id = "" };

            AccAux.generateID();

            Assert.IsTrue(Regex.IsMatch(AccAux.Id, @"^U-[a-zA-Z0-9]{12}"));
        }


        [TestMethod()]
        public void CheckIDTest()
        {
            var actual = acc.Id;

            acc.generateID();

            Assert.IsTrue(Regex.IsMatch(acc.Id, @"^U-[a-zA-Z0-9]{12}"));
            Assert.IsTrue((acc.Id == actual));
        }
    }
}