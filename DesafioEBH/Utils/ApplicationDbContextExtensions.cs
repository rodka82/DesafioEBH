using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Utils
{
    public static class ApplicationDbContextExtensions
    {
        public static void Reset(this ApplicationDbContext context)
        {
            if (context != null)
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
