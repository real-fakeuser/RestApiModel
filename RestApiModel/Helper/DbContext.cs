using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiModel.Interfaces;
using RestApiModel.Model;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace RestApiModel.Helper
{
    public class DbContext : IDbContext
    {
        private readonly DbSettings _settings;
        public DbContext(IOptions<DbSettings> options)
        {
            _settings = options.Value;
        }
        public IDbConnection GetCompany()
        {
            var con = new SqlConnection(_settings.Company);
            con.Open();
            return con;
        }
    }
}
