using Contract.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.Table;
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
    /// API for managing tables.
    /// </summary>
    [ApiController]
    [Route("api/tables")]
    public class TableController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TableController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all tables. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="tableParameters">Parameters for filtering and paging tables.</param>
        /// <returns>
        /// Returns a collection of all tables.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTable([FromQuery]TableParameters tableParameters)
        {
            var pagedResult = await _service.TablesService.GetAllTableAsync(tableParameters,trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metadata));
            return Ok(pagedResult.tables);
        }

        /// <summary>
        /// Get a specific table by its ID. This API requires authentication and is role-based (Admin, User)
        /// </summary>
        /// <param name="tableId">The unique ID of the table to retrieve.</param>
        /// <returns>
        /// Returns the requested table.
        /// Returns status code 200 (OK).
        /// </returns>
        /// 
        [Authorize(Roles = "User")]
        [HttpGet("{tableId:guid}", Name = "GetTable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTable(Guid tableId)
        {
            var table = await _service.TablesService.GetTableAsync(tableId, trackChanges: false);
            return Ok(table);
        }

        /// <summary>
        /// Create a new table. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="tableCreate">Data for creating a new table.</param>
        /// <returns>
        /// Returns the newly created table.
        /// Returns status code 201 (Created) upon successful creation.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTable([FromBody]TableUpdateCreateDto tableCreate)
        {
            var table = await _service.TablesService.CreateTableAsync(tableCreate);
            return CreatedAtRoute("GetTable", new {table.TableID}, table);
        }

        /// <summary>
        /// Update an existing table by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="tableId">The unique ID of the table to update.</param>
        /// <param name="tableUpdate">Data for updating the table.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful update.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpPut("{tableId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTable(Guid tableId,TableUpdateCreateDto tableUpdate)
        {
            await _service.TablesService.UpdateTableAsync(tableId, tableUpdate, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete an existing table by its ID. This API requires authentication and is role-based (Admin)
        /// </summary>
        /// <param name="tableId">The unique ID of the table to delete.</param>
        /// <returns>
        /// Returns status code 204 (No Content) upon successful deletion.
        /// </returns>
        /// 
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTable(Guid tableId)
        {
            await _service.TablesService.DeleteTableAsync(tableId, trackChanges: true);
            return NoContent();
        }
    }
}
