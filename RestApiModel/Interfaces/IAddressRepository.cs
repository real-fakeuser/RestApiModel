using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiModel.Model;


namespace RestApiModel.Interfaces
{
    public interface IAddressRepository
    {
        List<Address> Read();
        List<Address> Read(int Id);
        List<Address> Create(Address NewAddressSet);
        List<Address> Update(Address NewAddressSet);
        int Delete(int Id);
    }

}
