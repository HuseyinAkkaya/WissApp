using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using AppCore.Services;
using WissAppEF.Contexts;
using WissAppEntities.Entities;

namespace MvcUnitTesting
{
    [TestClass]
    public class WissAppTest
    {
        DbContext db = new WissAppContext();

        [TestMethod]
        public void ShouldGet3Users()
        {
            using (var userService = new Service<Users>(db) )
            {
                var entities = userService.GetEntities();

                Assert.AreEqual(3,entities.Count);

            }

        }
    }
}
