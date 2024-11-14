using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using TrendSet.Models;


namespace TrendSet.Controllers
{
    [Authorize]
    public class TailorDressCategoryMappingsController : Controller
    {
        private TrendSetContext db = new TrendSetContext();



        //GET: TailorDressCategoryMappings
        [Authorize(Roles = "Customer")]
        public ActionResult SearchTailor()
        {
            var tailors = db.TailorDressCategoryMappings.DistinctBy(t => t.UserId).ToList();
            return View(tailors);
        }
        [Authorize(Roles = "Customer")]
        public ActionResult FindIndex(int? id)
        {
            var tailorDressCategoryMappings = (from t in db.TailorDressCategoryMappings where t.UserId == id select t).ToList();
            return View(tailorDressCategoryMappings);
        }
        [Authorize(Roles = "Customer")]
        public ActionResult ViewTailor()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName");
            return View();
        }
        [HttpPost]
        public ActionResult ViewTailor(TailorDressCategoryMapping user)
        {
            
            if (ModelState.IsValid)
            {
                var tailor = (from c in db.TailorDressCategoryMappings where c.CategoryId == user.CategoryId && c.DressTypeId == user.DressTypeId select c).ToList();
                bool validOrder = db.TailorDressCategoryMappings.Any(t => t.CategoryId == user.CategoryId && t.DressTypeId == user.DressTypeId);
                if (!validOrder)
                {
                    ModelState.AddModelError("", "Tailor with such mappping is not found");
                    
                }
                else
                {
                    return View("TailorsList", tailor);
                }
               
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName");

            return View("ViewTailor");
        }




        [Authorize(Roles ="Tailor")]
        public ActionResult Index()
        {
            var tailorDressCategoryMappings = db.TailorDressCategoryMappings.Include(t => t.Category).Include(t => t.DressType).Include(t => t.UserDetail);
            return View(tailorDressCategoryMappings.ToList());
        }




        // GET: TailorDressCategoryMappings/Create
        [Authorize(Roles = "Tailor")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName");
            
            
            return View();
        }

        // POST: TailorDressCategoryMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,DressTypeId,UserId,TypeId,Cost")] TailorDressCategoryMapping tailorDressCategoryMapping)
        {
            if (ModelState.IsValid)
            {
                string userName= User.Identity.Name;
                int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();
                tailorDressCategoryMapping.UserId = id;
                db.TailorDressCategoryMappings.Add(tailorDressCategoryMapping);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", tailorDressCategoryMapping.CategoryId);
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName", tailorDressCategoryMapping.DressTypeId);
            
           
            return View(tailorDressCategoryMapping);
        }

        // GET: TailorDressCategoryMappings/Edit/5
        [Authorize(Roles = "Tailor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailorDressCategoryMapping tailorDressCategoryMapping = db.TailorDressCategoryMappings.Find(id);
            if (tailorDressCategoryMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", tailorDressCategoryMapping.CategoryId);
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName", tailorDressCategoryMapping.DressTypeId);
            
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", tailorDressCategoryMapping.UserId);
            return View(tailorDressCategoryMapping);
        }

        // POST: TailorDressCategoryMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,DressTypeId,UserId,TypeId,Cost")] TailorDressCategoryMapping tailorDressCategoryMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tailorDressCategoryMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", tailorDressCategoryMapping.CategoryId);
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName", tailorDressCategoryMapping.DressTypeId);
            
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", tailorDressCategoryMapping.UserId);
            return View(tailorDressCategoryMapping);
        }

        // GET: TailorDressCategoryMappings/Delete/5
        [Authorize(Roles = "Tailor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TailorDressCategoryMapping tailorDressCategoryMapping = db.TailorDressCategoryMappings.Find(id);
            if (tailorDressCategoryMapping == null)
            {
                return HttpNotFound();
            }
            return View(tailorDressCategoryMapping);
        }

        // POST: TailorDressCategoryMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TailorDressCategoryMapping tailorDressCategoryMapping = db.TailorDressCategoryMappings.Find(id);
            db.TailorDressCategoryMappings.Remove(tailorDressCategoryMapping);
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
