using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;

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
            IBall b = DApi.CreateBall(1);
            Assert.AreEqual(1, b.ID);

            Assert.IsTrue(b.X >= b.Size);
            Assert.IsTrue(b.X <= (DApi.Width - b.Size));
            Assert.IsTrue(b.Y >= b.Size);
            Assert.IsTrue(b.Y <= (DApi.Height - b.Size));

            Assert.AreEqual(30, b.Size);
            Assert.IsTrue(b.Weight == b.Size);
            Assert.IsTrue(b.NewX >= -5 && b.NewX <= 6);
            Assert.IsTrue(b.NewY >= -5 && b.NewY <= 6);
        }

        [TestMethod]
        public void moveTest()
        {
            DApi = DataAbstractApi.CreateApi(800, 600);
            IBall b = DApi.CreateBall(1);
            double x = b.X;
            double y = b.Y;
            b.changeVelocity(5, 5);
            ConcurrentQueue<IBall> queue = new ConcurrentQueue<IBall>();
            b.Move(1, queue);
            Assert.AreNotEqual(x, b.X);
            Assert.AreNotEqual(y, b.Y);
;
        }




    }
}