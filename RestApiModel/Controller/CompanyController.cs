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

namespace RestApiModel.Controller
{
    [Produces("application/json")]
    [Route("api/company")]
    public class CompanyController : Microsoft.AspNetCore.Mvc.Controller
    {        // GET api/values
        private readonly ICompanyRepository _companyRepo;
        private readonly Authorization _authorization;

        public CompanyController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
            _authorization = new Authorization();
        }


        [HttpGet()]                                                             //Read
        public IActionResult GetCompany()
        {
            _authorization.check(Request.Headers["Authorization"]);
            try
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
            catch (RepoException ex)
            {
                switch (ex.Type)
                {
                    case EnumResultTypes.INVALIDARGUMENT:
                        return StatusCode(StatusCodes.Status400BadRequest);
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


        [HttpGet("{id}")]                                                       //Read
        public IActionResult GetCompany(int Id)
        {
            try
            {
                var Company = _companyRepo.Read(Id);
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
            catch (Helper.RepoException ex)
            {
                switch (ex.Type)
                {
                    case EnumResultTypes.INVALIDARGUMENT:
                        return StatusCode(StatusCodes.Status400BadRequest);
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
            catch (NullReferenceException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                ErrHandler.Go(ex,null,null);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }


        [HttpPost()]                                                            //Create
        public IActionResult Add([FromBody] Company value)
        {
            try
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
            catch (Helper.RepoException ex)
            {
                switch (ex.Type)
                {
                    case EnumResultTypes.INVALIDARGUMENT:
                        return StatusCode(StatusCodes.Status400BadRequest);
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
            catch (NullReferenceException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                ErrHandler.Go(ex, null, null);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }


        [HttpPut()]                                                       //Update
        public IActionResult Update([FromBody] Model.Company value)
        {
            try
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
            }
            catch (Helper.RepoException ex)
            {
                switch (ex.Type)
                {
                    case EnumResultTypes.INVALIDARGUMENT:
                        return StatusCode(StatusCodes.Status400BadRequest);
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
            catch (NullReferenceException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                ErrHandler.Go(ex, null, null);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
            
        }


        [HttpDelete()]                                                       //Update
        public IActionResult Delete([FromBody] Model.Company value)
        {
            try
            {
                int Id = value.Id;
                if (Id != 0)
                {
                    int Company = _companyRepo.Update(Id, null, true);
                    if (Company < 1)
                    {
                        return StatusCode(StatusCodes.Status206PartialContent);
                    }
                    else if (Company == 1)
                    {
                        return StatusCode(StatusCodes.Status200OK, Company);
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
            }
            catch (Helper.RepoException ex)
            {
                switch (ex.Type)
                {
                    case EnumResultTypes.INVALIDARGUMENT:
                        return StatusCode(StatusCodes.Status400BadRequest);
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
            catch (NullReferenceException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                ErrHandler.Go(ex, null, null);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
    }
}