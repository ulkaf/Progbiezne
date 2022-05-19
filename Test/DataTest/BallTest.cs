using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

            Assert.IsTrue(DApi.GetBall(0).Size >= 20 && DApi.GetBall(0).Size <= 40);
            Assert.IsTrue(DApi.GetBall(0).Weight == DApi.GetBall(0).Size);
            Assert.IsTrue(DApi.GetBall(0).NewX >= -11 && DApi.GetBall(0).NewX <= 11);
            Assert.IsTrue(DApi.GetBall(0).NewY >= -11 && DApi.GetBall(0).NewY <= 11);
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
            DApi.GetBall(0).Move();
            Assert.AreNotEqual(x, DApi.GetBall(0).X);
            Assert.AreNotEqual(y, DApi.GetBall(0).Y);
        }

        [TestMethod]
        public void setTests()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            DApi.CreateBallsList(1);
            DApi.GetBall(0).X = 10;
            DApi.GetBall(0).Y = 17;
            DApi.GetBall(0).NewX = 4;
            DApi.GetBall(0).NewY = -3;
            Assert.AreEqual(10, DApi.GetBall(0).X);
            Assert.AreEqual(17, DApi.GetBall(0).Y);
            Assert.AreEqual(4, DApi.GetBall(0).NewX);
            Assert.AreEqual(-3, DApi.GetBall(0).NewY);
        }




    }
}