using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PaymentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayment()
        {
            var payments = await _service.PaymentService.GetAllPaymentAsync(trackChanges: false);
            return Ok(payments);
        }

        [HttpGet("{paymentId:Guid}", Name = "GetPayemnt")]
        public async Task<IActionResult> GetPayment(Guid paymentId)
        {
            var payment = await _service.PaymentService.GetPaymentAsync(paymentId, trackChanges: false);
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody]PaymentUpdateCreateDto paymentCreate)
        {
            var payment = await _service.PaymentService.CreatePaymentAsync(paymentCreate);
            return CreatedAtRoute("GetPayemnt", new { payment.PaymentId }, payment);
        }

        [HttpPut("paymentId:guid")]
        public async Task<IActionResult> UpdatePayment(Guid paymentId, PaymentUpdateCreateDto paymentUpdate)
        {
            await _service.PaymentService.UpdatePaymentAsync(paymentId, paymentUpdate, trackChanges: true);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePayment(Guid paymentId)
        {
            await _service.PaymentService.DeletePaymentAsync(paymentId, trackChanges: false);
            return NoContent();
        }

    }
}
