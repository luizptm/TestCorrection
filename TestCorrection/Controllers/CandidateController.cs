using AutoMapper;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestCorrection.Library.Security;
using TestCorrection.Mappers;
using TestCorrection.Model;
using TestCorrection.Model.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Controllers
{
    public class CandidateController : ApplicationController<Candidate>
    {
        private Entities db = new Entities();

        IMapper mapper;

        private const int defaultPageSize = 10;

        public CandidateController()
        {
            Mapper.Initialize(MappingConfiguration.Configure);

            mapper = Mapper.Instance;
        }

        // GET: Candidate
        public async Task<ActionResult> Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            var list = await db.Candidate.ToListAsync();
            PagedList<Candidate> paged_list = new PagedList<Candidate>(list, currentPageIndex, defaultPageSize);
            return View(paged_list);
        }

        // GET: Candidate
        public async Task<PartialViewResult> PartialIndex(int? page, string grid_column = "", string grid_dir = "")
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;

            grid_column = HttpContext.Request["grid-column"] != null ? HttpContext.Request["grid-column"].ToString() : grid_column;
            grid_dir = HttpContext.Request["grid-dir"] != null ? HttpContext.Request["grid-dir"].ToString() : grid_dir;

            ViewBag.grid_column = grid_column;
            ViewBag.grid_dir = grid_dir;

            var list = await db.Candidate.ToListAsync();
            PagedList<Candidate> paged_list = new PagedList<Candidate>(list, currentPageIndex, defaultPageSize);
            return PartialView(paged_list);
        }

        // GET: Candidate/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: QuestionGrade/Details/5
        [HttpPost]
        public JsonResult GetDetails(int id)
        {
            Candidate Candidate = db.Candidate.Find(id);
            if (Candidate == null)
            {
                return Json(new { Status = 0, Message = "Not found" });
            }
            CandidateVM vm = mapper.Map<CandidateVM>(Candidate);
            return Json(new { Status = 1, Message = "Ok", Content = RenderPartialViewToString("Details", vm) });
        }

        // GET: Candidate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidate/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Candidate.Add(candidate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(candidate);
        }

        // GET: Candidate/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidate/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        // GET: Candidate/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = await db.Candidate.FindAsync(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Candidate candidate = await db.Candidate.FindAsync(id);
            db.Candidate.Remove(candidate);
            await db.SaveChangesAsync();
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
