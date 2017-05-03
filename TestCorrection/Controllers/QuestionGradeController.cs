using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TestCorrection.Library.Security;
using TestCorrection.Mappers;
using TestCorrection.Model;
using TestCorrection.Model.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Controllers
{
    public class QuestionGradeController : ApplicationController<QuestionGradeVM>
    {
        private Entities db = new Entities();

        private AccessControl ac = new AccessControl();

        IMapper mapper;

        public QuestionGradeController()
        {
            Mapper.Initialize(MappingConfiguration.Configure);

            mapper = Mapper.Instance;
        }

        // GET: QuestionGrade
        public ActionResult Index()
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            IEnumerable<Question> dbListQuestion = db.Question;
            ViewBag.Question = new SelectList(dbListQuestion, "Id", "Number");

            IEnumerable<Grade> dbListGrade = db.Grade;
            ViewBag.Grade = new SelectList(dbListGrade, "Id", "Grade1");
            
            IEnumerable<QuestionGrade> dbList = db.QuestionGrade;
            IEnumerable<QuestionGradeVM> list = mapper.Map<IEnumerable<QuestionGradeVM>>(dbList);
            return View(list);
        }

        // GET: QuestionGrade/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGrade e = db.QuestionGrade.Find(id);
            QuestionGradeVM vm = mapper.Map<QuestionGradeVM>(e);
            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: QuestionGrade/Details/5
        [HttpPost]
        public JsonResult GetDetails(int id)
        {
            QuestionGrade QuestionGrade = db.QuestionGrade.Find(id);
            if (QuestionGrade == null)
            {
                return Json(new { Status = 0, Message = "Not found" });
            }
            QuestionGradeVM vm = mapper.Map<QuestionGradeVM>(QuestionGrade);
            return Json(new { Status = 1, Message = "Ok", Content = RenderPartialViewToString("Details", vm) });
        }

        // GET: QuestionGrade/Create
        public ActionResult Create()
        {
            IEnumerable<Question> dbListQuestion = db.Question;
            ViewBag.Question = new SelectList(dbListQuestion, "Id", "Number");

            IEnumerable<Grade> dbListGrade = db.Grade;
            ViewBag.Grade = new SelectList(dbListGrade, "Id", "Grade1");

            IEnumerable<Grade> dbList = db.Grade;
            ViewBag.Grade = new SelectList(dbList, "Id", "Grade1");
            return View();
        }

        // POST: QuestionGrade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionGradeVM vm)
        {
            if (ModelState.IsValid)
            {
                QuestionGrade e = mapper.Map<QuestionGrade>(vm);
                db.QuestionGrade.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            IEnumerable<Question> dbListQuestion = db.Question;
            ViewBag.Question = new SelectList(dbListQuestion, "Id", "Number");

            IEnumerable<Grade> dbListGrade = db.Grade;
            ViewBag.Grade = new SelectList(dbListGrade, "Id", "Grade1");

            return View(vm);
        }

        // GET: QuestionGrade/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionGrade e = db.QuestionGrade.Find(id);
            QuestionGradeVM vm = mapper.Map<QuestionGradeVM>(e);
            if (e == null)
            {
                return HttpNotFound();
            }

            IEnumerable<Question> dbListQuestion = db.Question;
            ViewBag.Question = new SelectList(dbListQuestion, "Id", "Number");

            IEnumerable<Grade> dbListGrade = db.Grade;
            ViewBag.Grade = new SelectList(dbListGrade, "Id", "Grade1");

            return View(vm);
        }

        // POST: QuestionGrade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionGradeVM vm)
        {
            if (ModelState.IsValid)
            {
                QuestionGrade e = mapper.Map<QuestionGrade>(vm);
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            IEnumerable<Question> dbListQuestion = db.Question;
            ViewBag.Question = new SelectList(dbListQuestion, "Id", "Number");

            IEnumerable<Grade> dbListGrade = db.Grade;
            ViewBag.Grade = new SelectList(dbListGrade, "Id", "Grade1");

            return View(vm);
        }

        // POST: QuestionGrade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionGrade e = db.QuestionGrade.Find(id);
            db.QuestionGrade.Remove(e);
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
