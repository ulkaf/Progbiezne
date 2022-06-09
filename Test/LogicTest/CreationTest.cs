using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestLogic
{
    [TestClass]
    public class CreationTest
    {

        private LogicAbstractApi LApi;


        [TestMethod]
        public void testCreateApi()
        {
            LApi = LogicAbstractApi.CreateApi(800, 600);
            Assert.IsNotNull(LApi);
        }

        [TestMethod]
        public void getWidthHeightTest()
        {
            LApi = LogicAbstractApi.CreateApi(800, 600);
            Assert.AreEqual(800, LApi.Width);
            Assert.AreEqual(600, LApi.Height);
          
        }

        [TestMethod]
        public void createDeleteTest()
        {
            LApi = LogicAbstractApi.CreateApi(800, 600);
            Assert.AreEqual(5, LApi.CreateBalls(5).Count);
            Assert.AreEqual(2, LApi.DeleteBalls(3).Count);
            Assert.AreEqual(0, LApi.DeleteBalls(3).Count);
        }

    }
}