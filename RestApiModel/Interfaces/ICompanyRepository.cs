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
        List<Company> Read(int Id);
        int Add(string name);
        int Update(int Id, string name, bool delete);
    }
}
