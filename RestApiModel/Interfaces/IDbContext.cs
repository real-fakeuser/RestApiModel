using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;


namespace RestApiModel.Interfaces
{
    public interface IDbContext
    {
        IDbConnection GetCompany();
        IDbConnection GetAddress();
    }
}
