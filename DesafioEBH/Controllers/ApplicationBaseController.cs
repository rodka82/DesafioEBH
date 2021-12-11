using API.Utils;
using Application.Utils;
using AutoMapper;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ApplicationBaseController : Controller
    {
        protected readonly IMapper _mapper;
        public ApplicationBaseController(IMapper mapper)
        {
            _mapper = mapper;
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
    }
}
