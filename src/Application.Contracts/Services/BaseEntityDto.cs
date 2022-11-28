using Book.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Contracts.Services;
public class BaseEntityDto<Tkey>: AuditableEntity
{
    public Tkey Id { get; set; }
}
