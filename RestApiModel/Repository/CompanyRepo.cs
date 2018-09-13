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



    }
}


