using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Domain.Entities;
public abstract class AuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
    [ForeignKey("CreatedBy")]
    public virtual User CreatedByUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int? UpdatedBy { get; set; }

    [ForeignKey("UpdatedBy")]
    public virtual User UpdatedByUser { get; set; }
}
