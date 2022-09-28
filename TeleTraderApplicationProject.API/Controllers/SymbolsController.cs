using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeleTraderApplicationProject.Application.Dto;
using TeleTraderApplicationProject.Application.UseCases.Queries;
using TeleTraderApplicationProject.Implementation.UseCases;

namespace TeleTraderApplicationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymbolsController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IWebHostEnvironment _environment;
        private string _path;
        public SymbolsController(UseCaseHandler handler, IWebHostEnvironment environment)
        {
            this._handler = handler;
            this._environment = environment;
            this._path = string.Concat(this._environment.WebRootPath, "/cryptos.xml"); 
        }
        [HttpGet("all")]
        public IActionResult Get([FromServices] IGetAllSymbolsQuery query)
        {
            try
            {
                return Ok(this._handler.HandleQueryWithRequestData(query, this._path));
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("quotes")]
        public IActionResult Get([FromQuery] SearchSymbolsDto dto, [FromServices] IGetSumbolsWithQuotesQuery query)
        {
            try
            {
                var data = new SymbolsIdsAndPathDto()
                {
                    Id = dto.Id,
                    Path = this._path
                };
                var result =  this._handler.HandleQueryWithRequestData(query, data).Result;
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
