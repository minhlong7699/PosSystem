using Contract.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Payment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    /// <summary>
    /// API for managing payments.
    /// </summary>
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PaymentController(IServiceManager service)
        {
            _service = service;
        }
        

        /// <summary>
        /// Get all payments. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <returns>
        /// Returns a collection of all payments.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPayment()
        {
            var payments = await _service.PaymentService.GetAllPaymentAsync(trackChanges: false);
            return Ok(payments);
        }

        /// <summary>
        /// Get a specific payment by its ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="paymentId">The unique ID of the payment to retrieve.</param>
        /// <returns>
        /// Returns the requested payment.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("{paymentId:Guid}", Name = "GetPayemnt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayment(Guid paymentId)
        {
            var payment = await _service.PaymentService.GetPaymentAsync(paymentId, trackChanges: false);
            return Ok(payment);
        }

        /// <summary>
        /// Create a new payment. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="paymentCreate">Data for creating a new payment.</param>
        /// <returns>
        /// Returns the newly created payment.
        /// Returns status code 201 (Created) upon successful creation.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePayment([FromBody]PaymentUpdateCreateDto paymentCreate)
        {
            var payment = await _service.PaymentService.CreatePaymentAsync(paymentCreate);
            return CreatedAtRoute("GetPayemnt", new { payment.PaymentId }, payment);
        }

        /// <summary>
        /// Update an existing payment by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="paymentId">The unique ID of the payment to update.</param>
        /// <param name="paymentUpdate">Data for updating the payment.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful update.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("paymentId:guid")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePayment(Guid paymentId, PaymentUpdateCreateDto paymentUpdate)
        {
            await _service.PaymentService.UpdatePaymentAsync(paymentId, paymentUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing payment by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="paymentId">The unique ID of the payment to delete.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful deletion.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePayment(Guid paymentId)
        {
            await _service.PaymentService.DeletePaymentAsync(paymentId, trackChanges: false);
            return NoContent();
        }

    }
}
