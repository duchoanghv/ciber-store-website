using CiberStoreWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CiberStoreWebsite.Bll
{
    public interface IOrderBll
    {
        Task<List<OrderModel>> GetOrdersList(string categoryName = "");
        Task<OrderDataModel> GetInitialData();
        string CheckValidityOfOrder(OrderModel order);
        void DoSaveOrder(OrderModel order);
    }
}
