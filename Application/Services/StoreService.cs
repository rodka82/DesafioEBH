using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class StoreService : IStoreService
    {
        public void Save(Store store)
        {
            if (HasNullValue(store))
            {
                throw new ArgumentNullException("Nome da loja não pode ser nulo");
            }

            if (HasEmptyValue(store))
            {
                throw new ArgumentException("Nome da loja não pode ser vazio");
            }
        }

        private static bool HasEmptyValue(Store store)
        {
            return IsEmpty(store.Name) || IsEmpty(store.Address);
        }

        private static bool IsEmpty(string value)
        {
            return value.Trim() == string.Empty;
        }

        private static bool HasNullValue(Store store)
        {
            return IsNull(store.Name) || IsNull(store.Address);
        }

        private static bool IsNull(string value)
        {
            return value == null;
        }
    }
}
