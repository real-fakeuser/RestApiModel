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
        List<Address> Add(List<Address> NewAddressSet);
        List<Address> Update(List<Address> NewAddressSet);
        List<Address> Delete(int Id);
    }

}
