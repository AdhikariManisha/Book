using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.Interfaces;
public interface IDataSeeder
{
    public Task<bool> SeedAsync();
}
