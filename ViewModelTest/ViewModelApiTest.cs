using Model;
using ViewModel;

namespace ViewModelTest
{
    [TestClass]
    public class ViewModelApiTest
    {
        AbstractModelApi abstractModelApi;
        ViewModelApi api;


        [TestInitialize]
        public void Initialize()
        {
            abstractModelApi = AbstractModelApi.CreateApi();

            api = new ViewModelApi();
            api.SetModelApi(abstractModelApi);


        }

        [TestMethod]
        public void MakingApiTest()
        {

            Assert.IsNotNull(api);
            Assert.AreEqual(api.GetModelApi(), abstractModelApi);
        }

        [TestMethod]
        public void EnableDisableTest()
        {
            api.enable();
            Assert.IsTrue(abstractModelApi.IsEnabled());
            Assert.AreEqual(api.isEnabled, true);
            Assert.AreEqual(api.BallsListHandler, abstractModelApi.GetAllBalls());
            Assert.AreEqual(Convert.ToInt32(api.BallCounterHandler), abstractModelApi.GetAllBalls().Count);

            api.disable();
            Assert.IsFalse(abstractModelApi.IsEnabled());
            Assert.AreEqual(api.isEnabled, false);

        }
    }
}