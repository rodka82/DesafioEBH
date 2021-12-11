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
    public class StoreController : ApplicationBaseController
    {
        private readonly IService<Store> _storeService;
        private readonly ILogger<StoreController> _logger;

        public StoreController(IService<Store> storeService, IMapper mapper, ILogger<StoreController> logger)
            :base(mapper)
        {
            _storeService = storeService;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult GetById(int id)
        {
            IApplicationResponse result = new ApplicationResponse();

            try
            {
                var store = _storeService.GetById(id);
                var dto = _mapper.Map<StoreDTO>(store);
                var response = ReturnSuccessResponse(dto);
                return Ok(response);
            }
            catch (Exception e)
            {
                AddUserFriendlyErrorMessage(result);
                _logger.LogError($"Ocorreu um erro interno: { e.Message } {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }

        [HttpPost]
        [HttpPut]
        public IActionResult Save(Store store)
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
        public IActionResult Delete(Store store)
        {
            IApplicationResponse result = new ApplicationResponse();

            try
            {
                result = _storeService.Delete(store);
                return SetStatusCode(result);
            }
            catch (Exception)
            {
                AddUserFriendlyErrorMessage(result);
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }
    }
}
