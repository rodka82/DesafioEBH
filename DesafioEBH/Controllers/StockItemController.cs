using API.Utils;
using Application.Services;
using Application.Utils;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockItemController : ApplicationBaseController<StockItemDTO>
    {
        private readonly IStockItemService _storeService;

        public StockItemController(IStockItemService storeService, IMapper mapper, ILogger<StockItemController> logger)
            : base(mapper, logger)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var stockItem = _storeService.GetById(id);
            var dto = _mapper.Map<StockItemDTO>(stockItem);
            var response = ReturnSuccessResponse(dto);
            return SetStatusCodeForSearch(response);
        }

        [HttpPost]
        public IActionResult Save(StockItem store)
        {
            IApplicationResponse result = new ApplicationResponse();

            try
            {
                result = _storeService.Save(store);
                return SetStatusCode(result);
            }
            catch (Exception e)
            {
                AddUserFriendlyErrorMessage(result);
                LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }

        [HttpPut]
        public IActionResult Update(StockItem stockItem)
        {
            if (stockItem.Id == 0)
            {
                IApplicationResponse result = new ApplicationResponse();
                result.IsValid = false;
                result.Messages = new List<string>() { "Informe um Id para alteração" };

                return BadRequest(result);
            }

            return Save(stockItem);
        }

        [HttpDelete]
        public IActionResult Delete(StockItem stockItem)
        {
            IApplicationResponse result = new ApplicationResponse();

            try
            {
                result = _storeService.Delete(stockItem);
                return SetStatusCode(result);
            }
            catch (Exception e)
            {
                AddUserFriendlyErrorMessage(result);
                LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }
    }
}
