using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Infrastructure.Database.Interfaces;
using Infrastructure.EntityFramework;

namespace Infrastructure.Database.EFImplementations
{
    public class OrderDetailsRepository : IRepository<OrderDetail>
    {
        private readonly SweetShopDataContext _context;

        public OrderDetailsRepository(SweetShopDataContext context)
        {
            _context = context;
        }

        public OrderDetail Get(int id)
        {
            return _context.OrderDetails.Find(id);
        }

        public IList<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail Create(OrderDetail entity)
        {
            return _context.OrderDetails.Add(entity);
        }

        public OrderDetail Edit(OrderDetail entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return entity;
        }

        public OrderDetail Remove(OrderDetail entity)
        {
            return _context.OrderDetails.Remove(entity);
        }
    }
}