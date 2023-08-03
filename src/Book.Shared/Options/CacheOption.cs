using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Shared.Options;
public class CacheOption
{
    public const string Cache = "Cache";
    public int AbsoluteExpirationInMins { get; set; }
    public int BookGetListAbsoluteExpirationInMins { get; set; }
}
