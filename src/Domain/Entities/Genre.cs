using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
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

        public string? GenreName { get; set; }
        public bool Status { get; set; }
    }
}
