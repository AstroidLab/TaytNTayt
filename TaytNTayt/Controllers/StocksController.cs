using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaytNTayt.Models;

namespace TaytNTayt.Controllers
{
    public class StocksController : Controller
    {
        private taytntaytEntities db = new taytntaytEntities();

        // GET: Stocks
        public ActionResult Index()
        {
            var stocks = db.Stocks.Include(s => s.Product).Include(s => s.StockPoint).Include(s => s.Supplier);
            return View(stocks.ToList());
        }

        // GET: Stocks/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ImageURL");
            ViewBag.StockPointID = new SelectList(db.StockPoints, "ID", "Name");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "SupplierName");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SupplierID,CreatedBy,IsDeleted,ProductID,StockPointID,Amount")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.ID = Guid.NewGuid();
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "ImageURL", stock.ProductID);
            ViewBag.StockPointID = new SelectList(db.StockPoints, "ID", "Name", stock.StockPointID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "SupplierName", stock.SupplierID);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ImageURL", stock.ProductID);
            ViewBag.StockPointID = new SelectList(db.StockPoints, "ID", "Name", stock.StockPointID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "SupplierName", stock.SupplierID);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SupplierID,CreatedBy,IsDeleted,ProductID,StockPointID,Amount")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "ImageURL", stock.ProductID);
            ViewBag.StockPointID = new SelectList(db.StockPoints, "ID", "Name", stock.StockPointID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "SupplierName", stock.SupplierID);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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
