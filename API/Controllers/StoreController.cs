using Application.Services;
using Application.Utils;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ApplicationBaseController<StoreDTO>
    {
        private readonly IService<Store> _storeService;

        public StoreController(IService<Store> storeService, IMapper mapper, ILogger<StoreController> logger)
            :base(mapper, logger)
        {
            _storeService = storeService;
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult GetById(int id)
        {
            IApplicationResponse result = new ApplicationResponse();

            try
            {
                var store = _storeService.GetById(id);
                var dto = _mapper.Map<StoreDTO>(store);
                var response = ReturnSuccessResponse(dto);
                return SetStatusCodeForSearch(response);
            }
            catch (Exception e)
            {
                AddUserFriendlyErrorMessage(result);
                _logger.LogError($"Ocorreu um erro interno: { e.Message } {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save(Store store)
        {
            IApplicationResponse result = new ApplicationResponse();

            if (store.Id != 0)
            {
                result.IsValid = false;
                result.Messages = new List<string>() { "Não se pode adicionar uma loja especificando um Id." };

                return BadRequest(result);
            }

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
        [Authorize]
        public IActionResult Update(Store store)
        {
            IApplicationResponse result = new ApplicationResponse();
            if (store.Id == 0)
            {
                result.IsValid = false;
                result.Messages = new List<string>() { "Informe um Id para alteração" };

                return BadRequest(result);
            }

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

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(Store store)
        {
            IApplicationResponse result = new ApplicationResponse();

            try
            {
                result = _storeService.Delete(store);
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
