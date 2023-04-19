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
    [Authorize("Roles=AdminEntity")]
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

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
    }
}