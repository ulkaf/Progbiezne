using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestLogic
{
    [TestClass]
    public class CollisionTest
    {
        private LogicAbstractApi LApi;


        [TestMethod]
        public void testWallCollision()
        {
            LApi = LogicAbstractApi.CreateApi(800, 600);
            LApi.CreateBalls(1);
            LApi.GetBall(0).NewX = 5;
            LApi.GetBall(0).X = 790;
            Assert.AreNotEqual(5, LApi.GetBall(0).NewX);
            LApi.GetBall(0).NewX = -3;
            LApi.GetBall(0).X = -3;
            Assert.AreNotEqual(-3, LApi.GetBall(0).NewX);
            LApi.GetBall(0).NewY = -7;
            LApi.GetBall(0).Y = -2;
            Assert.AreNotEqual(-7, LApi.GetBall(0).NewY);
            LApi.GetBall(0).NewY = 7;
            LApi.GetBall(0).Y = 607;
            Assert.AreNotEqual(7, LApi.GetBall(0).NewY);

        }
    }
}