using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
namespace Test
{
    [TestClass]
    public class CreationTest
    {

        private LogicAbstractApi LApi;
      


        [TestMethod]
        public void getCount()
        {
           
            LApi = LogicAbstractApi.CreateApi(800, 600);
            //Assert.AreEqual(800, LApi.Width);
           // Assert.AreEqual(600, LApi.Height);
            LApi.CreateBalls(5);
            Assert.AreEqual(5, LApi.GetCount);
            LApi.CreateBalls(-3);
            Assert.AreEqual(2, LApi.GetCount);
            LApi.CreateBalls(-3);
            Assert.AreEqual(0, LApi.GetCount);
        }

      



    }
}