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
    public class ProductsController : Controller
    {
        private taytntaytEntities db = new taytntaytEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.User);
            return View(products.ToList());
        }
        public void addMultipleDummy()
        {
            for (int i = 0; i < 50; i++)
            {
                Product dummy = new Product();
                dummy.ID = Guid.NewGuid();
                dummy.User = db.Users.FirstOrDefault();
                dummy.CategoriID = 1;
                dummy.ImageURL = "/images/product-08.jpg";
                dummy.ProductName = "Dummy Product " + i;
                dummy.ProductDescription = "Description " + i;
                dummy.Price = Convert.ToDecimal(10 + i);

                Stock dummyStock = new Stock();
                dummyStock.ID = Guid.NewGuid();
                dummyStock.Product = dummy;
                dummyStock.StockPoint = db.StockPoints.FirstOrDefault();
                dummyStock.Supplier = db.Suppliers.FirstOrDefault();
                dummyStock.Amount = 9999;

                db.Products.Add(dummy);
                db.Stocks.Add(dummyStock);
                
            }
            db.SaveChanges();
        }
        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoriID = new SelectList(db.Categories, "ID", "CategoryName");
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Email");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ImageURL,ProductName,ProductDescription,CategoriID,CreatedBy,IsDeleted,IsApproved")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ID = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoriID);
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Email", product.CreatedBy);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoriID);
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Email", product.CreatedBy);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImageURL,ProductName,ProductDescription,CategoriID,CreatedBy,IsDeleted,IsApproved")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriID = new SelectList(db.Categories, "ID", "CategoryName", product.CategoriID);
            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Email", product.CreatedBy);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
