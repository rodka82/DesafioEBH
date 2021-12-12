using Application.Utils;
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
        private const string NOT_FOUND_MESSAGE = "Nenhum registro foi encontrado com o Id informado.";
        private const string NULL_ENTITY_MESSAGE = "Nenhum registro foi informado para remoção.";

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
                return ReturnValidationErrorResponse(entity);

            return AddOrUpdate(entity);
        }

        protected IApplicationResponse AddOrUpdate(T entity)
        {
            if (entity.Id == 0)
                _repository.Add(entity);
            else
            {
                var existentEntity = _repository.GetById(entity.Id);

                if (existentEntity == null)
                {
                    _validator.ErrorMessages.Add(NOT_FOUND_MESSAGE);
                    return ReturnValidationErrorResponse();
                }
                
                existentEntity = entity;
                _repository.Update(existentEntity);
            }

            return ReturnSuccessResponse(null, entity);
        }

        public IApplicationResponse Delete(T entity)
        {
            if (entity == null)
            {
                _validator.ErrorMessages.Add(NULL_ENTITY_MESSAGE);
                return ReturnValidationErrorResponse();
            }

            var existentEntity = _repository.GetById(entity.Id);

            if(existentEntity == null)
            {
                _validator.ErrorMessages.Add(NOT_FOUND_MESSAGE);
                return ReturnValidationErrorResponse();
            }

            _validator.IsValid = true;
            _repository.Delete(existentEntity);

            return ReturnSuccessResponse();
        }

        protected IApplicationResponse ReturnSuccessResponse(string message = null, BaseEntity entity = null)
        {
            return new ApplicationResponse
            {
                Result = entity,
                IsValid = _validator.IsValid,
                Messages = message != null ? new List<string> { message } : null
            };
        }

        protected IApplicationResponse ReturnValidationErrorResponse(BaseEntity entity = null)
        {
            return new ApplicationResponse
            {
                Result = entity,
                IsValid = _validator.IsValid,
                Messages = _validator.ErrorMessages
            };
        }
    }
}
