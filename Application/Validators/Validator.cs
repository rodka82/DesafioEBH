using Domain.Entities;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validators
{
    public abstract class Validator<T> : IValidator<T> where T :BaseEntity
    {
        public List<string> ErrorMessages { get; set; }

        public bool IsValid { get; set; }

        public Validator()
        {
            ErrorMessages = new List<string>();
        }

        public abstract void Validate(T entity);

        protected void SetValidity()
        {
            IsValid = !ErrorMessages.Any();
        }
    }
}
