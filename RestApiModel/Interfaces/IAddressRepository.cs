﻿using System;
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
        bool Add(Model.Address value);
        bool Update(Model.Address value);
        int Delete(int Id);
    }

}
