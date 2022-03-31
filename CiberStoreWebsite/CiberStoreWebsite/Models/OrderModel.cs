using CiberStoreWebsite.Entities;
using System;
using System.Collections.Generic;

namespace CiberStoreWebsite.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public int? ProductId { get; set; }
        public string CategoryName { get; set; }
        public int? Amount { get; set; }
        public string AmountDisplay { get { return Amount.HasValue ? String.Format("{0:n0}", Amount) : string.Empty; } }
        public DateTime? OrderDate { get; set; }
        public string OrderDateDisplay { get { return OrderDate.HasValue ? OrderDate.Value.ToString("dd-MMM-yyyy") : string.Empty; } }
        public List<Customer> Customers = new List<Customer>();
        public List<Product> Products = new List<Product>();
    }
}
