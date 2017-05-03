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
    public class QuestionController : ApplicationController<QuestionVM>
    {
        private Entities db = new Entities();

        private AccessControl ac = new AccessControl();

        IMapper mapper;

        public QuestionController()
        {
            Mapper.Initialize(MappingConfiguration.Configure);

            mapper = Mapper.Instance;
        }

        // GET: Question
        public ActionResult Index()
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<Question> dbList = db.Question;
            IEnumerable<QuestionVM> list = mapper.Map<IEnumerable<QuestionVM>>(dbList);
            return View(list);
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question e = db.Question.Find(id);
            QuestionVM vm = mapper.Map<QuestionVM>(e);
            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: Question/Details/5
        [HttpPost]
        public JsonResult GetDetails(int id)
        {
            Question Question = db.Question.Find(id);
            if (Question == null)
            {
                return Json(new { Status = 0, Message = "Not found" });
            }
            QuestionVM vm = mapper.Map<QuestionVM>(Question);
            return Json(new { Status = 1, Message = "Ok", Content = RenderPartialViewToString("Details", vm) });
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Number,Descripton")] QuestionVM vm)
        {
            if (ModelState.IsValid)
            {
                Question e = mapper.Map<Question>(vm);
                db.Question.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question e = db.Question.Find(id);
            QuestionVM vm = mapper.Map<QuestionVM>(e);
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(vm);
        }

        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Number,Descripton")] QuestionVM vm)
        {
            if (ModelState.IsValid)
            {
                Question e = mapper.Map<Question>(vm);
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question e = db.Question.Find(id);
            db.Question.Remove(e);
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
