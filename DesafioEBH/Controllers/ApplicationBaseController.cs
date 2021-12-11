using API.Utils;
using Application.Utils;
using AutoMapper;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ApplicationBaseController : Controller
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger<ApplicationBaseController> _logger;
        
        public ApplicationBaseController(IMapper mapper, ILogger<ApplicationBaseController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        protected IActionResult SetStatusCode(IApplicationResponse result)
        {
            if (result.Result != null)
                return Ok(ReturnApiResponse(result));
            else
                return BadRequest(ReturnApiResponse(result));
        }

        protected static void AddUserFriendlyErrorMessage(IApplicationResponse result)
        {
            result.Messages.Add("Houve um erro ao processar sua solicitação");
        }

        protected ApiResponse ReturnApiResponse(IApplicationResponse result)
        {
            var response = _mapper.Map<ApiResponse>(result);
            if (result.Result != null)
            {
                response.Result = _mapper.Map<BaseEntityDTO>(result.Result);
                response.StatusCode = GetStatusCode(response);
            }
            else
            {
                response.StatusCode = 500;
            }
            return response;
        }

        protected static int GetStatusCode(ApiResponse response)
        {
            return response.Messages.Any() ? 400 : 200;
        }

        protected ApiResponse ReturnSuccessResponse(BaseEntityDTO dto)
        {
            return new ApiResponse
            {
                Result = dto,
                StatusCode = 200,
            };
        }

        protected void LogError(Exception e)
        {
            _logger.LogError($"Ocorreu um erro interno: { e.Message } {e.StackTrace}");
        }
    }
}
