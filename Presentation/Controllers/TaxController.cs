using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/taxes")]
    public class TaxController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TaxController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaxes([FromQuery]TaxParameters taxParameters)
        {
            var pagedResult = await _service.TaxService.GetAllTaxesAsync(taxParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metadata));

            return Ok(pagedResult.taxes);
        }

        [HttpGet("{taxId:guid}", Name = "getTax")]
        public async Task<IActionResult> GetTax(Guid taxId)
        {
            var tax = await _service.TaxService.GetTaxAsync(taxId, trackChanges: false);
            return Ok(tax);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTax(TaxCreateUpdateDto taxCreate)
        {
            var tax = await _service.TaxService.CreateTax(taxCreate);
            return CreatedAtRoute("getTax", new {tax.TaxId}, tax);
        }

    }
}
