using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repo.Repository;
using Repo;

namespace App.Controllers
{
    public class TeacherController : Controller
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
        QuestionBankRepository QuestionBankRepo = new QuestionBankRepository();
        // GET: Teacher

        public ActionResult Index() //Complete
        {
            
            return View(EventRepo.GetAll());
        }

        [HttpGet]
        public ActionResult UserQuestionBank() //Complete
        {
            return View(QuestionBankRepo.GetAll().Where(qb=>qb.OwnerID==((UserInfo)Session["UserInfo"]).UserID));
        }
        [HttpGet]
        public ActionResult CreateBank() //Complete
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBank(QuestionBank qb)
        {
            qb.OwnerID= ((UserInfo)Session["UserInfo"]).UserID;
            QuestionBankRepo.Insert(qb);
            return RedirectToAction("AddQuestionToBank",new { QuestionBankID=qb.QuestionBankID});
        }
        [HttpGet]
        public ActionResult AddQuestionToBank(int QuestionBankID)
        {

            return View(QuestionBankRepo.Get(QuestionBankID));
        }
        [HttpPost]
        public ActionResult AddQuestionToBank(List<Question> questions, int QuestionBankID)
        {
            return null;
        }

        [HttpGet]
        public ActionResult ImportFile(int QuestionBankID)
        {
            return View(QuestionBankRepo.Get(QuestionBankID));

        }
        

        [HttpGet]
        public ActionResult CreateEvent() //Complete
        {
            TempData["EventType"] = EventTypeRepo.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult CreateEvent(Event ev) 
        {
            ev.EventStatusID = 1;
            ev.HostID = ((UserInfo)Session["UserInfo"]).UserID;
            if (ev.IsPublic == true) ev.EventTypeID = 1; else ev.EventTypeID = 2;
            EventRepo.Insert(ev);
            return RedirectToAction("AddQuestionEvent", new {  ev.EventID });
        }
        public ActionResult AddQuestionEvent(int EventID)
        {
            return View();
        }

    }
}