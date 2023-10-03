using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IServiceManager _service;
        public SupplierController(IServiceManager service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _service.SupplierService.GetAllSuppliersAsync(trackChanges: false);
            return Ok(suppliers);
        }

        [HttpGet("{supplierId:guid}")]
        public async Task<IActionResult> GetSupplier(Guid supplierId)
        {
            var supplier = await _service.SupplierService.GetSupplierAsync(supplierId ,trackChanges: false);
            return Ok(supplier);
        }
    }
}
