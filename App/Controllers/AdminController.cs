using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repo;
using System.IO;
using Repo.Repository;

namespace App.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        UserInfoRepository usrepo = new UserInfoRepository();
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

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UserPanel()
        {
            return View(usrepo.GetAll().Where(us => us.UserType.UserTypeName != "Admin"));
        }
        [HttpGet]
        public ActionResult NewUser()
        {
            TempData["AccType"] = AccountTypeRepo.GetAll();
            TempData["UserType"] = UserTypeRepo.GetAll();
            TempData["UserStatus"] = UserStatusRepo.GetAll();

            return View();
        }
        [HttpPost]
        public ActionResult NewUser(UserInfo us)
        {
            us.JoiningDate = DateTime.Now;
            us.TotalEventsAttended = us.TotalHostedEvents = us.TotalParticipants = 0;
            usrepo.Insert(us);
            HttpPostedFileBase file1 = Request.Files[0];
            HttpPostedFileBase file2 = Request.Files[1];
            if (file1.ContentLength > 0)
            {
                var fileName = us.UserID + Path.GetExtension(file2.FileName);
                us.Portfolio = fileName;
                var path = Path.Combine(Server.MapPath("~/App_Data/Portfolio"), fileName);
                file2.SaveAs(path);
            }
            if (file2.ContentLength > 0)
            {
                var fileName = us.UserID + Path.GetExtension(file1.FileName);
                us.ProfilePic = fileName;
                var path = Path.Combine(Server.MapPath("~/App_Data/ProPic"), fileName);
                file1.SaveAs(path);
            }
            usrepo.Update(us);
            return View();
        }

        [HttpGet]
        public ActionResult EditUser(int UserID)
        {
            TempData["AccType"] = AccountTypeRepo.GetAll();
            TempData["UserType"] = UserTypeRepo.GetAll();
            TempData["UserStatus"] = UserStatusRepo.GetAll();

            return View(usrepo.Get(UserID));
        }
        [HttpPost]
        public ActionResult EditUser(UserInfo us)
        {
            usrepo.Insert(us);
            return RedirectToAction("UserDetail",new { UserID = us.UserID });
        }

        [HttpGet]
        public ActionResult UserDetail(int UserID)
        {
            return View(usrepo.Get(UserID));
        }

        [HttpGet]
        public ActionResult Subscription()
        {
            return View(AccountTypeRepo.GetAll());
        }
        [HttpPost]
        public ActionResult Subscription(AccountType at)
        {
            if(AccountTypeRepo.GetAll().Find(a=>a.AccountTypeName==at.AccountTypeName)==null)
            {
                AccountTypeRepo.Insert(at);
            }
            return RedirectToAction("Subscription");
        }
        [HttpGet]
        public ActionResult UserType()
        {
            return View(UserTypeRepo.GetAll());
        }
        [HttpPost]
        public ActionResult UserType(UserType ut)
        {
            if (UserTypeRepo.GetAll().Find(a => a.UserTypeName == ut.UserTypeName) == null)
            {
                UserTypeRepo.Insert(ut);
            }
            return RedirectToAction("UserType");
        }
        
        [HttpGet]
        public ActionResult StatusType()
        {
            return View(UserStatusRepo.GetAll());
        }
        [HttpPost]
        public ActionResult StatusType(UserStatu us)
        {
            if (UserStatusRepo.GetAll().Find(a => a.StatusName == us.StatusName) == null)
            {
                UserStatusRepo.Insert(us);
            }
            return RedirectToAction("StatusType");
        }

        [HttpGet]
        public ActionResult Institution()
        {
            return View(InstitutionRepo.GetAll());
        }
        [HttpPost]
        public ActionResult Institution(Institution ins)
        {
            if (InstitutionRepo.GetAll().Find(a => a.InstitutionName == ins.InstitutionName) == null)
            {
                InstitutionRepo.Insert(ins);
            }
            return RedirectToAction("Institution");
        }

        [HttpGet]
        public ActionResult Organization()
        {
            return View(OrganizationRepo.GetAll());
        }
        [HttpPost]
        public ActionResult Organization(Organization org)
        {
            if (OrganizationRepo.GetAll().Find(a => a.OrganizationName == org.OrganizationName) == null)
            {
                OrganizationRepo.Insert(org);
            }
            return RedirectToAction("Organization");
        }

        public ActionResult EventPanel()
        {
            return View(EventRepo.GetAll());
        }
        [HttpGet]
        public ActionResult EventPrivacy()
        {
            return View(EventTypeRepo.GetAll());
        }
        [HttpPost]
        public ActionResult EventPrivacy(EventType et)
        {
            if (EventTypeRepo.GetAll().Find(a => a.EventTypeName == et.EventTypeName) == null)
            {
                EventTypeRepo.Insert(et);
            }
            return RedirectToAction("EventPrivacy");
        }
        [HttpGet]
        public ActionResult EventStatus()
        {
            return View(EventStatusRepo.GetAll());
        }
        [HttpPost]
        public ActionResult EventStatus(EventStatu es)
        {
            if (EventStatusRepo.GetAll().Find(a => a.EventStatusName == es.EventStatusName) == null)
            {
                EventStatusRepo.Insert(es);
            }
            return RedirectToAction("EventStatus");
        }
        [HttpGet]
        public ActionResult EventRegistrationStatus()
        {
            return View(EventRegistrationStatusRepo.GetAll());
        }
        [HttpPost]
        public ActionResult EventRegistrationStatus(EventRegistrationStatu ers)
        {
            if (EventRegistrationStatusRepo.GetAll().Find(a => a.EventRegistrationStatusName == ers.EventRegistrationStatusName) == null)
            {
                EventRegistrationStatusRepo.Insert(ers);
            }
            return RedirectToAction("EventRegistrationStatus");
        }
        [HttpGet]
        public ActionResult EventInvitationStatus()
        {
            return View(EventInvitationStatusRepo.GetAll());
        }
        [HttpPost]
        public ActionResult EventInvitationStatus(EventInvitationStatu eis)
        {
            if (EventInvitationStatusRepo.GetAll().Find(a => a.EventInvitationStatusName == eis.EventInvitationStatusName) == null)
            {
                EventInvitationStatusRepo.Insert(eis);
            }
            return RedirectToAction("EventInvitationStatus");
        }
        [HttpGet]
        public ActionResult InstitutionCategoryType()
        {

            return View(InstitutionCategoryTypeRepo.GetAll());
        }

        [HttpPost]
        public ActionResult InstitutionCategoryType(InstitutionCategoryType ict)
        {
            if (InstitutionCategoryTypeRepo.GetAll().Find(a => a.InstitutionCategoryTypeName == ict.InstitutionCategoryTypeName) == null)
            {
                InstitutionCategoryTypeRepo.Insert(ict);
            }

            

            return RedirectToAction("InstitutionCategoryType");
        }
        [HttpGet]
        public ActionResult InstitutionCategoryClass(int InstitutionCategoryTypeID)
        {
            return View(InstitutionCategoryRepo.GetAll().Where(ict=>ict.InstitutionCategoryTypeID==InstitutionCategoryTypeID));
        }
        [HttpPost]
        public ActionResult InstitutionCategoryClass(InstitutionCategory ic)
        {
            if (InstitutionCategoryRepo.GetAll().Find(a => a.InstitutionCategoryClass == ic.InstitutionCategoryClass && a.InstitutionCategoryTypeID==ic.InstitutionCategoryTypeID) == null)
            {
                InstitutionCategoryRepo.Insert(ic);
            }
            return RedirectToAction("InstitutionCategoryClass", new { InstitutionCategoryTypeID = ic.InstitutionCategoryTypeID });
        }

        [HttpGet] public ActionResult Question()
        {
            return View(AllQuestionRepo.GetAll());
        }

        [HttpGet]
        public ActionResult QuestionDifficulty()
        {
            return View(QuestionDifficultyRepo.GetAll());
        }
        [HttpPost]
        public ActionResult QuestionDifficulty(QuestionDifficulty qd)
        {
            if (QuestionDifficultyRepo.GetAll().Find(a => a.QuestionDifficultyName == qd.QuestionDifficultyName) == null)
            {
                QuestionDifficultyRepo.Insert(qd);
            }
            return RedirectToAction("QuestionDifficulty");
        }
        [HttpGet]
        public ActionResult QuestionType()
        {
            return View(QuestionTypeRepo.GetAll());
        }
        [HttpPost]
        public ActionResult QuestionType(QuestionType qt)
        {
            if (QuestionTypeRepo.GetAll().Find(a => a.QuestionTypeName == qt.QuestionTypeName) == null)
            {
                QuestionTypeRepo.Insert(qt);
            }
            return RedirectToAction("QuestionType");
        }
        [HttpGet]
        public ActionResult Subject()
        {
            return View(SubjectRepo.GetAll());
        }
        [HttpPost]
        public ActionResult Subject(Subject sub)
        {
            if (SubjectRepo.GetAll().Find(a => a.SubjectName == sub.SubjectName) == null)
            {
                SubjectRepo.Insert(sub);
            }
            return RedirectToAction("Subject");
        }


    }
}