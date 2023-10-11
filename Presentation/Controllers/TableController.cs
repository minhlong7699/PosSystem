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
    [Route("api/tables")]
    public class TableController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TableController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTable([FromQuery]TableParameters tableParameters)
        {
            var pagedResult = await _service.TablesService.GetAllTableAsync(tableParameters,trackChanges: false);
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(pagedResult.metadata));
            return Ok(pagedResult.tables);
        }

        [HttpGet("{tableId:guid}", Name = "GetTable")]
        public async Task<IActionResult> GetTable(Guid tableId)
        {
            var table = await _service.TablesService.GetTableAsync(tableId, trackChanges: false);
            return Ok(table);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody]TableUpdateCreateDto tableCreate)
        {
            var table = await _service.TablesService.CreateTableAsync(tableCreate);
            return CreatedAtRoute("GetTable", new {table.TableID}, table);
        }
    }
}
