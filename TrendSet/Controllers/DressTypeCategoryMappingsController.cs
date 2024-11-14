using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrendSet.Models;

namespace TrendSet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DressTypeCategoryMappingsController : Controller
    {
        private TrendSetContext db = new TrendSetContext();

        // GET: DressTypeCategoryMappings
        public ActionResult Index()
        {
            var dressTypeCategoryMappings = db.DressTypeCategoryMappings.Include(d => d.Category).Include(d => d.DressType);
            return View(dressTypeCategoryMappings.ToList());
        }

        

        // GET: DressTypeCategoryMappings/Create
       

        // GET: DressTypeCategoryMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DressTypeCategoryMapping dressTypeCategoryMapping = db.DressTypeCategoryMappings.Find(id);
            if (dressTypeCategoryMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dressTypeCategoryMapping.CategoryId);
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName", dressTypeCategoryMapping.DressTypeId);
            return View(dressTypeCategoryMapping);
        }

        // POST: DressTypeCategoryMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,DressTypeId")] DressTypeCategoryMapping dressTypeCategoryMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dressTypeCategoryMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", dressTypeCategoryMapping.CategoryId);
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName", dressTypeCategoryMapping.DressTypeId);
            return View(dressTypeCategoryMapping);
        }

        // GET: DressTypeCategoryMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DressTypeCategoryMapping dressTypeCategoryMapping = db.DressTypeCategoryMappings.Find(id);
            if (dressTypeCategoryMapping == null)
            {
                return HttpNotFound();
            }
            return View(dressTypeCategoryMapping);
        }

        // POST: DressTypeCategoryMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DressTypeCategoryMapping dressTypeCategoryMapping = db.DressTypeCategoryMappings.Find(id);
            db.DressTypeCategoryMappings.Remove(dressTypeCategoryMapping);
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
