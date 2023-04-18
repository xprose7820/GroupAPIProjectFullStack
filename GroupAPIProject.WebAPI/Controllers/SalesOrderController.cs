using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupAPIProject.WebAPI.Controllers
{
    [Authorize("Roles=RetailerEntity")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {

    }
}