using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using RestApiModel.Model;



namespace RestApiModel.Repository
{
    public class CompanyRepo
    {
        public List<Model.Company> Read(SqlConnection conn)
        {
            string sqlStatement = @"SELECT Id,
                                            Company AS Name
                                    FROM viCompany;";
            var result = conn.Query<Company>(sqlStatement).ToList();
            return result;
        }



    }
}


