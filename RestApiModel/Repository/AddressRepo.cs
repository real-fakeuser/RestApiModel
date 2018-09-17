using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using RestApiModel.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RestApiModel.Helper;
using RestApiModel.Interfaces;

namespace RestApiModel.Repository
{
    public class AddressRepo : IAddressRepository
    {

        IDbContext _dbContext;
        public AddressRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        const string _ReadAll = @"SELECT Id, 
                                                Street,
                                                City,
                                                ZipCode,
                                                CountryCode
                                       FROM viAddress;";
        const string _ReadById = @"SELECT Id, 
                                                Street,
                                                City,
                                                ZipCode,
                                                CountryCode
                                       FROM viAddress
                                       WHERE   Id = @Id;";
        public List<Address> Read()
        {
            return _ReadDataset();
        }
        public List<Address> Read(int Id)
        {
            return _ReadDataset(_ReadById, Id);
        }
        public List<Address> Create(List<Address> NewAddressSet)
        {
            if(_WriteOrUpdate("Create", 0, NewAddressSet.Street, NewAddressSet.ZipCode, NewAddressSet.City, NewAddressSet.CountryCode))
            {
                return _ReadDataset();
            }
            else
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }
        }
        public List<Address> Update(List<Address> NewAddressSet)
        {
            if (_WriteOrUpdate("Update", NewAddressSet.Id , NewAddressSet.Street, NewAddressSet.ZipCode, NewAddressSet.City, NewAddressSet.CountryCode))
            {
                return _ReadDataset();
            }
            else
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }
        }
        public List<Address> Delete(int Id)
        {
            if (_WriteOrUpdate("Delete", Id))
            {
                return _ReadDataset();
            }
            else
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }
        }
        private List<Address> _ReadDataset(string _query = _ReadAll, int Id = -1)
        {
            List<Address> retVal;
            var con = _dbContext.GetCompany();
            DynamicParameters param = new DynamicParameters();


            if (Id != -1)
            {
                param.Add("@Id", Id);
            }
            using (con)
            {
                retVal = con.Query<Address>(_query, param).ToList();
            }
            return retVal;

        }
        private bool _WriteOrUpdate(string mode = "None", int Id = -1, string street = null, string ZipCode = null, string City = null, string CountryCode = null)
        {
            string query = "spCreateOrUpdateAddress";
            var param = new DynamicParameters();
            switch (mode)
            {
                case "Create":
                    break;
                case "Update":
                    param.Add("@Id", Id);
                    break;
                case "Delete":
                    param.Add("@Id", Id);
                    param.Add("@Delete", true);
                    break;
                case "None":
                    throw new RepoException(EnumResultTypes.ERROR);
                default:
                    throw new RepoException(EnumResultTypes.ERROR);
            }
            param.Add("@Street", street);
            param.Add("@ZipCode", ZipCode);
            param.Add("@City", City);
            param.Add("@CountryCode", CountryCode);
            var con = _dbContext.GetCompany();
            int retVal;
            retVal = con.Execute(query, param, null, null, CommandType.StoredProcedure);
            return retVal > 0;
        }

    }
}


