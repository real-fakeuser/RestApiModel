using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace RestApiModel.Controllers
{
    [Route("api/department")]

    public class DepartmentController : Controller
    {
        [HttpGet()]
        public JsonResult GetDepartment()
        {
            return new JsonResult(new List<object>()
            {
                new { id=1, Name="Rechnungswesen"},
                new { id=2, Name="Buchhaltung"}
            });
        }

    }
}