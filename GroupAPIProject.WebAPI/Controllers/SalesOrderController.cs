using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GroupAPIProject.Services.SalesOrder;
using GroupAPIProject.Models.SalesOrder;

namespace GroupAPIProject.WebAPI.Controllers
{
    [Authorize("Roles=RetailerEntity")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {

        private readonly ISalesOrderService _salesOrderService;
        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSalesOrder([FromBody] SalesOrderCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _salesOrderService.CreateSalesOrderAsync(model))
            {
                return Ok("ProductOrderItem added to Retailer");
            }
            return BadRequest("ProductOrderItem not added to Retailer");
        }
    }
}