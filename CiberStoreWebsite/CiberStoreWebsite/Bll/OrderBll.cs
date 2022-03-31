using CiberStoreWebsite.Entities;
using CiberStoreWebsite.Lib;
using CiberStoreWebsite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiberStoreWebsite.Bll
{
    public class OrderBll : IOrderBll
    {
        private readonly CiberStoreDbContext _dbContext;
        private readonly ILogger _logger;

        public OrderBll(CiberStoreDbContext context, ILogger<OrderModel> logger)
        {
            _logger = logger;
            _dbContext = context;
        }

        public string CheckValidityOfOrder(OrderModel order)
        {
            int? quantity = _dbContext.Products.AsNoTracking().SingleOrDefault(e => e.Id == order.ProductId)?.Quantity;
            if (quantity.HasValue && quantity < order.Amount)
            {
                string msg =  string.Format("The amount of the order is greater than the quantity of the product({0})", quantity);
                _logger.LogWarning(MyLogEvents.InsertItem, msg);
                return msg;
            }
            return string.Empty;
        }

        public void DoSaveOrder(OrderModel order)
        {
            _dbContext.Orders.Add(new Order()
            {
                Amount = order.Amount,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                ProductId = order.ProductId
            });
            Product product = _dbContext.Products.Find(order.ProductId);
            if (product != null)
                product.Quantity -= order.Amount;
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, ex, "Save order error", order.Name);
                throw;
            }
        }

        public async Task<OrderDataModel> GetInitialData()
        {
            OrderDataModel model = new OrderDataModel();
            model.Orders = await GetOrdersList();
            model.Customers = await GetCustomersList();
            model.Products = await GetProductsList();
            if (model.Orders.Count == 0)
            {
                _logger.LogWarning(MyLogEvents.GetItemNotFound, "No orders yet");
            }
            return model;
        }

        public Task<List<OrderModel>> GetOrdersList(string categoryName = "")
        {
            return (from odr in _dbContext.Orders.AsNoTracking()
                    join cst in _dbContext.Customers on odr.CustomerId equals cst.Id
                    join pro in _dbContext.Products on odr.ProductId equals pro.Id
                    join cat in _dbContext.Categories on pro.CategoryId equals cat.Id
                    where categoryName == "" || categoryName == null || cat.Name.Contains(categoryName)
                    select new OrderModel()
                    {
                        Amount = odr.Amount,
                        CategoryName = cat.Name,
                        ProductName = pro.Name,
                        CustomerName = cst.Name,
                        OrderDate = odr.OrderDate
                    }).ToListAsync();
        }
        private Task<List<Customer>> GetCustomersList()
        {
            return _dbContext.Customers.AsNoTracking().ToListAsync();
        }


        private Task<List<Product>> GetProductsList()
        {
            return _dbContext.Products.AsNoTracking().ToListAsync();
        }
    }
}
