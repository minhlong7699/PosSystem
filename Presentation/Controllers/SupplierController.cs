using Contract.Service;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Supplier;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    /// <summary>
    /// API for managing suppliers.
    /// </summary>
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IServiceManager _service;
        public SupplierController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all suppliers. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="supplierParameter">Parameters for filtering and paging suppliers.</param>
        /// <returns>
        /// Returns a collection of all suppliers.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSuppliers([FromQuery]SupplierParameter supplierParameter)
        {
            var pagedResult = await _service.SupplierService.GetAllSuppliersAsync(supplierParameter, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metadata));
            return Ok(pagedResult.suppliers);
        }

        /// <summary>
        /// Get a specific supplier by its ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="supplierId">The unique ID of the supplier to retrieve.</param>
        /// <returns>
        /// Returns the requested supplier.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("{supplierId:guid}", Name = "getSupplier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSupplier(Guid supplierId)
        {
            var supplier = await _service.SupplierService.GetSupplierAsync(supplierId, trackChanges: false);
            return Ok(supplier);
        }

        /// <summary>
        /// Create a new supplier. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="supplierCreate">Data for creating a new supplier.</param>
        /// <returns>
        /// Returns the newly created supplier.
        /// Returns status code 201 (Created) upon successful creation.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSupplier(SupplierCreateUpdateDto supplierCreate)
        {
            var supplier = await _service.SupplierService.CreateSupplierAsync(supplierCreate);
            return CreatedAtRoute("getSupplier", new { supplier.SupplierId }, supplier);
        }

        /// <summary>
        /// Update an existing supplier by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="supplierId">The unique ID of the supplier to update.</param>
        /// <param name="supplierUpdate">Data for updating the supplier.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful update.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("{supplierId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateSupplier(Guid supplierId, SupplierCreateUpdateDto supplierUpdate)
        {
            await _service.SupplierService.UpdateSupplierAsync(supplierId, supplierUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing supplier by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="supplierId">The unique ID of the supplier to delete.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful deletion.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSupplier(Guid supplierId)
        {
            await _service.SupplierService.DeleteSupplierAsync(supplierId, trackChanges: false);
            return NoContent();
        }


    }
}
