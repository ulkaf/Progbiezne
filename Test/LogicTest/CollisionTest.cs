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
            double oldX = LApi.GetBall(0).NewY;
            bool contin = true;
            while(contin)
            {
                LApi.GetBall(0).Move(1);
                if(oldX != LApi.GetBall(0).NewY)
                {
                    contin = false;
                }
            }
            Assert.AreNotEqual(oldX, LApi.GetBall(0).NewY);
            Assert.AreEqual(-oldX, LApi.GetBall(0).NewY);
        }
    }
}