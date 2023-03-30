using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
* @author urman
*
* @date - 3/30/2023 12:17:16 PM 
*/

namespace Book.Domain
{
    [Table("Sale")]
    public class Sale : BaseEntity<int>
    {
        public Sale()
        {
        }
        public string ProductName { get; set; }
        public decimal Price { get; set; }

    }
}