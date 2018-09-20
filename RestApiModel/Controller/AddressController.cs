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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TobitLogger.Core;
using TobitLogger.Core.Models;
using TobitWebApiExtensions.Extensions;

namespace RestApiModel.AddressController
{
    [Produces("application/json")]
    [Route("api/address")]
    [ApiController]
    public class AddressController : Microsoft.AspNetCore.Mvc.Controller
    {        // GET api/values
        private readonly IAddressRepository _AddressRepo;

        private readonly ILogger<AddressController> _logger;


        public AddressController(IAddressRepository AddressRepo, ILoggerFactory loggerFactory)
        {
            _AddressRepo = AddressRepo;
            _logger = loggerFactory.CreateLogger<AddressController>();
        }

        [Authorize(Roles = "1")]
        [HttpGet()]                                                             //Read
        public IActionResult GetAddress()
        {
            try
            {
                throw new ArgumentException();
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
            //catch (Helper.RepoException ex)
            catch (Exception ex)
            {
                var logObj = new ExceptionData(ex);
                logObj.CustomNumber = 1111;
                logObj.CustomText = "###testException###";
                logObj.Add("start_time", DateTime.UtcNow);
                logObj.Add("myObject", new
                {
                    TappId = 15,
                    Name = "NoName"
                });
                _logger.Error(logObj);
                return StatusCode(StatusCodes.Status501NotImplemented);

                /*
                switch (exs.Type)
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
                }*/
            }
            throw new Exception("New exception");

        }

        [Authorize(Roles = "1")]
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
            catch (Exception ex)
            {
                var logObj1 = new ExceptionData(ex);
                logObj1.CustomNumber = 123;
                logObj1.CustomText = "abs";
                logObj1.Add("start_time", DateTime.UtcNow);
                logObj1.Add("myObject", new
                {
                    TappId = 15,
                    Name = "Sebastian"
                });
                _logger.Error(logObj1);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }

        [Authorize(Roles = "1")]
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
            catch (Exception ex)
            {
                var logObj2 = new ExceptionData(ex);
                logObj2.CustomNumber = 123;
                logObj2.CustomText = "abs";
                logObj2.Add("start_time", DateTime.UtcNow);
                logObj2.Add("myObject", new
                {
                    TappId = 15,
                    Name = "Sebastian"
                });
                _logger.Error(logObj2);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }

        [Authorize(Roles = "1")]
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
            catch (Exception ex)
            {
                var logObj3 = new ExceptionData(ex);
                logObj3.CustomNumber = 123;
                logObj3.CustomText = "abs";
                logObj3.Add("start_time", DateTime.UtcNow);
                logObj3.Add("myObject", new
                {
                    TappId = 15,
                    Name = "Sebastian"
                });
                _logger.Error(logObj3);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }

        [Authorize(Roles = "1")]
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
            catch (Exception ex)
            {
                var logObj4 = new ExceptionData(ex);
                logObj4.CustomNumber = 123;
                logObj4.CustomText = "abs";
                logObj4.Add("start_time", DateTime.UtcNow);
                logObj4.Add("myObject", new
                {
                    TappId = 15,
                    Name = "Sebastian"
                });
                _logger.Error(logObj4);
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }
    }
}