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



        public int Update(int Id, string name, bool delete)
        {
            try
            {
                int retVal;
                var con = _dbContext.GetAddress();
                string query = "spCreateOrUpdateAddress";
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                param.Add("@Name", name);
                param.Add("@Delete", delete);
                retVal = con.Execute(query, param, null, null, CommandType.StoredProcedure);
                return retVal;
            }
            catch (SqlException)
            {
                throw new RepoException(EnumResultTypes.SQLERROR);
            }
            catch (Exception)
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }
        }

        List<Address> IAddressRepository.Read()
        {
            try
            {
                List<Address> retVal;
                var con = _dbContext.GetAddress();
                string AddressSelect = @"SELECT Id, 
                                                Street,
                                                City,
                                                ZipCode,
                                                CountryCode
                                       FROM viAddress;";
                using (con)
                {
                    retVal = con.Query<Address>(AddressSelect).ToList();
                }
                return retVal;
            }
            catch (SqlException)
            {
                throw new RepoException(EnumResultTypes.SQLERROR);
            }
            catch (Exception)
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }
            throw new NotImplementedException();
        }

        List<Address> IAddressRepository.Read(int Id)
        {
            try
            {
                List<Address> retVal;
                var con = _dbContext.GetAddress();
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                string AddressSelect = @"SELECT Id, 
                                                Street,
                                                City,
                                                ZipCode,
                                                CountryCode
                                       FROM viAddress
                                       WHERE   Id = @Id;";
                using (con)
                {
                    retVal = con.Query<Address>(AddressSelect, param).ToList();
                }
                return retVal;
            }
            catch (SqlException)
            {
                throw new RepoException(EnumResultTypes.SQLERROR);
            }
            catch (Exception)
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }

            throw new NotImplementedException();
        }

        bool IAddressRepository.AddOrUpdate(Model.Address value)
        {
            try
            {
                if (value != null)
                {
                    bool retVal;
                    var con = _dbContext.GetAddress();
                    string query = "spCreateOrUpdateAddress";
                    var param = new DynamicParameters();
                    if (value.Id != 0)
                    {
                        param.Add("@Id", value.Id);
                    }
                    param.Add("@Street", value.Street);
                    param.Add("@City", value.City);
                    param.Add("@ZipCode", value.ZipCode);
                    param.Add("@CountryCode", value.CountryCode);
                    retVal = Convert.ToBoolean(con.Execute(query, param, null, null, CommandType.StoredProcedure));
                    return retVal;
                }
                else
                {
                    throw new RepoException(EnumResultTypes.NOHANDOVERCONTENT);
                }
            }
            catch (SqlException)
            {
                throw new RepoException(EnumResultTypes.SQLERROR);
            }
            catch (Exception)
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }

            throw new NotImplementedException();
        }
        int IAddressRepository.Delete(int Id)
        {

        }
    }
}


