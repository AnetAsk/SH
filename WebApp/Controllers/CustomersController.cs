using AutoMapper;
using Domain.Model;
using Infrastructure.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.Customer;

namespace WebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View(_uow.Customers.GetAll());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            return View(_uow.Customers.Get(id));
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(CreateCustomerViewModel createCustomerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = _mapper.Map<Customer>(createCustomerViewModel);
                    _uow.Customers.Create(customer);
                    _uow.Save();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var product = _uow.Customers.Get((int)id);

            if (product == null)
                return HttpNotFound("Customer not found!");

            var editCustomerViewModel = _mapper.Map<EditCustomerViewModel>(product);

            return View(editCustomerViewModel);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(EditCustomerViewModel editCustomerViewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(editCustomerViewModel);
                _uow.Customers.Edit(customer);
                _uow.Save();

                return RedirectToAction("Index");

            }

            return View();
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _uow.Customers.Get(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = _uow.Customers.Get(id);
            _uow.Customers.Remove(customer);
            _uow.Save();
            return RedirectToAction("Index");
        }
    }
}
