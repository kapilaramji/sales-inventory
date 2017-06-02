using App.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private MainDBContext db = new MainDBContext();
        public class SalesTotal
        {
            public DateTime Date { get; set; }
            public Decimal Total { get; set; }
        }
        public ActionResult Index()
        {
            // get last 10 day sales:
            
            var cutoff = DateTime.Now.AddDays(-10);
            ViewBag.sales = db.Sales
                  .Where(p => p.SaleDate >= cutoff)
                  .GroupBy(p => DbFunctions.TruncateTime(p.SaleDate))
                  .Select(g => new SalesTotal {
                      Date = g.Key ?? DateTime.Now,
                      Total = g.Sum(p => p.Amount)
                  })
                  .OrderByDescending(g => g.Date);
            return View();
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