using AutoMapper;
using Grid.Mvc.Ajax.GridExtensions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestCorrection.Library;
using TestCorrection.Library.Helper;
using TestCorrection.Library.Security;
using TestCorrection.Mappers;
using TestCorrection.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Controllers
{
    public class MainSiteController : BaseController
    {
        #region Actions

        // GET: /MainSite/AccessDenied
        [HttpGet]
		public ActionResult AccessDenied()
        {
            return View();
        }

		// GET: /MainSite/
		[HttpGet]
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

            return View();
        }

		#endregion

    }
}
