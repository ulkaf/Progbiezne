using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    [TestClass]
    public class LogicTest
    {

        private LogicAbstractApi LApi;
        
        [TestMethod]
        public void getSize()
        {
            LApi = LogicAbstractApi.CreateApi(800,600);
            LApi.CreateBallsList(5);
            Assert.AreEqual(5,LApi.GetCount);
        }
      
    }
}