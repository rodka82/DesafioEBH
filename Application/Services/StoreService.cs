using Application.Utils;
using Application.Validators;
using Domain.Entities;
using Domain.Extensions;
using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class StoreService : BaseService<Store>
    {
        public StoreService(IValidator<Store> validator, IRepository<Store> repository) 
            : base(validator, repository)
        {
        }
    }
}
