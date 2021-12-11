﻿using Application.Utils;
using Application.Validators;
using Domain.Entities;
using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class BaseService<T> : IService<T> where T: BaseEntity
    {
        protected readonly IValidator<T> _validator;
        protected readonly IRepository<T> _repository;

        public BaseService(IValidator<T> validator, IRepository<T> repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public T GetById(int id)
        {
            var entity = _repository.GetById(id);
            return entity;
        }

        public IApplicationResponse Save(T entity)
        {
            _validator.Validate(entity);

            if (!_validator.IsValid)
                return ReturnValidationErrorResponse();

            AddOrUpdate(entity);

            return ReturnSuccessResponse();
        }

        protected IApplicationResponse ReturnSuccessResponse()
        {
            return new ApplicationResponse
            {
                IsValid = _validator.IsValid,
                Messages = new List<string> { "Registro salvo com sucesso" }
            };
        }

        protected IApplicationResponse ReturnValidationErrorResponse()
        {
            return new ApplicationResponse
            {
                IsValid = _validator.IsValid,
                Messages = _validator.ErrorMessages
            };
        }

        protected void AddOrUpdate(T entity)
        {
            if (entity.Id == 0)
                _repository.Add(entity);
            else
                _repository.Update(entity);
        }

        public IApplicationResponse Delete(T entity)
        {
            if (entity == null || entity.Id <= 0)
                ReturnValidationErrorResponse();

            _repository.Delete(entity);
            return ReturnSuccessResponse();
        }
    }
}
