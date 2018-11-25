using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repo.Repository;
using Repo;

namespace App.Controllers
{
    public class LoginController : Controller
    {


        UserInfoRepository UsRepo = new UserInfoRepository();
        UserStatusRepository UserStatusRepo = new UserStatusRepository();
        UserTypeRepository UserTypeRepo = new UserTypeRepository();
        AccountTypeRepository AccountTypeRepo = new AccountTypeRepository();
        InstitutionRepository InstitutionRepo = new InstitutionRepository();
        OrganizationRepository OrganizationRepo = new OrganizationRepository();
        EventRepository EventRepo = new EventRepository();
        EventTypeRepository EventTypeRepo = new EventTypeRepository();
        EventStatusRepository EventStatusRepo = new EventStatusRepository();
        EventRegistrationStatusRepository EventRegistrationStatusRepo = new EventRegistrationStatusRepository();
        EventInvitationStatusRepository EventInvitationStatusRepo = new EventInvitationStatusRepository();
        InstitutionCategoryRepository InstitutionCategoryRepo = new InstitutionCategoryRepository();
        InstitutionCategoryTypeRepository InstitutionCategoryTypeRepo = new InstitutionCategoryTypeRepository();
        AllQuestionRepository AllQuestionRepo = new AllQuestionRepository();
        QuestionDifficultyRepository QuestionDifficultyRepo = new QuestionDifficultyRepository();
        QuestionTypeRepository QuestionTypeRepo = new Repo.Repository.QuestionTypeRepository();
        SubjectRepository SubjectRepo = new SubjectRepository();

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
                    Session["UserInfo"] = user;
                    
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
        [HttpGet]
        public ActionResult SignUp()
        {
            TempData["UserType"] = UserTypeRepo.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserInfo us)
        {
            us.AccountTypeID = 2;
            us.StatusID = 1;
            us.JoiningDate = DateTime.Now;
            us.TotalEventsAttended = us.TotalHostedEvents = us.TotalParticipants = 0;
            UsRepo.Insert(us);
           
           
            return RedirectToAction("Login");
        }
    }
}