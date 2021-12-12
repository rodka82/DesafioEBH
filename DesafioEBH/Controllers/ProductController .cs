using API.Enum;
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
    public class ProductController : ApplicationBaseController<ProductDTO>
    {
        private readonly IService<Product> _storeService;

        public ProductController(IService<Product> storeService, IMapper mapper, ILogger<ProductController> logger)
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
                var product = _storeService.GetById(id);
                var dto = _mapper.Map<ProductDTO>(product);
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
        public IActionResult Save(Product product)
        {
            IApplicationResponse result = new ApplicationResponse();

            if (product.Id != 0)
            {
                result.IsValid = false;
                result.Messages = new List<string>() { "Não se pode adicionar um produto especificando um Id." };

                return BadRequest(result);
            }

            try
            {
                result = _storeService.Save(product);
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
        public IActionResult Update(Product product)
        {
            IApplicationResponse result = new ApplicationResponse();

            if (product.Id == 0)
            {
                result.IsValid = false;
                result.Messages = new List<string>() { "Informe um Id para alteração" };

                return BadRequest(result);
            }

            try
            {
                result = _storeService.Save(product);
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
        public IActionResult Delete(Product product)
        {
            IApplicationResponse result = new ApplicationResponse();

            try
            {
                result = _storeService.Delete(product);
                return SetStatusCode(result, OperationType.Delete);
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
