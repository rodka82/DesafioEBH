using API.Controllers;
using API.Mapper;
using API.Utils;
using Application.Services;
using Application.Utils;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockItemControllerTest
    {
        private readonly Mock<IStockItemService> _stockItemService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<StockItemController>> _logger;
        private readonly StockItem _stockItem;
        private readonly StockItemController _controller;
        public StockItemControllerTest()
        {
            _stockItemService = new Mock<IStockItemService>();
            _mapper = MapperGenerator.GenerateMapper();
            _logger = new Mock<ILogger<StockItemController>>();
            _stockItem = new StockItem { Id = 1, Store = new Store(), Product = new Product(), Quantity = 10 };
            _controller = new StockItemController(_stockItemService.Object, _mapper, _logger.Object);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingGetByIdResult()
        {
            _stockItemService.Setup(s => s.GetById(It.IsAny<int>())).Returns(_stockItem);

            var result = _controller.GetById(1);
            var objectResult = (OkObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnInvalidSave()
        {
            _stockItemService.Setup(s => s.Save(It.IsAny<StockItem>()))
                .Returns(new ApplicationResponse()
                {
                    IsValid = false,
                    Messages = new List<string> { "Nome da Loja deve ser preenchido" }
                });

            var result = _controller.Save(_stockItem);
            var objectResult = (BadRequestObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
            Assert.True(response.Messages.Any());
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnServerErrorr()
        {
            _stockItemService.Setup(s => s.Save(It.IsAny<StockItem>()))
                .Returns(() => throw new ApplicationException());

            var result = _controller.Save(_stockItem);
            var objectResult = (ObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
            Assert.True(response.Messages.Any());
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingMessagesOnDelete()
        {
            _stockItemService.Setup(s => s.Delete(It.IsAny<StockItem>()))
                .Returns(new ApplicationResponse()
                {
                    IsValid = true,
                    Messages = new List<string> { "Registro removido com sucesso" }
                });

            var result = _controller.Delete(_stockItem);
            var objectResult = (ObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
            Assert.True(response.Messages.Any());
        }
    }
}
