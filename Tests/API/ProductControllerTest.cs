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
    public class ProductControllerTest
    {
        private readonly Mock<IService<Product>> _productService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<ProductController>> _logger;
        private readonly Product _product;
        private readonly ProductController _controller;
        public ProductControllerTest()
        {
            _productService = new Mock<IService<Product>>();
            _mapper = MapperGenerator.GenerateMapper();
            _logger = new Mock<ILogger<ProductController>>();
            _product = new Product { Id = 1, Name = "Nome Produto", Price = 10.5};
            _controller = new ProductController(_productService.Object, _mapper, _logger.Object);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingGetByIdResult()
        {
            _productService.Setup(s => s.GetById(It.IsAny<int>())).Returns(_product);

            var result = _controller.GetById(1);
            var objectResult = (OkObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnInvalidSave()
        {
            _productService.Setup(s => s.Save(It.IsAny<Product>()))
                .Returns(new ApplicationResponse()
                {
                    IsValid = false,
                    Messages = new List<string> { "Nome da Loja deve ser preenchido" }
                });

            var result = _controller.Save(_product);
            var objectResult = (BadRequestObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
            Assert.True(response.Messages.Any());
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnServerErrorr()
        {
            _productService.Setup(s => s.Save(It.IsAny<Product>()))
                .Returns(() => throw new ApplicationException());

            var result = _controller.Save(_product);
            var objectResult = (ObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
            Assert.True(response.Messages.Any());
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingMessagesOnDelete()
        {
            _productService.Setup(s => s.Delete(It.IsAny<Product>()))
                .Returns(new ApplicationResponse()
                {
                    IsValid = true,
                    Messages = new List<string> { "Registro removido com sucesso" }
                });

            var controller = new ProductController(_productService.Object, _mapper, _logger.Object);

            var result = _controller.Delete(_product);
            var objectResult = (ObjectResult)result;
            var response = (ApiResponse)objectResult.Value;

            Assert.IsType<ApiResponse>(response);
            Assert.True(response.Messages.Any());
        }
    }
}
