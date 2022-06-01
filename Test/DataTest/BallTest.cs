using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestData
{


    [TestClass]
    public class BallTest
    {
        private DataAbstractApi DApi;

        [TestMethod]
        public void createIBallTest()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            DApi.CreateBallsList(1);
            Assert.AreEqual(1, DApi.GetBall(0).ID);

            Assert.IsTrue(DApi.GetBall(0).X >= DApi.GetBall(0).Size);
            Assert.IsTrue(DApi.GetBall(0).X <= (DApi.Width - DApi.GetBall(0).Size));
            Assert.IsTrue(DApi.GetBall(0).Y >= DApi.GetBall(0).Size);
            Assert.IsTrue(DApi.GetBall(0).Y <= (DApi.Height - DApi.GetBall(0).Size));

            Assert.AreEqual(30, DApi.GetBall(0).Size);
            Assert.IsTrue(DApi.GetBall(0).Weight == DApi.GetBall(0).Size);
            Assert.IsTrue(DApi.GetBall(0).NewX >= -5 && DApi.GetBall(0).NewX <= 6);
            Assert.IsTrue(DApi.GetBall(0).NewY >= -5 && DApi.GetBall(0).NewY <= 6);
        }

        [TestMethod]
        public void moveTest()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            DApi.CreateBallsList(1);
            double x = DApi.GetBall(0).X;
            double y = DApi.GetBall(0).Y;
            DApi.GetBall(0).NewX = 5;
            DApi.GetBall(0).NewY = 5;
            DApi.GetBall(0).Move(1);
            Assert.AreNotEqual(x, DApi.GetBall(0).X);
            Assert.AreNotEqual(y, DApi.GetBall(0).Y);
        }




    }
}