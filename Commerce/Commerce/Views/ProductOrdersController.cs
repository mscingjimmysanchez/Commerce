using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Commerce.Models;

namespace Commerce.Views
{
    public class ProductOrdersController : Controller
    {
        private CommerceEntities db = new CommerceEntities();

        // GET: ProductOrders
        public ActionResult Index()
        {
            var productOrder = db.ProductOrder.Include(p => p.Order).Include(p => p.Product);
            return View(productOrder.ToList());
        }

        // GET: ProductOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrder.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            return View(productOrder);
        }

        // GET: ProductOrders/Create
        public ActionResult Create()
        {
            ViewBag.IdOrder = new SelectList(db.Order, "Id", "Id");
            ViewBag.IdProduct = new SelectList(db.Product, "Number", "Title");
            return View();
        }

        // POST: ProductOrders/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrder,IdProduct,Quantity")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            {
                db.ProductOrder.Add(productOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = new SelectList(db.Order, "Id", "Id", productOrder.IdOrder);
            ViewBag.IdProduct = new SelectList(db.Product, "Number", "Title", productOrder.IdProduct);
            return View(productOrder);
        }

        // GET: ProductOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrder.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOrder = new SelectList(db.Order, "Id", "Id", productOrder.IdOrder);
            ViewBag.IdProduct = new SelectList(db.Product, "Number", "Title", productOrder.IdProduct);
            return View(productOrder);
        }

        // POST: ProductOrders/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrder,IdProduct,Quantity")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdOrder = new SelectList(db.Order, "Id", "Id", productOrder.IdOrder);
            ViewBag.IdProduct = new SelectList(db.Product, "Number", "Title", productOrder.IdProduct);
            return View(productOrder);
        }

        // GET: ProductOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrder.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            return View(productOrder);
        }

        // POST: ProductOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductOrder productOrder = db.ProductOrder.Find(id);
            db.ProductOrder.Remove(productOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
