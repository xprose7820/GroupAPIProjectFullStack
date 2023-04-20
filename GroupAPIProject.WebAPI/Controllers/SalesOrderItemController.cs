using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.SalesOrderItem;
using GroupAPIProject.Services.SalesOrderItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupAPIProject.WebAPI.Controllers
{
    [Authorize("Roles=RetailerEntity")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderItemController : ControllerBase
    {
        private readonly ISalesOrderItemService _salesOrderItemService;
        public SalesOrderItemController(ISalesOrderItemService salesOrderItemService)
        {
            _salesOrderItemService = salesOrderItemService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSalesOrderItem([FromBody] SalesOrderItemCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _salesOrderItemService.CreateSalesOrderItemAsync(model))
            {
                return Ok("ProductOrderItem added to Retailer");
            }
            return BadRequest("ProductOrderItem not added to Retailer");
        }
    }
}
