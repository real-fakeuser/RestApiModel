using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApiModel.Stores;
using RestApiModel.Repository;
using Microsoft.AspNetCore.Http;
using RestApiModel.Model;
using RestApiModel.Helper;

namespace RestApiModel.Controllers
{
    [Route("api/company")]
    public class CompanyController : Controller
    {
        CompanyRepo repo = new CompanyRepo();


        [HttpGet()]
        public IActionResult GetCompany()
        {
            List<Model.Company> dt = repo.Read();
            
            

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
            List<Model.Company> dt = repo.Read(Id);
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
        public IActionResult Add([FromBody] Model.Company value)
        {
            string name = value.Name;
            bool bsuccess = false;
            try
            {
                bsuccess = repo.CreateCompany(name);
                if (bsuccess)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (Helper.RepoException ex)
            {
                switch (ex.Type)
                {
                    case EnumResultTypes.OK:
                        break;
                    case EnumResultTypes.INVALIDARGUMENT:
                        return StatusCode(StatusCodes.Status405MethodNotAllowed);
                    case EnumResultTypes.NOTFOUND:
                        return StatusCode(StatusCodes.Status204NoContent);
                    case EnumResultTypes.SQLERROR:
                        return StatusCode(StatusCodes.Status408RequestTimeout);
                    case EnumResultTypes.ERROR:
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    default:
                        return StatusCode(StatusCodes.Status501NotImplemented);
                }
            }
            return StatusCode(StatusCodes.Status501NotImplemented);
        }


        [HttpPatch()]
        public IActionResult Update([FromBody] Model.Company value)
        {
            bool bsuccess = repo.UpdateCompany(value);
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


            bool bsuccess = repo.DeleteCompany(value);
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