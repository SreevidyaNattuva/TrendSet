using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrendSet.Models;
using System.Web.Security;
using System.Windows.Forms;

namespace TrendSet.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private TrendSetContext context = new TrendSetContext();

        
    

    public ActionResult Home()
        {
            return View();
        }

       


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                //Encryption encryption = new Encryption();
                string encryptedPassword = Encryption.Encrypt(user.Password);
                bool isValid = context.UserDetails.Any(x => x.UserName ==user.UserName && x.Password == encryptedPassword);
                bool validUserid = context.UserDetails.Any(x => x.UserName == user.UserName);
                if (!validUserid)
                {
                    ModelState.AddModelError("", "User Id Not Exists");
                }
                else if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "UserDetails");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and password");
                }
            }
            return View();
        }



           





        public ActionResult Register()
        {

            IEnumerable<SelectListItem> items = GetRoles();

            ViewBag.Roles = items;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                //Encryption encryption = new Encryption();
                string encryptedPassword = Encryption.Encrypt(registerViewModel.Password);
                UserDetail user = new UserDetail
                {
                    UserName = registerViewModel.UserName,
                    Password = encryptedPassword,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    Email = registerViewModel.Email,
                    ContactNumber = registerViewModel.ContactNumber,
                    Gender = registerViewModel.Gender,
                    DoB = registerViewModel.DoB
                };
                bool isValid = context.UserDetails.Any(x => x.UserName == user.UserName);

                if (isValid)
                {
                    ModelState.AddModelError("", "User Name Already Exist  ");
                    return View();
                }
                else
                {


                    RoleLoginMapping roleLoginMapping = new RoleLoginMapping
                    {
                        RoleId = registerViewModel.RoleId
                    };
                    context.RoleLoginMappings.Add(roleLoginMapping);
                    context.UserDetails.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Registered Successfully");
                    return RedirectToAction("Login");

                }
            }
            else
            {
                IEnumerable<SelectListItem> items = GetRoles();

                ViewBag.Roles = items;
                return View();
            }

            }

        private IEnumerable<SelectListItem> GetRoles()
        {
            return context.Roles.Where(x => x.RoleName != "Admin").Select(c => new SelectListItem
            {

                Value = c.RoleId.ToString(),
                Text = c.RoleName

            });
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Home");
        }
    }
}