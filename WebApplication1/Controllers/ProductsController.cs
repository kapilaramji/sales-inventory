using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using App.Model;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private MainDBContext db = new MainDBContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Product/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Product/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Name,Price")] Product productModels)
        {
            if (ModelState.IsValid)
            {
                // Insert Product in Inventory table
                var newInventory = new Inventory();
                newInventory.Id = productModels.Id;
                newInventory.ProductId = productModels.Id;
                newInventory.Quantity = 0;
                db.Inventory.Add(newInventory);
                // Insert Product in Product table
                db.Products.Add(productModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productModels);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product productModels = db.Products.Find(id);
            if (productModels == null)
            {
                return HttpNotFound();
            }
            return View(productModels);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] Product productModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productModels);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product productModels = db.Products.Find(id);
            if (productModels == null)
            {
                return HttpNotFound();
            }
            return View(productModels);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // delete the product from products table
            Product productModels = db.Products.Find(id);
            db.Products.Remove(productModels);
            // delete the product from inventory table
            Inventory inventoryModels = db.Inventory.Where(i => i.ProductId == id).FirstOrDefault<Inventory>();
            db.Inventory.Remove(inventoryModels);
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
