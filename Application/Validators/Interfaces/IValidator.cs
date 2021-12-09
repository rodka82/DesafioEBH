using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public interface IValidator<T>
    {
        List<string> ErrorMessages { get; set; }
        public bool IsValid { get; set; }
        void Validate(T entity);
    }
}
