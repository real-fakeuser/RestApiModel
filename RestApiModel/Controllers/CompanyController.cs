using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApiModel.Stores;

namespace RestApiModel.Controllers
{
    [Route("api/company")]
    public class CompanyController : Controller
    {
        [HttpGet()]
        public JsonResult GetCompany()
        {
            return new JsonResult(tempFillModelsWithDummyData.Current.Company);
        }
        [HttpGet("{id}")]
        public JsonResult GetCompany(int id)
        {
            return new JsonResult(
                CompanyDataStore.Current.Company.FirstOrDefault(c => c.Id == id)
                );
        }
    }
}
