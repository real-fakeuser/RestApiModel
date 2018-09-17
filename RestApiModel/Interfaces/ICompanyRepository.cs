using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiModel.Model;

namespace RestApiModel.Interfaces
{
    public interface ICompanyRepository
    {
        List<Company> Read();
    }
}
