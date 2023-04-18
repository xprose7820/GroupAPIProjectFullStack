using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.PurchaseOrderItem;
using GroupAPIProject.Services.PurchaseOrderItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupAPIProject.WebAPI.Controllers
{
    [Authorize("Roles=RetailerEntity")]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderItemController : ControllerBase
    {
        private readonly IPurchaseOrderItemService _purchaseOrderItemService;
        public PurchaseOrderItemController(IPurchaseOrderItemService purchaseOrderItemService)
        {
            _purchaseOrderItemService = purchaseOrderItemService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrderItem(PurchaseOrderItemCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _purchaseOrderItemService.CreatePurchaseOrderItemAsync(model))
            {
                return Ok("ProductOrderItem added to Retailer");
            }
            return BadRequest("ProductOrderItem not added to Retailer");
        }

    }
}