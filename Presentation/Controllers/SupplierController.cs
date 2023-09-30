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
        public IActionResult GetSuppliers()
        {
            var suppliers = _service.SupplierService.GetAllSuppliers(trackChanges: false);
            return Ok(suppliers);
        }

        [HttpGet("{supplierId:guid}")]
        public IActionResult GetSupplier(Guid supplierId)
        {
            var supplier = _service.SupplierService.GetSupplier(supplierId ,trackChanges: false);
            return Ok(supplier);
        }
    }
}
