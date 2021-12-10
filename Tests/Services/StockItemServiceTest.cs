using Application.Services;
using Application.Validators;
using Domain.Entities;
using Infra.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Services
{
    public class StockItemServiceTest : BaseStockItemServiceTest
    {
        protected readonly IStockItemValidator _validator;
        protected readonly IStockItemService _service;

        public StockItemServiceTest()
        {
            ResetRepository();
            _validator = new StockItemValidator();
            _service = new StockItemService(_validator, _repository);
        }

        protected StockItem GenerateValidStockItem()
        {
            var stockItem = new StockItem();
            stockItem.Product = new Product { Id = 1, Name = "Nome Produto", Price = 9.5 };
            stockItem.Store = new Store { Id = 1, Name = "Nome Loja" };
            stockItem.Quantity = 10;
            stockItem.OperationType = OperationType.Increment;
            return stockItem;
        }

        public class AssociationtValidation : StockItemServiceTest
        {
            [Fact]
            public void ShouldValidateNullProduct()
            {
                var StockItem = GenerateValidStockItem();
                StockItem.Product = null;
                var response = _service.Save(StockItem);
                Assert.Contains(response.Messages, m => m.Contains("Um produto deve ser associado a este Item"));
            }

            [Fact]
            public void ShouldValidateNullStore()
            {
                var StockItem = GenerateValidStockItem();
                StockItem.Store = null;
                var response = _service.Save(StockItem);
                Assert.Contains(response.Messages, m => m.Contains("Uma loja deve ser associada a este Item"));
            }
        }

        public class CreationValidation : StockItemServiceTest
        {
            [Fact]
            public void ShouldAddValidStockItem()
            {
                StockItem stockItem = GenerateValidStockItem();

                _service.Save(stockItem);

                var result = _repository.GetById(1);

                Assert.NotNull(result);
                Assert.IsType<StockItem>(result);
                Assert.Equal("Nome Produto", result.Product.Name);
                Assert.Equal("Nome Loja", result.Store.Name);
                Assert.Equal(10, result.Quantity);
                ResetRepository();
            }
        }

        public class QuantityManangementValidation : StockItemServiceTest
        {
            [Fact]
            public void ShouldIncrementValueToStockItemQuantity()
            {
                var stockItem = GenerateValidStockItem();

                _service.Save(stockItem);

                var stockItemToUpdate = GenerateValidStockItem();
                stockItemToUpdate.Id = 1;
                stockItemToUpdate.Quantity = 50;

                _service.Save(stockItemToUpdate);

                var result = _repository.GetById(1);

                Assert.NotNull(result);
                Assert.IsType<StockItem>(result);
                Assert.Equal("Nome Produto", result.Product.Name);
                Assert.Equal("Nome Loja", result.Store.Name);
                Assert.Equal(60, result.Quantity);

                ResetRepository();
            }


            [Fact]
            public void ShouldDecrementValueToStockItemQuantity()
            {
                var stockItem = GenerateValidStockItem();

                _service.Save(stockItem);

                var stockItemToUpdate = GenerateValidStockItem();
                stockItemToUpdate.Id = 1;
                stockItemToUpdate.Quantity = 50;
                stockItemToUpdate.OperationType = OperationType.Decrement;

                _service.Save(stockItemToUpdate);

                var result = _repository.GetById(1);

                Assert.NotNull(result);
                Assert.IsType<StockItem>(result);
                Assert.Equal("Nome Produto", result.Product.Name);
                Assert.Equal("Nome Loja", result.Store.Name);
                Assert.Equal(0, result.Quantity);

                ResetRepository();
            }

            [Fact]
            public void ShouldDeleteStockItem()
            {
                var stockItem = GenerateValidStockItem();

                _service.Save(stockItem);

                _service.Delete(stockItem);

                var result = _repository.GetById(1);

                Assert.Null(result);

                ResetRepository();
            }
        }
    }
}
