using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.PurchaseOrder;
using GroupAPIProject.Services.PurchaseOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupAPIProject.WebAPI.Controllers
{
    [Authorize("Roles=RetailerEntity")]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService){
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOrder(PurchaseOrderCreate model){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if(await _purchaseOrderService.CreatePurchaseOrderAsync(model)){
                return Ok("ProductOrder added to Retailer");
            }
            return BadRequest("ProductOrder not added to Retailer");

        }
        
    }
}