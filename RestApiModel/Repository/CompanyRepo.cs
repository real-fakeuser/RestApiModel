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
    public class CompanyRepo : ICompanyRepository
    {

        IDbContext _dbContext;
        public CompanyRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<Company> Read()
        {
            try
            {
                List<Company> retVal;
                var con = _dbContext.GetCompany();
                string companySelect = "SELECT  Id, " +
                    "                           Company AS Name " +
                    "                   FROM viCompany;";
                using (con)
                {
                    retVal = con.Query<Company>(companySelect).ToList();
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
        }


        public List<Company> Read(int Id)
        {
            try
            {
                List<Company> retVal;
                var con = _dbContext.GetCompany();
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", Id);
                string companySelect = @"   SELECT  Id, 
                                                Company AS Name 
                                        FROM    viCompany
                                        WHERE   Id = @Id;";
                using (con)
                {
                    retVal = con.Query<Company>(companySelect, param).ToList();
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
        }


        public int Add(string Name)
        {
            try
            {
                int retVal;
                var con = _dbContext.GetCompany();
                string query = "spCreateOrUpdateCompany";
                var param = new DynamicParameters();
                param.Add("@Name", Name);
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


        public int Update(int Id, string name, bool delete)
        {
            try
            {
                int retVal;
                var con = _dbContext.GetCompany();
                string query = "spCreateOrUpdateCompany";
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
    }
}


