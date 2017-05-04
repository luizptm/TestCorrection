using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCorrection.Library;
using TestCorrection.Library.Security;
using TestCorrection.Mappers;
using TestCorrection.Model;
using TestCorrection.Model.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Controllers
{
    public class CorrigirController : ApplicationController<QuestionResultVM>
    {
        private Entities db = new Entities();

        private AccessControl ac = new AccessControl();

        IMapper mapper;

        public CorrigirController()
        {
            Mapper.Initialize(MappingConfiguration.Configure);

            mapper = Mapper.Instance;
        }

        // GET: Corrigir
        public ActionResult Index()
        {
            AccessControl ac = new AccessControl();
            string result = ac.HasAccess("administrator, user");
            if (result == "-1")
            {
                ModelState.AddModelError("error", TestCorrection.Resources.Resources.SessionExpired);
                return RedirectToAction("Index", "Login");
            }
            else if (result == "-2")
            {
                ModelState.AddModelError("error", TestCorrection.Resources.Resources.NoAccess);
                return RedirectToAction("Index", "Login");
            }

            var dbList = db.Question.ToList();
            return View(dbList);
        }

        // GET: Corrigir/Details/5
        [HttpPost]
        public JsonResult GetDetails(int id)
        {
            QuestionResult QuestionResult = db.QuestionResult.Find(id);
            if (QuestionResult == null)
            {
                return Json(new { Status = 0, Message = "Not found" });
            }
            QuestionResultVM vm = mapper.Map<QuestionResultVM>(QuestionResult);
            return Json(new { Status = 1, Message = "Ok", Content = RenderPartialViewToString("Details", vm) });
        }

        // GET: Corrigir/Corrigir/5
        public ActionResult Corrigir(int? id)
        {
            AccessControl ac = new AccessControl();
            if (ac.GetUser("Administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question e = db.Question.Find(id);

            if (e == null)
            {
                ModelState.AddModelError("error", "Registro não encontrado");
                return RedirectToAction("Index");
            }
            if (e != null)
            {
                IEnumerable<Grade> dbList = GetGrade(e.Id);
                ViewBag.Grade = new SelectList(dbList, "Id", "Grade1");

                int QuestionId = e.Id;
                return ShowView(QuestionId, false);
            }

            ModelState.AddModelError("error", "Erro inesperado. Tente novamente");
            return RedirectToAction("Index");
        }

        // POST: Corrigir/Corrigir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Corrigir(ImageCandidate ic)
        {
            AccessControl ac = new AccessControl();
            if (ac.GetUser("Administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                QuestionResult e = new QuestionResult();
                e.CandidateId = ic.CandidateId;
                e.QuestionId = ic.QuestiontId;
                e.Grade = Convert.ToDecimal(Request.Form["Grade"].ToString());
                db.QuestionResult.Add(e);
                //db.SaveChanges();

                ic.InUse = false;
                db.Entry(ic).State = EntityState.Modified;
                db.SaveChanges();

                IEnumerable<Grade> dbList = GetGrade(ic.QuestiontId);
                ViewBag.Grade = new SelectList(dbList, "Id", "Grade1");

                int QuestionId = ic.QuestiontId;
                return ShowView(QuestionId, true);
            }

            ModelState.AddModelError("error", "Erro inesperado. Tente novamente");
            return RedirectToAction("Index");
        }

        private IEnumerable<Grade> GetGrade(int questionId)
        {
            IEnumerable<Grade> dbList = new List<Grade>();
            IEnumerable<QuestionGrade> dbListQuestionGrade = db.QuestionGrade;
            if (dbListQuestionGrade.Count() == 0)
                dbList = db.Grade;
            else
            {
                dbListQuestionGrade = db.QuestionGrade.Where(x => x.QuestionId == questionId);
                if (dbListQuestionGrade.Count() == 0)
                {
                    dbList = db.Grade;
                }
                else
                {
                    dbList = db.QuestionGrade.Where(x => x.QuestionId == questionId).Select<QuestionGrade, Grade>(x => x.Grade1).ToList<Grade>();
                }
            }

            return dbList;
        }

        private ViewResult ShowView(int QuestionId, bool InUse)
        {
            ImageCandidate ic = new ImageCandidate();
            DbRawSqlQuery<Candidate> queryCandidate = db.Database.SqlQuery<Candidate>("SELECT TOP 1 Id, Name FROM Candidate c INNER JOIN ImageCandidate ic ON c.Id = ic.CandidateId " +
                                                        "WHERE ic.QuestiontId = " + QuestionId + " AND " +
                                                        "Id NOT IN (SELECT CandidateId from QuestionResult WHERE " +
                                                        "QuestionId = " + QuestionId + ") " +
                                                        "ORDER BY NEWID()");
            Int32 candidateId = queryCandidate != null ? queryCandidate.Single().Id : 0;

            IQueryable<ImageCandidate> query = db.ImageCandidate.Where(x => x.CandidateId == candidateId && x.QuestiontId == QuestionId);
            ic = query != null && query.Count() > 0 ? query.Single() : null;
            if (query != null && ic != null)
            {
                ic.InUse = false;
                db.Entry(ic).State = EntityState.Modified;
                db.SaveChanges();

                return View("Corrigir", ic);
            }
            return View("Index");
        }
    }
}