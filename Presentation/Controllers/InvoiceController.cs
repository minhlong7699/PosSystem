using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IServiceManager _service;

        public InvoiceController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoice(InvoiceParameter invoiceParameter)
        {
            var result = await _service.InvoiceService.GetAllInvoiceAsync(invoiceParameter, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(result.metadata));
            return Ok(result.invoices);
        }

        [HttpGet("invoiceId:guid", Name = "getInvoice")]
        public async Task<IActionResult> GetInvoice(Guid invoiceId)
        {
            var invoice = await _service.InvoiceService.GetInvoiceAsync(invoiceId, trackChanges: false);
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(InvoiceCreateUpdateDto invoiceCreate)
        {
            var invoice = await _service.InvoiceService.CreateInvoiceAsync(invoiceCreate, trackChanges: true);
            return CreatedAtRoute("getInvoice", new { invoice.InvoiceId }, invoice);
        }

        [HttpPut("{invoiceId:guid}")]
        public async Task<IActionResult> UpdateInvoice(Guid invoiceId, InvoiceUpdateDto invoiceUpdate)
        {
            await _service.InvoiceService.UpdateInvoiceAsync(invoiceId, invoiceUpdate, trackChanges: true);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInvoice(Guid invoiceId)
        {
            await _service.InvoiceService.DelteInvoiceAsync(invoiceId, trackChanges: false);
            return NoContent();
        }

    }
}
