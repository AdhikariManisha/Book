using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
* @author urman
*
* @date - 3/29/2023 11:34:09 AM 
*/

namespace Book.Domain
{
    [Table("Actor")]
    public class Actor : BaseEntity<int>
    {
        public Actor()
        {
        }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string phone { get; set; }

    }
}