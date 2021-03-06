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
        private readonly StockItemDTO _stockItemDTO;
        private readonly StockItemController _controller;
        public StockItemControllerTest()
        {
            _stockItemService = new Mock<IStockItemService>();
            _mapper = MapperGenerator.GenerateMapper();
            _logger = new Mock<ILogger<StockItemController>>();
            _stockItem = new StockItem { Id = 1, StoreId = 1, ProductId = 1, Quantity = 10 };
            _stockItemDTO = new StockItemDTO { Id = 1, StoreId = 1, ProductId = 1, Quantity = 10 };
            _controller = new StockItemController(_stockItemService.Object, _mapper, _logger.Object);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnInvalidSave()
        {
            _stockItemService.Setup(s => s.Save(It.IsAny<StockItem>()))
                .Returns(new ApplicationResponse()
                {
                    IsValid = false,
                    Messages = new List<string> { "" }
                });

            var result = _controller.Save(_stockItemDTO);
            var objectResult = (BadRequestObjectResult)result;
            var response = (ApplicationResponse)objectResult.Value;

            Assert.IsType<ApplicationResponse>(response);
            Assert.True(response.Messages.Any());
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnServerErrorr()
        {
            _stockItemService.Setup(s => s.Save(It.IsAny<StockItem>()))
                .Returns(() => throw new ApplicationException());

            var result = _controller.Save(_stockItemDTO);
            var objectResult = (ObjectResult)result;
            var response = (ApplicationResponse)objectResult.Value;

            Assert.IsType<ApplicationResponse>(response);
            Assert.True(response.Messages.Any());
        }
    }
}
