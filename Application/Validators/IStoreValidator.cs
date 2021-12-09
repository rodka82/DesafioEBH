using Application.Utils;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Validators
{
    public interface IStoreValidator
    {
        List<string> ErrorMessages { get; set; }
        public bool IsValid { get; set; }
        void Validate(Store entity);
    }
}