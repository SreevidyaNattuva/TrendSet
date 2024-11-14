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
    public class DressTypesController : Controller
    {
        private TrendSetContext db = new TrendSetContext();

        // GET: DressTypes
        public ActionResult Index()
        {
            return View(db.DressTypes.ToList());
        }
        public ActionResult AddDressType()
        {
            ViewBag.Category = db.Categories;
            return View();
        }
        [HttpPost]
        public ActionResult AddDressType(string dressTypeName, int[] categoryIds)
        {
            if (ModelState.IsValid)
            {
                bool dresstyopeExists = db.DressTypes.Any(t => t.DressTypeName == dressTypeName);
                if (!dresstyopeExists)
                {
                    DressType dressType = new DressType();
                    dressType.DressTypeName = dressTypeName;
                    db.DressTypes.Add(dressType);
                    db.SaveChanges();

                    int id = (from c in db.DressTypes where c.DressTypeName == dressTypeName select c.DressTypeId).FirstOrDefault();
                    foreach (var category in categoryIds)
                    {
                        DressTypeCategoryMapping dressTypeCategoryMapping = new DressTypeCategoryMapping();
                        dressTypeCategoryMapping.DressTypeId = id;
                        dressTypeCategoryMapping.CategoryId = category;
                        db.DressTypeCategoryMappings.Add(dressTypeCategoryMapping);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "DressTypeCategoryMappings");
                }
                else
                {
                    ModelState.AddModelError("", "DressType with the same name exists");
                }
            }

            ViewBag.Category = db.Categories;
            return View();
        }

        // GET: DressTypes/Details/5
      

        // GET: DressTypes/Create
      
        // GET: DressTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DressType dressType = db.DressTypes.Find(id);
            if (dressType == null)
            {
                return HttpNotFound();
            }
            return View(dressType);
        }

        // POST: DressTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DressTypeId,DressTypeName")] DressType dressType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dressType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dressType);
        }

        // GET: DressTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DressType dressType = db.DressTypes.Find(id);
            if (dressType == null)
            {
                return HttpNotFound();
            }
            return View(dressType);
        }

        // POST: DressTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DressType dressType = db.DressTypes.Find(id);
            db.DressTypes.Remove(dressType);
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
