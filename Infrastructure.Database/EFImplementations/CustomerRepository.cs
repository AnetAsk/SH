using Domain.Model;
using Infrastructure.Database.Interfaces;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.EFImplementations
{
    class CustomerRepository : IRepository<Customer>
    {
        private readonly SweetShopDataContext _context;

        public CustomerRepository(SweetShopDataContext context)
        {
            _context = context;
        }

        public Customer Create(Customer entity)
        {
           return _context.Customers.Add(entity);
        }

        public Customer Edit(Customer entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            return entity;
        }

        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public IList<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer Remove(Customer entity)
        {
            return _context.Customers.Remove(entity);
        }
    }
}
