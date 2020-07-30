using AutoMapper;
using Domain.Model;
using Infrastructure.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.OrderDetails;

namespace WebApp.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderDetailsController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // GET: OrderDetailss
        public ActionResult Index()
        {
            return View(_uow.OrderDetails.GetAll());
        }

        // GET: OrderDetailss/Details/5
        public ActionResult Details(int id)
        {
            return View(_uow.OrderDetails.Get(id));
        }

        // GET: OrderDetailss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDetailss/Create
        [HttpPost]
        public ActionResult Create(CreateOrderDetailsViewModel createOrderDetailsViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var orderDetails = _mapper.Map<OrderDetail>(createOrderDetailsViewModel);
                    _uow.OrderDetails.Create(orderDetails);
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

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var orderDetails = _uow.OrderDetails.Get((int)id);

            if (orderDetails == null)
                return HttpNotFound("OrderDetails not found!");

            var editOrderDetailsViewModel = _mapper.Map<EditOrderDetailsViewModel>(orderDetails);

            return View(editOrderDetailsViewModel);
        }

        // POST: OrderDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(EditOrderDetailsViewModel editOrderDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                var orderDetails = _mapper.Map<OrderDetail>(editOrderDetailsViewModel);
                _uow.OrderDetails.Edit(orderDetails);
                _uow.Save();

                return RedirectToAction("Index");

            }

            return View();
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetails = _uow.OrderDetails.Get(id.Value);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View(orderDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var orderDetails = _uow.OrderDetails.Get(id);
            _uow.OrderDetails.Remove(orderDetails);
            _uow.Save();
            return RedirectToAction("Index");
        }
    }
}