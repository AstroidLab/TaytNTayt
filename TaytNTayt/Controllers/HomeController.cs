using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaytNTayt.Models;

namespace TaytNTayt.Controllers
{
    public class HomeController : Controller
    {

        taytntaytEntities db = new taytntaytEntities();

        List<Product> ProductList(int _category,int _top)
        {
            int minStockLimitToShow = 0;
            //int top = 12;
            var query = db.Stocks.AsQueryable();
            if (_category>0)
            {
                query = query.Where(x => x.Product.CategoriID == _category);
            }
            if (_top<4)
            {
                _top = 4;
            }
            query = query.Where(x => x.Amount > minStockLimitToShow);
            query = query.Take(_top);
         
            var stocks = query.ToList();
            List<Product> productsInStocs = new List<Product>();

            foreach (var item in stocks)
            {
                productsInStocs.Add(item.Product);
            }
            return productsInStocs;
        }

        public PartialViewResult GetProducts(int category,int top)
        {
            return PartialView(ProductList(category,top));
        }


        public ActionResult Index()
        {
           
            return View(ProductList(0,14));
        }
        public PartialViewResult _GetProducts()
        {
            var query = db.Products.AsQueryable();



            return PartialView(query.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}