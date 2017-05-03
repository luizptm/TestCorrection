using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;
using AutoMapper;
using TestCorrection.Library;
using TestCorrection.Library.Security;
using TestCorrection.Model;
using TestCorrection.ViewModels;
using TestCorrection.Mappers;
using TestCorrection.Model.Model;

namespace TestCorrection.Controllers
{
    [TimeAuthorize]
    public class UsersController : ApplicationController<UserVM>
    {
        private Entities db = new Entities();

        AccessControl ac = new AccessControl();

        IMapper mapper;

        public UsersController()
        {
            Mapper.Initialize(MappingConfiguration.Configure);

            mapper = Mapper.Instance;
        }

        // GET: /Users/
        [HttpGet]
        public ActionResult Index()
        {
			if (ac.GetUser("administrator") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			var dbList = db.User.ToList();
            IEnumerable<UserVM> list= mapper.Map<IEnumerable<UserVM>>(dbList);
			return View("Index", list);
        }

        //
        // GET: /Users/Details/5
        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            User User = db.User.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
			UserVM vm = mapper.Map<UserVM>(User);
            return View(vm);
        }

		[HttpPost]
        public JsonResult GetDetails(int id)
        {
			User User = db.User.Find(id);
            if (User == null)
            {
                return Json(new { Status = 0, Message = "Not found" });
            }
			UserVM vm = mapper.Map<UserVM>(User);
            return Json(new { Status = 1, Message = "Ok", Content = RenderPartialViewToString("Details", vm) });
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (ac.GetUser("administrator") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVM vm)
        {
            if (ModelState.IsValid)
            {
				User User = mapper.Map<User>(vm);
                db.User.Add(User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        //
        // GET: /Users/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            User User = db.User.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
			UserVM vm = mapper.Map<UserVM>(User);

            return View(vm);
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserVM vm)
        {
			if (ModelState.IsValid)
            {
				User User= mapper.Map<User>(vm);
                db.Entry(User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            User User = db.User.Find(id);
            if (User == null)
            {
                return HttpNotFound();
            }
			User vm = mapper.Map<User>(User);
            return View(vm);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public String DeleteConfirmed(int id)
        {
			User item = db.User.Find(id);
			if (item != null)
			{
				db.User.Remove(item);
				return db.SaveChanges().ToString();
				//return RedirectToAction("Index");
			}
			else
			{
				return "0";
			}
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

	}
}