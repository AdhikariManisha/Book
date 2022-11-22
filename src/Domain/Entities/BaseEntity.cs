using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Entities
{
    public abstract class BaseEntity<TKey>: AuditableEntity
    {
        [Key]
        public TKey Id { get; set; }
    }
}
