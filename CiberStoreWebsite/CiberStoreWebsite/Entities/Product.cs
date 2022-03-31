using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CiberStoreWebsite.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public long? Price { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
