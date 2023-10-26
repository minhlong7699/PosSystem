using Contract.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Tax;
using Shared.RequestFeatures;
using System.Data;

namespace Presentation.Controllers
{
    /// <summary>
    /// API for managing taxes.
    /// </summary>
    [ApiController]
    [Route("api/taxes")]
    public class TaxController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TaxController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all taxes. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="taxParameters">Parameters for filtering and paging taxes.</param>
        /// <returns>
        /// Returns a collection of all taxes.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTaxes([FromQuery]TaxParameters taxParameters)
        {
            var pagedResult = await _service.TaxService.GetAllTaxesAsync(taxParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metadata));

            return Ok(pagedResult.taxes);
        }

        /// <summary>
        /// Get a specific tax by its ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="taxId">The unique ID of the tax to retrieve.</param>
        /// <returns>
        /// Returns the requested tax.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("{taxId:guid}", Name = "getTax")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTax(Guid taxId)
        {
            var tax = await _service.TaxService.GetTaxAsync(taxId, trackChanges: false);
            return Ok(tax);
        }

        /// <summary>
        /// Create a new tax. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="taxCreate">Data for creating a new tax.</param>
        /// <returns>
        /// Returns the newly created tax.
        /// Returns status code 201 (Created) upon successful creation.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTax(TaxCreateUpdateDto taxCreate)
        {
            var tax = await _service.TaxService.CreateTax(taxCreate);
            return CreatedAtRoute("getTax", new {tax.TaxId}, tax);
        }

        /// <summary> 
        /// Update an existing tax by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="taxId">The unique ID of the tax to update.</param>
        /// <param name="taxUpdate">Data for updating the tax.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful update.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("{taxId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTax(Guid taxId, TaxCreateUpdateDto taxUpdate)
        {
            await _service.TaxService.UpdateTaxAsync(taxId, taxUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing tax by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="taxId">The unique ID of the tax to delete.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful deletion.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTax(Guid taxId)
        {
            await _service.TaxService.DeleteTaxAsync(taxId, trackChanges: true);
            return NoContent();
        }
    }
}
