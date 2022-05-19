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
        public void getCount()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            Assert.AreEqual(800, DApi.Width);
            Assert.AreEqual(600, DApi.Height);
            DApi.CreateBallsList(5);
            Assert.AreEqual(5, DApi.GetCount);
            DApi.CreateBallsList(-3);
            Assert.AreEqual(2, DApi.GetCount);
            DApi.CreateBallsList(-3);
            Assert.AreEqual(0, DApi.GetCount);
        }

        [TestMethod]
        public void getBall()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            DApi.CreateBallsList(3);
            Assert.AreNotEqual(DApi.GetBall(0), DApi.GetBall(1));
            Assert.AreNotEqual(DApi.GetBall(1), DApi.GetBall(2));
            Assert.AreNotEqual(DApi.GetBall(0), DApi.GetBall(2));
        }

    }
}