using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain.Model;
using Infrastructure.Database.EFImplementations;
using Infrastructure.Database.Interfaces;
using WebApp.Models.Order;

namespace WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View(_uow.Orders.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(_uow.Orders.Get(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateOrderViewModel createOrderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = _mapper.Map<Order>(createOrderViewModel);
                    _uow.Orders.Create(order);
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

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var order = _uow.Orders.Get((int)id);

            if (order == null)
                return HttpNotFound("Order not found!");

            var editOrderViewModel = _mapper.Map<EditOrderViewModel>(order);

            return View(editOrderViewModel);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(EditOrderViewModel editOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(editOrderViewModel);
                _uow.Orders.Edit(order);
                _uow.Save();

                return RedirectToAction("Index");

            }

            return View();
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = _uow.Orders.Get(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = _uow.Orders.Get(id);
            _uow.Orders.Remove(order);
            _uow.Save();
            return RedirectToAction("Index");
        }
    }
}