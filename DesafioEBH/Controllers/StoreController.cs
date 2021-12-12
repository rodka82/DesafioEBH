﻿using Application.Services;
using Application.Utils;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
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
                LogError(e);
                return StatusCode(StatusCodes.Status500InternalServerError, ReturnApiResponse(result));
            }
        }

        [HttpPut]
        public IActionResult Update(Store store)
        {
            if (store.Id == 0)
            {
                IApplicationResponse result = new ApplicationResponse();
                result.IsValid = false;
                result.Messages = new List<string>() { "Informe um Id para alteração" };

                return BadRequest(result);
            }

            return Save(store);
        }

        [HttpDelete]
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