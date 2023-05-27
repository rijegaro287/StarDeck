namespace Stardeck.Models.Tests
{
    [TestClass()]
    public class CollectionTest
    {
        private Collection collection;
        
        
        [TestInitialize]
        public void poblateData()
        {
            collection = new Collection(new List<string>().ToArray());
            collection.Collectionlist.Add("1");
            collection.IdAccount="U-123456789012";
        }
        
        [TestMethod()]
        public void CollectionCreation()
        {
            collection = new Collection(new List<string>().ToArray());
            collection.Collectionlist.Add("1");
            collection.IdAccount="U-123456789012";
            Assert.IsNotNull(collection);
            Assert.IsNotNull(collection.Collectionlist);
            Assert.AreEqual(collection.Collectionlist.Count, 1);
            Assert.AreEqual(collection.IdAccount, "U-123456789012");
            

        }
        [TestMethod()]
        public void addCardTest()
        {
            
            int initial= collection.Collectionlist.Count;
            collection.Collectionlist.Add("2");
            Assert.IsTrue(collection.Collectionlist.Count == initial+1);

        }
        
        [TestMethod()]
        public void addCardErrorTest()
        {
            int initial= collection.Collectionlist.Count;
            collection.Collectionlist.Add("1");
            Assert.IsFalse(collection.Collectionlist.Count == initial+1);

        }
    }
}