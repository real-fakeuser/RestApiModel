using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestApiModel.Repository;
using Microsoft.AspNetCore.Http;
using RestApiModel.Model;
using RestApiModel.Helper;
using RestApiModel.Interfaces;

namespace RestApiModel.Controllers
{
    [Produces("application/json")]
    [Route("api/company")]
    public class CompanyController : Controller
    {        // GET api/values
        private readonly ICompanyRepository _companyRepo;

        public CompanyController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        //CompanyRepo repo = new CompanyRepo();
        [HttpGet()]                                                             //Read
        public IActionResult GetCompany()
        {
            var names = _companyRepo.Read();
            if (names.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, names);
            }
            else
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
        }

        [HttpGet("{id}")]                                                       //Read
        public IActionResult GetCompany(int Id)
        {
            var Company = _companyRepo.Read(Id);
            //Console.WriteLine("##Debugging value returned list length: " + Convert.ToString(Company.Count));
            if (Company.Count == 1)
            {
                return StatusCode(StatusCodes.Status302Found, Company);
            }
            else if (Company.Count < 1)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else if (Company.Count > 1)
            {
                return StatusCode(StatusCodes.Status207MultiStatus);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }
        [HttpPost()]                                                            //Create
        public IActionResult Add([FromBody] Company value)
        {
            string name = value.Name;
            if (name.Length < 1)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            int Company = _companyRepo.Add(name);
            if (Company < 1)
            {
                return StatusCode(StatusCodes.Status206PartialContent);
            }
            else if (Company == 1)
            {
                return StatusCode(StatusCodes.Status201Created, Company);
            }
            else
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed);
            }

        }


        [HttpPatch()]                                                       //Update
        public IActionResult Update([FromBody] Model.Company value)
        {
            int Id = value.Id;
            string name = value.Name;
            if (Id != 0 && name != null)
            {
                int Company = _companyRepo.Update(Id, name, false);
                if (Company < 1)
                {
                    return StatusCode(StatusCodes.Status206PartialContent);
                }
                else if (Company == 1)
                {
                    return StatusCode(StatusCodes.Status201Created, Company);
                }
                else
                {
                    return StatusCode(StatusCodes.Status501NotImplemented);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }
        
            /*bool bsuccess = repo.UpdateCompany(value);
            if (bsuccess)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }*/
        }

        /*
                        [HttpPost()]                                                            //Create
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
                        }


                        [HttpPatch()]                                                       //Update
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
                        [HttpDelete()]                                                       //Delete
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




                    */
    }
}