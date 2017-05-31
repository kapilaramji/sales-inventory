using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Model;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InventoryController : Controller
    {
        private MainDBContext db = new MainDBContext();

        // GET: Inventory
        public ActionResult Index()
        {
            return View(db.Inventory.Include(p => p.Product).ToList());
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventory/Add
        public ActionResult Add()
        {
            return View(db.Products.ToList());
        }

        // POST: Inventory/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection forms)
        {
            int product_id = Int32.Parse(forms["Product"]);
            int quantity = Int32.Parse(forms["Quantity"]);

            Inventory inventoryModels = db.Inventory.Where(i => i.ProductId == product_id).FirstOrDefault<Inventory>();
            inventoryModels.Quantity += quantity;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Inventory/Remove
        public ActionResult Remove()
        {
            return View(db.Products.ToList());
        }

        // POST: Inventory/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(FormCollection forms)
        {
            int product_id = Int32.Parse(forms["Product"]);
            int quantity = Int32.Parse(forms["Quantity"]);

            Inventory inventoryModels = db.Inventory.Where(i => i.ProductId == product_id).FirstOrDefault<Inventory>();
            inventoryModels.Quantity -= quantity;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
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
