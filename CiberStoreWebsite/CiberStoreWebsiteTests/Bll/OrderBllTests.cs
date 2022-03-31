using Microsoft.VisualStudio.TestTools.UnitTesting;
using CiberStoreWebsite.Entities;
using Microsoft.EntityFrameworkCore;

namespace CiberStoreWebsite.Bll.Tests
{
    [TestClass()]
    public class OrderBllTests
    {
        private static DbContextOptions options = new DbContextOptionsBuilder<CiberStoreDbContext>()
                .UseSqlServer("server=HOANGDUC-PC; database=CiberStoreDB;Trusted_Connection=True;")
                .Options;
        private readonly OrderBll _orderBll = new OrderBll(new CiberStoreDbContext(options), null);

        [TestMethod()]
        public void GetOrdersListTest1()
        {
            using (var context = new CiberStoreDbContext(options))
            {
                Assert.IsTrue(_orderBll.GetOrdersList().Result.Count > 0);
            }
        }
        [TestMethod()]
        public void GetOrdersListTest2()
        {
            using (var context = new CiberStoreDbContext(options))
            {
                Assert.IsTrue(_orderBll.GetOrdersList("hoang").Result.Count == 0);
                Assert.IsTrue(_orderBll.GetOrdersList("house").Result.Count > 0);
                Assert.IsTrue(_orderBll.GetOrdersList("").Result.Count > 0);
                Assert.IsTrue(_orderBll.GetOrdersList(null).Result.Count > 0);
            }
        }
    }
}