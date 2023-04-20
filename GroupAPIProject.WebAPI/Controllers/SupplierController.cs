using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.Supplier;
using GroupAPIProject.Services.Supplier;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GroupAPIProject.WebAPI.Controllers
{   

    
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [Authorize(Policy = "CustomAdminEntity")]
        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _supplierService.CreateSupplierAsync(model))
            {
                return Ok("Supplier added to database");
            }
            return BadRequest("Supplier could not be added to database");
        }

        [Authorize(Policy = "CustomAdminEntity")]
        [HttpDelete]
        public async Task<IActionResult> RemoveSupplierAsync(int SupplierId)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _supplierService.RemoveSupplierAsync(SupplierId))
            {
                return Ok("Supplier has been deleted");
            }
            return BadRequest("Supplier could not be deleted");
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplierByIdAsync([FromRoute]int SupplierId)
        {
           var SupplierToDisplay = await _supplierService.GetSupplierByIdAsync(SupplierId);
            return Ok(SupplierToDisplay);
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplierListAsync()
        {
           var SuppliersToDisplay = await _supplierService.GetSupplierListAsync();
            return Ok(SuppliersToDisplay);
        }
    }
}