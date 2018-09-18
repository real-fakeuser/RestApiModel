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

namespace RestApiModel.AddressController
{
    [Produces("application/json")]
    [Route("api/address")]
    public class AddressController : Microsoft.AspNetCore.Mvc.Controller
    {        // GET api/values
        private readonly IAddressRepository _AddressRepo;

        public AddressController(IAddressRepository AddressRepo)
        {
            _AddressRepo = AddressRepo;
        }


        [HttpGet()]                                                             //Read
        public IActionResult GetAddress()
        {
            try
            {
                var names = _AddressRepo.Read();
                if (names.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, names);
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
        public IActionResult GetAddress(int Id)
        {
            try
            {
                var Address = _AddressRepo.Read(Id);
                if (Address.Count == 1)
                {
                    return StatusCode(StatusCodes.Status302Found, Address);
                }
                else if (Address.Count < 1)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                else if (Address.Count > 1)
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
                ErrHandler.Go(ex, null, null);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }


        [HttpPost()]                                                            //Create
        public IActionResult Add([FromBody] Address value)
        {
            try
            {
                if (value.Id != 0)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable);
                }
                List<Address> AddressList = _AddressRepo.Create(value);
                if (AddressList == null)
                {
                    return StatusCode(StatusCodes.Status206PartialContent);
                }
                else if (AddressList != null)
                {
                    return StatusCode(StatusCodes.Status201Created, AddressList);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
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
        public IActionResult Update([FromBody] Model.Address value)
        {
            try
            {
                List<Address> Address = _AddressRepo.Update(value);
                if (Address == null)
                {
                    return StatusCode(StatusCodes.Status206PartialContent);
                }
                else if (Address != null)
                {
                    return StatusCode(StatusCodes.Status201Created, Address);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
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
        public IActionResult Delete([FromBody] Model.Address value)
        {
            try
            {
                int Id = value.Id;
                if (Id != 0)
                {
                    int Address = _AddressRepo.Delete(Id);
                    if (Address < 1)
                    {
                        return StatusCode(StatusCodes.Status206PartialContent);
                    }
                    else if (Address == 1)
                    {
                        return StatusCode(StatusCodes.Status200OK, Address);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status501NotImplemented);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable);
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