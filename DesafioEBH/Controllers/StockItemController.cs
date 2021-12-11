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
    public class StockItemController : ApplicationBaseController
    {
        private readonly IStockItemService _storeService;

        public StockItemController(IStockItemService storeService, IMapper mapper, ILogger<ApplicationBaseController> logger)
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
            return Ok(response);
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
                _logger.LogError($"Ocorreu um erro interno: { e.Message } {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }

        [HttpPut]
        public IActionResult Update(StockItem stockItem)
        {
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
            catch (Exception)
            {
                AddUserFriendlyErrorMessage(result);
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }

        private IActionResult SetStatusCode(IApplicationResponse result)
        {
            if (result.Result != null)
                return Ok(ReturnApiResponse(result));
            else
                return BadRequest(ReturnApiResponse(result));
        }

        private static void AddUserFriendlyErrorMessage(IApplicationResponse result)
        {
            result.Messages.Add("Houve um erro ao processar sua solicitação");
        }

        private ApiResponse ReturnApiResponse(IApplicationResponse result)
        {
            var response = _mapper.Map<ApiResponse>(result);
            if (result.Result != null)
            {
                response.Result = _mapper.Map<StoreDTO>(result.Result);
                response.StatusCode = GetStatusCode(response);
            }
            else
            {
                response.StatusCode = 500;
            }
            return response;
        }

        private static int GetStatusCode(ApiResponse response)
        {
            return response.Messages.Any() ? 400 : 200;
        }

        private ApiResponse ReturnSuccessResponse(StockItemDTO dto)
        {
            return new ApiResponse
            {
                Result = dto,
                StatusCode = 200,
            };
        }
    }
}
