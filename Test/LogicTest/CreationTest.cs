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
        public void getCount()
        {
            LApi = LogicAbstractApi.CreateApi(800, 600);
            Assert.AreEqual(800, LApi.Width);
            Assert.AreEqual(600, LApi.Height);
            LApi.CreateBalls(5);
            Assert.AreEqual(5, LApi.GetCount);
            LApi.DeleteBalls(3);
            Assert.AreEqual(2, LApi.GetCount);
            LApi.DeleteBalls(3);
            Assert.AreEqual(0, LApi.GetCount);
        }

        [TestMethod]
        public void getBall()
        {
            LApi = LogicAbstractApi.CreateApi(800, 600);
            LApi.CreateBalls(3);
            Assert.AreNotEqual(LApi.GetBall(0), LApi.GetBall(1));
            Assert.AreNotEqual(LApi.GetBall(1), LApi.GetBall(2));
            Assert.AreNotEqual(LApi.GetBall(0), LApi.GetBall(2));
        }
    }
}