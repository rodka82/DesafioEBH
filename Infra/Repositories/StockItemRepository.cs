using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class StockItemRepository : Repository<StockItem>
    {
        public StockItemRepository(DbContext context)
            :base(context)
        {

        }

        public override StockItem GetById(int id)
        {
            return _entities
                .Include(p => p.Product)
                .Include(p => p.Store)
                .FirstOrDefault(e => e.Id == id);
        }

        public override bool Add(StockItem entity)
        {
            if (entity == null)
                return false;
            
            _entities.Add(entity);

            _context.SaveChanges();

            return true;
        }
    }
}
