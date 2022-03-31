using CiberStoreWebsite.Entities;
using System.Collections.Generic;
namespace CiberStoreWebsite.Models
{
    public class OrderDataModel
    {
        public List<OrderModel> Orders = new List<OrderModel>();
        public List<Customer> Customers = new List<Customer>();
        public List<Product> Products = new List<Product>();
    }
}
