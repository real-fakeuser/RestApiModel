using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApiModel.Stores;
using RestApiModel.Repository;
using Microsoft.AspNetCore.Http;
using RestApiModel.Model;

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
            if (dt != null)
            {
                return StatusCode(StatusCodes.Status200OK, dt);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent, null);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(int Id)
        {
            List<Model.Company> dt = getData.Read(Id);
            if (dt != null)
            {
                return StatusCode(StatusCodes.Status200OK, dt);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent, null);
            }
        }

        [HttpPost()]
        public IActionResult AddCompany([FromBody] Model.Company value)
        {

            string name = value.Name;
            bool bsuccess = getData.CreateCompany(name);
            if (bsuccess)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }


        [HttpPatch()]
        public IActionResult Update([FromBody] Model.Company value)
        {


            bool bsuccess = getData.UpdateCompany(value);
            if (bsuccess)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        [HttpDelete()]
        public IActionResult Delete([FromBody] Model.Company value)
        {


            bool bsuccess = getData.DeleteCompany(value);
            if (bsuccess)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }





    }
}