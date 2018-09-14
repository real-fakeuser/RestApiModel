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


namespace RestApiModel.Repository
{
    public class CompanyRepo
    {
        public SqlConnection DefineSqlConn()
        {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Properties.Resources.ConnectionStringTappqa;
                return conn;
            
        }



        public List<Model.Company> Read()
        {
            SqlConnection conn = DefineSqlConn();
            string sqlStatement = @"SELECT Id,
                                            Company AS Name
                                    FROM viCompany;";
            var result = conn.Query<Model.Company>(sqlStatement).ToList();
            return result;
        }
        public List<Model.Company> Read(int Id)
        {

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            SqlConnection conn = DefineSqlConn();
            string sqlStatement = @"SELECT Id,
                                            Company AS Name
                                    FROM viCompany
                                    WHERE Id = @Id;";
            var result = conn.Query<Model.Company>(sqlStatement, param).ToList();
            return result;
        }
        public bool CreateCompany(string Name)
        {
            try
            {
                SqlConnection conn = DefineSqlConn();
                string query = "spCreateOrUpdateCompany";
                var param = new DynamicParameters();
                param.Add("@Name", Name);
                var result = conn.Execute(query, param, null, null, CommandType.StoredProcedure);
                return result > 0;
                
            }
            catch(SqlException)
            {
                throw new RepoException(EnumResultTypes.SQLERROR);
            }
            catch(Exception)
            {
                throw new RepoException(EnumResultTypes.ERROR);
            }
        }

        public bool UpdateCompany(Model.Company value)
        {
            SqlConnection conn = DefineSqlConn();
            string query = "spCreateOrUpdateCompany";
            var param = new DynamicParameters();
            param.Add("@Id", value.Id);
            param.Add("@Name", value.Name);
            param.Add("@Delete", 0);
            var result = conn.Execute(query, param, null, null, CommandType.StoredProcedure);
            return result > 0; 
        }

        public bool DeleteCompany(Model.Company value)
        {
            SqlConnection conn = DefineSqlConn();
            string query = "spCreateOrUpdateCompany";
            var param = new DynamicParameters();
            param.Add("@Id", value.Id);
            param.Add("@Name", null);
            param.Add("@Delete", 1);
            var result = conn.Execute(query, param, null, null, CommandType.StoredProcedure);
            return result > 0;
        }

    }
}


