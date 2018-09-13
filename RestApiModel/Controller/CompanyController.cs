using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApiModel.Stores;
using RestApiModel.Repository;
using Microsoft.AspNetCore.Http;

namespace RestApiModel.Controllers
{
    [Route("api/company")]
    public class CompanyController : Controller
    {
        CompanyRepo getData = new CompanyRepo();


        [HttpGet()]
        public IActionResult GetCompany()
        {
            List<Model.Company> dt = getData.Read();
            return dt != null ? StatusCode(StatusCodes.Status200OK, dt) : StatusCode(StatusCodes.Status204NoContent, null);
        }
        [HttpGet("{id}")]
        public IActionResult GetCompany(int Id)
        {
            List<Model.Company> dt = getData.Read(Id);
            return dt != null ? StatusCode(StatusCodes.Status200OK, dt) : StatusCode(StatusCodes.Status204NoContent, null);
        }

    }
}