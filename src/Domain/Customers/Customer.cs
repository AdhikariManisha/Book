using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
* @author urman
*
* @date - 3/29/2023 11:31:47 AM 
*/

namespace Book.Domain
{
    [Table("Customer")]
    public class Customer : BaseEntity<int>
    {
        public Customer()
        {
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

    }
}