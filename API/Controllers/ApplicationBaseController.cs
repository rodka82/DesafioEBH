using API.Enum;
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
    public class ApplicationBaseController<T> : Controller where T : BaseEntityDTO
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger<ApplicationBaseController<T>> _logger;
        
        public ApplicationBaseController(IMapper mapper, ILogger<ApplicationBaseController<T>> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        protected IActionResult SetStatusCode(IApplicationResponse result, OperationType? operationType = null)
        {
            if (result.IsValid)
                return Ok(ReturnApiResponse(result, operationType));

            if(result.Messages.Any() && operationType != OperationType.Delete)
                return BadRequest(ReturnApiResponse(result));

            return NotFound(ReturnApiResponse(result));
        }

        protected IActionResult SetStatusCodeForSearch(ApplicationResponse response)
        {
            if (response.Result == null)
            {
                response.IsValid = false;
                return NotFound(response);
            }

            return Ok(response);
        }

        protected static void AddUserFriendlyErrorMessage(IApplicationResponse result)
        {
            result.Messages = new List<string>() { "Houve um erro ao processar sua solicitação"  };
        }

        protected ApplicationResponse ReturnApiResponse(IApplicationResponse result, OperationType? operationType = null)
        {
            var response = _mapper.Map<ApplicationResponse>(result);

            if (IsSuccessfullSaveOperation(result))
            {
                response.Result = _mapper.Map<T>(result.Result);
                return response;
            }

            return response;
        }

        private static bool IsSuccessfullSaveOperation(IApplicationResponse result)
        {
            return result.Result != null;
        }

        protected ApplicationResponse ReturnSuccessResponse(BaseEntityDTO dto)
        {
            return new ApplicationResponse
            {
                IsValid = true,
                Result = dto,
            };
        }

        protected void LogError(Exception e)
        {
            _logger.LogError($"Ocorreu um erro interno: { e.Message } {e.StackTrace}");
        }
    }
}
