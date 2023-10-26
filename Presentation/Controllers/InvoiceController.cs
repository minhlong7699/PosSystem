using Contract.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Invoice;
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
    /// API for managing invoices.
    /// </summary>
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IServiceManager _service;

        public InvoiceController(IServiceManager service)
        {
            _service = service;
        }


        /// <summary>
        /// Get all invoices with optional filtering. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="invoiceParameter">Parameters for filtering and pagination.</param>
        /// <returns>
        /// Returns a collection of invoices.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInvoice([FromQuery]InvoiceParameter invoiceParameter)
        {
            var result = await _service.InvoiceService.GetAllInvoiceAsync(invoiceParameter, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.metadata));
            return Ok(result.invoices);
        }

        /// <summary>
        /// Get a specific invoice by its ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="invoiceId">The unique ID of the invoice to retrieve.</param>
        /// <returns>
        /// Returns the requested invoice.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("invoiceId:guid", Name = "getInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvoice(Guid invoiceId)
        {
            var invoice = await _service.InvoiceService.GetInvoiceAsync(invoiceId, trackChanges: false);
            return Ok(invoice);
        }

        /// <summary>
        /// Create a new invoice. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="invoiceCreate">Data for creating a new invoice.</param>
        /// <returns>
        /// Returns the newly created invoice.
        /// Returns status code 201 (Created) upon successful creation.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateInvoice(InvoiceCreateUpdateDto invoiceCreate)
        {
            var invoice = await _service.InvoiceService.CreateInvoiceAsync(invoiceCreate, trackChanges: true);
            return CreatedAtRoute("getInvoice", new { invoice.InvoiceId }, invoice);
        }

        /// <summary>
        /// Update an existing invoice. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to update.</param>
        /// <param name="invoiceUpdate">Data for updating the invoice.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful update.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("{invoiceId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateInvoice(Guid invoiceId, InvoiceUpdateDto invoiceUpdate)
        {
            await _service.InvoiceService.UpdateInvoiceAsync(invoiceId, invoiceUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing invoice. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to delete.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful deletion.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteInvoice(Guid invoiceId)
        {
            await _service.InvoiceService.DelteInvoiceAsync(invoiceId, trackChanges: false);
            return NoContent();
        }

    }
}
