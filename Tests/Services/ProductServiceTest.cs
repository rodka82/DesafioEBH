using Application.Services;
using Application.Validators;
using Domain.Entities;
using System;
using Tests.Services;
using Xunit;

namespace Tests.Services
{
    public class ProductServiceTest : BaseProductServiceTest
    {
        protected readonly IValidator<Product> _validator;
        protected readonly IProductService _service;

        public ProductServiceTest()
        {
            ResetRepository();
            _validator = new ProductValidator();
            _service = new ProductService(_validator, _repository);
        }

        private Product GenerateValidProduct()
        {
            var Product = new Product();
            Product.Name = "Product Name";
            Product.Price = 10.50;
            return Product;
        }

        public class NameValidation : ProductServiceTest
        {
            [Fact]
            public void ShouldValidateNullProductName()
            {
                var Product = new Product();
                Product.Name = null;
                var response = _service.Save(Product);
                Assert.Contains(response.Messages, m => m.Contains("Informe o Nome do produto."));
            }

            [Fact]
            public void ShouldValidateEmptyProductName()
            {
                var Product = new Product();
                Product.Name = string.Empty;
                Product.Price = 10.5;
                var response = _service.Save(Product);
                Assert.Contains(response.Messages, m => m.Contains("Informe o Nome do produto."));
            }

            [Fact]
            public void ShouldValidateBlankStringProductName()
            {
                var Product = new Product();
                Product.Name = " ";
                Product.Price = 10.5;
                var response = _service.Save(Product);
                Assert.Contains(response.Messages, m => m.Contains("Informe o Nome do produto."));
            }

        }

        public class PriceValidation : ProductServiceTest
        {
            [Fact]
            public void ShouldValidateDefaultDoubleProductPrice()
            {
                var Product = new Product();
                Product.Name = "Product Name";
                Product.Price = default(double);
                var response = _service.Save(Product);
                Assert.Contains(response.Messages, m => m.Contains("O preço do produto deve ser informado"));
            }
        }

        public class CrudValidation : ProductServiceTest
        {
            [Fact]
            public void ShouldAddValidProduct()
            {
                Product Product = GenerateValidProduct();

                _service.Save(Product);

                var result = _repository.GetById(1);

                Assert.NotNull(result);
                Assert.IsType<Product>(result);
                Assert.Equal("Product Name", result.Name);

                ResetRepository();
            }


            [Fact]
            public void ShouldUpdateValidProduct()
            {
                var Product = GenerateValidProduct();

                _service.Save(Product);

                var ProductToUpdate = GenerateValidProduct();
                ProductToUpdate.Id = 1;
                ProductToUpdate.Name = "Updated Name";
                ProductToUpdate.Price = 20.8;

                _service.Save(ProductToUpdate);

                var result = _repository.GetById(1);

                Assert.NotNull(result);
                Assert.IsType<Product>(result);
                Assert.Equal("Updated Name", result.Name);
                Assert.Equal(20.8, result.Price);

                ResetRepository();
            }

            [Fact]
            public void ShouldDeleteProduct()
            {
                var Product = GenerateValidProduct();

                _service.Save(Product);

                _service.Delete(Product);

                var result = _repository.GetById(1);

                Assert.Null(result);

                ResetRepository();
            }
        }
    }
}
