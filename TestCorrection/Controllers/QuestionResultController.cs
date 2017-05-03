using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCorrection.Model;
using TestCorrection.ViewModels;
using AutoMapper;
using TestCorrection.Library.Security;
using TestCorrection.Mappers;
using TestCorrection.Model.Model;

namespace TestCorrection.Controllers
{
    public class QuestionResultController : ApplicationController<QuestionResultVM>
    {
        private Entities db = new Entities();

        private AccessControl ac = new AccessControl();

        IMapper mapper;

        public QuestionResultController()
        {
            Mapper.Initialize(MappingConfiguration.Configure);

            mapper = Mapper.Instance;
        }

        // GET: Correction
        public ActionResult Index()
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var dbList = db.QuestionResult.ToList();
            IEnumerable<QuestionResultVM> list = mapper.Map<IEnumerable<QuestionResultVM>>(dbList);
            return View(list);
        }

        // GET: QuestionResult/Details/5
        public ActionResult Details(int id = 0)
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            QuestionResult QuestionResult = db.QuestionResult.Find(id);
            if (QuestionResult == null)
            {
                return HttpNotFound();
            }
            QuestionResultVM vm = Mapper.Map<QuestionResultVM>(QuestionResult);
            return View(vm);
        }

        // GET: QuestionResult/Details/5
        [HttpPost]
        public JsonResult GetDetails(int id)
        {
            QuestionResult QuestionResult = db.QuestionResult.Find(id);
            if (QuestionResult == null)
            {
                return Json(new { Status = 0, Message = "Not found" });
            }
            QuestionResultVM vm = Mapper.Map<QuestionResultVM>(QuestionResult);
            return Json(new { Status = 1, Message = "Ok", Content = RenderPartialViewToString("Details", vm) });
        }

        // GET: Correction/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.QuestionResultId = new SelectList(db.QuestionResult, "Id", "Type");
            return View();
        }

        // POST: Correction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionResultVM vm)
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                QuestionResult e = Mapper.Map<QuestionResult>(vm);
                db.QuestionResult.Add(e);
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionResultId = new SelectList(db.QuestionResult, "Id", "Type", vm.QuestionId);
            return View(vm);
        }

        // GET: Correction/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionResult e = db.QuestionResult.Find(id);
            QuestionResultVM vm = mapper.Map<QuestionResultVM>(e);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Correction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionResultVM vm)
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                Question e = mapper.Map<Question>(vm);
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionResultId = new SelectList(db.QuestionResult, "Id", "Type", vm.QuestionId);
            return View(vm);
        }

        // POST: Correction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionResult e = db.QuestionResult.Find(id);
            db.QuestionResult.Remove(e);
            db.SaveChangesAsync();
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
