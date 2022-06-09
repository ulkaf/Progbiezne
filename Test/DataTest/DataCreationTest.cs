using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestData
{
    [TestClass]
    public class DataCreationTest
    {
        private DataAbstractApi DApi;


        [TestMethod]
        public void testCreateApi()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            Assert.IsNotNull(DApi);
        }

        [TestMethod]
        public void getWidtHeighTest()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            Assert.AreEqual(800, DApi.Width);
            Assert.AreEqual(600, DApi.Height);
        }


    }
}