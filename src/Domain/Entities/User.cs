using Book.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public User()
        {
        }

        public User(string? userName, string? name, string? surname, DateTime? dOB, string? address)
        {
            UserName = userName;
            Name = name;
            Surname = surname;
            DOB = dOB;
            Address = address;
        }

        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public int? Age { get {

                // validation
                if (DOB != null) {
                    return DateTime.Now.Year - ((DateTime)DOB).Year;
                }

                return null;
            } 
        }
    }
}
