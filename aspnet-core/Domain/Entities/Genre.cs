using Book.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Entities
{
    public class Genre : BaseEntity<int>
    {
        public Genre()
        {
        }

        public Genre(string? genreName, bool status)
        {
            GenreName = genreName;
            Status = status;
        }
        [Required]
        public string? GenreName { get; set; }
        public bool Status { get; set; }
    }
}
