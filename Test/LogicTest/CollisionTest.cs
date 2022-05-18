using Logic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestLogic
{
    [TestClass]
    public class CollisionTest
    {
        private LogicAbstractApi LApi;


        [TestMethod]
        public void testCollision()
        {
            LApi = LogicAbstractApi.CreateApi(800, 600);
            LApi.CreateBalls(2);

            LApi.GetBall(0).NewX = 5;
            LApi.GetBall(0).NewY = 5;
            LApi.GetBall(1).NewX = -3;
            LApi.GetBall(1).NewY = -3;

            LApi.GetBall(0).X = 20;
            LApi.GetBall(1).X = 30;
            LApi.GetBall(0).Y = 20;
            LApi.GetBall(1).Y = 30;
            Assert.AreNotEqual(-3, LApi.GetBall(1).NewX);
            Assert.AreNotEqual(-3, LApi.GetBall(1).NewY);
            LApi.GetBall(0).X = 40;
            LApi.GetBall(0).Y = 40;
            Assert.AreNotEqual(5, LApi.GetBall(0).NewX);
            Assert.AreNotEqual(5, LApi.GetBall(0).NewY);
        }


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