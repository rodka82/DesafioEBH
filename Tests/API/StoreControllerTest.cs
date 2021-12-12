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
    public class StoreControllerTest
    {
        private readonly Mock<IService<Store>> _storeService;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<StoreController>> _logger;
        private readonly Store _store;
        private readonly StoreController _controller;
        public StoreControllerTest()
        {
            _storeService = new Mock<IService<Store>>();
            _mapper = MapperGenerator.GenerateMapper();
            _logger = new Mock<ILogger<StoreController>>();
            _store = new Store { Id = 1, Name = "Nome Loja", Address = "Endereço Loja" };
            _controller = new StoreController(_storeService.Object, _mapper, _logger.Object);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingGetByIdResult()
        {
            _storeService.Setup(s => s.GetById(It.IsAny<int>())).Returns(_store);

            var result = _controller.GetById(1);
            var objectResult = (OkObjectResult)result;
            var response = (ApplicationResponse)objectResult.Value;

            Assert.IsType<ApplicationResponse>(response);
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnInvalidSave()
        {
            _storeService.Setup(s => s.Save(It.IsAny<Store>()))
                .Returns(new ApplicationResponse()
                {
                    IsValid = false,
                    Messages = new List<string> { "Nome da Loja deve ser preenchido" }
                });

            var result = _controller.Save(_store);
            var objectResult = (BadRequestObjectResult)result;
            var response = (ApplicationResponse)objectResult.Value;

            Assert.IsType<ApplicationResponse>(response);
            Assert.True(response.Messages.Any());
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingErrorMessagesOnServerErrorr()
        {
            _storeService.Setup(s => s.Save(It.IsAny<Store>()))
                .Returns(() => throw new ApplicationException());

            var result = _controller.Save(_store);
            var objectResult = (ObjectResult)result;
            var response = (ApplicationResponse)objectResult.Value;

            Assert.IsType<ApplicationResponse>(response);
            Assert.True(response.Messages.Any());
        }

        [Fact]
        public void ShouldReturnApiResponseWrappingMessagesOnDelete()
        {
            _storeService.Setup(s => s.Delete(It.IsAny<Store>()))
                .Returns(new ApplicationResponse()
                {
                    IsValid = true,
                    Messages = new List<string> { "Registro removido com sucesso" }
                });

            var controller = new StoreController(_storeService.Object, _mapper, _logger.Object);

            var result = _controller.Delete(_store);
            var objectResult = (ObjectResult)result;
            var response = (ApplicationResponse)objectResult.Value;

            Assert.IsType<ApplicationResponse>(response);
            Assert.True(response.Messages.Any());
        }
    }
}
