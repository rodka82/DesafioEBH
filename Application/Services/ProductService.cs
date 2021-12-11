using Application.Utils;
using Application.Validators;
using Domain.Entities;
using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductService : BaseService<Product>
    {
        public ProductService(IValidator<Product> validator, IRepository<Product> repository)
            :base(validator, repository)
        {
        }
    }
}
