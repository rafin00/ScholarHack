using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repo;

namespace App.Controllers
{
    public class LoginController : Controller
    {
        
        UserInfoRepository UsRepo= new UserInfoRepository();
        

        [HttpGet]
        public ActionResult Login()
        {
            if(Session["UserTypeName"]!=null)
            {
                if(Session["UserTypeName"].ToString()=="Admin") return RedirectToAction("Index", "Admin");
                if (Session["UserTypeName"].ToString() == "Teacher") return RedirectToAction("Index", "Teacher");
                else return RedirectToAction("Index", "Student");
            }
            else
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserInfo us)
        {
             UserInfo user =  UsRepo.GetByEmail(us.Email);
            if(user!=null)
            {
                if (user.Password == us.Password)
                {
                    Session["UserTypeName"] = user.UserType.UserTypeName;
                    if (user.UserType.UserTypeName == "Admin") return RedirectToAction("Index", "Admin");
                    if (user.UserType.UserTypeName == "Teacher") return RedirectToAction("Index", "Teacher");
                    else return RedirectToAction("Index", "Student");
                }
                else
                {
                    TempData["Err"] = "Invalid Credentials";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["Err"] = "Invalid Credentials";
                return RedirectToAction("Login");
            }
        }
    }
}