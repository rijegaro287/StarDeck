using Newtonsoft.Json;

namespace Stardeck.Models.Tests
{
    [TestClass()]
    public class AccountTests
    {
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
    }
}