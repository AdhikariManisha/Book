using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Services;
public class BaseEntityDto<Tkey>: AuditableEntityDto
{
    public Tkey Id { get; set; }
}
