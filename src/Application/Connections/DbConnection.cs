using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Application.Connections;
public class DbConnection: Contracts.Connections.IDbConnection
{
    private readonly IConfiguration _configuration;

    public DbConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public System.Data.IDbConnection GetConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        return new SqlConnection(connectionString);
    }
}
