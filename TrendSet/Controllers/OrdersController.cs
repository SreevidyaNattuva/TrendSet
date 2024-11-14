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
    public class OrdersController : Controller
    {
        private TrendSetContext db = new TrendSetContext();
        
        [Authorize(Roles="Customer")]
        public ActionResult OrdersViewForCustomer()
        {
            string userName = User.Identity.Name;
            int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();

            var neworders = (from c in db.Orders where c.Status == "New" && c.CustomerId == id select c).ToList();

            var activeorders = (from c in db.Orders where c.Status == "Active" && c.CustomerId == id select c).ToList();

            var completedorders = (from c in db.Orders where c.Status == "Completed" && c.CustomerId == id select c).ToList();

            ViewBag.NewOrders = neworders;
            ViewBag.ActiveOrders = activeorders;
            ViewBag.CompletedOrders = completedorders;
            TempData["Payment_Message"] = TempData["Payment_Successsful"];
            return View();

        }
        [Authorize(Roles = "Customer")]
        public ActionResult ViewOrderDetailsForCustomer(int? id)
        {
            var order = (from c in db.Orders where c.OrderId == id select c).ToList();
            return View(order);
        }
        [Authorize(Roles = "Customer")]
        public ActionResult PaymentViewForCustomer()
        {
            string userName = User.Identity.Name;
            int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();

            

            var activeorders = (from c in db.Orders where c.Status == "Active" && c.CustomerId == id select c).ToList();

            var completedorders = (from c in db.Orders where c.Status == "Completed" && c.CustomerId == id select c).ToList();

            
            ViewBag.ActiveOrders = activeorders;
            ViewBag.CompletedOrders = completedorders;
            TempData["Payment_Message"] = TempData["Payment_Successsful"];
            TempData["Payment_UnsuccesfulMessage"] = TempData["Payment_UnSuccesssful"];

            return View();
        }
        [Authorize(Roles = "Customer")]
        public ActionResult MakePayment(int? id)
        {
            TempData["Id"] = id;
            TempData["SecondId"] = id;
            TempData["ThirdId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult MakePayment(OnlinePayment onlinepayment)
        {
            int id = Convert.ToInt32(TempData["Id"]);
            if (ModelState.IsValid)
            {
                bool validpayment = db.OnlinePayments.Any(c => c.CardNumber == onlinepayment.CardNumber && c.ExpireDate == onlinepayment.ExpireDate && c.CVV == onlinepayment.CVV);
                if (validpayment)
                {
                    int thirdId = Convert.ToInt32(TempData["ThirdId"]);
                    var order = (from c in db.Orders where c.OrderId == thirdId select c).SingleOrDefault();
                    order.BillStatus = "Paid";
                    db.SaveChanges();
                    TempData["Payment_Successsful"] = "Payment Successful";
                    return RedirectToAction("PaymentViewForCustomer");
                }
                int secondId = Convert.ToInt32(TempData["SecondId"]);
                var orderunpaid = (from c in db.Orders where c.OrderId == secondId select c).SingleOrDefault();
                orderunpaid.BillStatus = "UnPaid";
                db.SaveChanges();
                TempData["Payment_UnSuccesssful"] = "Payment UnSuccessful,Please check your credentials and try again";
                return RedirectToAction("PaymentViewForCustomer");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminViewOrders()
        {
            var orders = db.Orders.Include(o => o.TailorDressCategoryMapping);
            return View(orders.ToList());
            
        }
        [Authorize(Roles = "Tailor")]
        public ActionResult TailorViewOrders()
        {
            string userName = User.Identity.Name;
            int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();
            var order = (from c in db.Orders where c.TailorDressCategoryMapping.UserId == id select c).ToList();
            
            return View(order.ToList());
        }
        [Authorize(Roles = "Tailor")]
        public ActionResult NewToActive(int? id)
        {
            TempData["orderId"] = id;
            int tcmId = (from c in db.Orders where c.OrderId == id select c.Id).SingleOrDefault();
            int cost= (from c in db.TailorDressCategoryMappings where c.Id ==tcmId select c.Cost).SingleOrDefault();
            var order = (from c in db.Orders where c.OrderId == id select c).SingleOrDefault();
            order.Bill = cost;
            var billAndExpectedDate = (from c in db.Orders where c.OrderId == id select c).SingleOrDefault();
            

            return View(billAndExpectedDate);
        }
        [HttpPost]
        public ActionResult NewToActive(DateTime expectedDate,int? bill)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(TempData["orderId"]);
                var order = (from c in db.Orders where c.OrderId == id select c).SingleOrDefault();
                order.Status = "Active";
                order.ExpectedDate = expectedDate;
                order.Bill = bill;
                db.SaveChanges();
                return RedirectToAction("TailorViewOrders");
            }
            else
            {
                ModelState.AddModelError("", "please enter the expected date");
                return View("NewToActive");
            }

        }
        [Authorize(Roles = "Tailor")]
        public ActionResult ActiveToComplete(int? id)
        {
            var order = (from c in db.Orders where c.OrderId == id select c).SingleOrDefault();
            order.Status = "Completed";
            db.SaveChanges();

            return View("TailorViewOrders");
        }
        [Authorize(Roles = "Tailor")]
        public ActionResult NewOrders()
        {
            string userName = User.Identity.Name;
            int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();
            var order = (from c in db.Orders where c.Status == "New" && c.TailorDressCategoryMapping.UserId==id select c).ToList();

            return View(order);
        }
        [Authorize(Roles = "Tailor")]
        public ActionResult ActiveOrders()
        {
            string userName = User.Identity.Name;
            int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();
            var order = (from c in db.Orders where c.Status == "Active" && c.TailorDressCategoryMapping.UserId == id select c).ToList();

            return View(order);
        }
        [Authorize(Roles = "Tailor")]
        public ActionResult CompletedOrders()
        {
            string userName = User.Identity.Name;
            int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();
            var order = (from c in db.Orders where c.Status == "Completed" && c.TailorDressCategoryMapping.UserId == id select c).ToList();

            return View(order);
        }


        [Authorize(Roles = "Customer")]
        public ActionResult OrderPlacing()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName");
            TempData["Display_Message"] = TempData["Registered_Successfully"];
            return View();
        }
        [HttpPost]
        public ActionResult OrderPlacing(TailorDressCategoryMapping user)
        {
            if (ModelState.IsValid)
            {

                var tailor = (from c in db.TailorDressCategoryMappings where c.CategoryId == user.CategoryId && c.DressTypeId == user.DressTypeId select c).ToList();
                bool validOrder = db.TailorDressCategoryMappings.Any(t => t.CategoryId == user.CategoryId && t.DressTypeId == user.DressTypeId);
                if (!validOrder)
                {
                    ModelState.AddModelError("", "We dont stitch such patterns");

                }
                else 
                {
                    return View("TailorsList", tailor);
                }
                
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.DressTypeId = new SelectList(db.DressTypes, "DressTypeId", "DressTypeName");
            return View();
        }


        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.TailorDressCategoryMapping);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Id,CustomerId,TopMaterialType,TopLengths,Neck,TopWaist,Chest,ShoulderLength,BottomMaterialType,BottomLength,Hip,KneeLength,ExpectedDate,Courier,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.ExpectedDate < DateTime.Now.Date)
                {
                    ModelState.AddModelError("", "Enter Proper Expected Date");
                }
                else
                {
                    string userName = User.Identity.Name;
                    int id = (from c in db.UserDetails where c.UserName == userName select c.UserId).SingleOrDefault();
                    order.CustomerId = id;
                    order.Status = "New";
                    order.BillStatus = "Unpaid";
                    db.Orders.Add(order);
                    db.SaveChanges();
                    TempData["Registered_Successfully"] = "Order Placed successfully";
                    return RedirectToAction("OrderPlacing");
                    
                }
            }

           
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.TailorDressCategoryMappings, "Id", "Id", order.Id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Id,CustomerId,TopMaterialType,TopLengths,Neck,TopWaist,Chest,ShoulderLength,BottomMaterialType,BottomLength,Hip,KneeLength,ExpectedDate,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ActiveOrders");
            }
            ViewBag.Id = new SelectList(db.TailorDressCategoryMappings, "Id", "Id", order.Id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
