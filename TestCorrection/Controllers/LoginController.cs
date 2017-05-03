using System.Web.Mvc;
using System.Web.Security;
using TestCorrection.Library.Security;
using TestCorrection.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Controllers
{
	public class LoginController : BaseController
	{
		[HttpGet]
		[AllowAnonymous]
		public ActionResult Index(string returnUrl = "")
		{
			if (User.Identity.IsAuthenticated)
			{
				return LogOut();
			}

			ViewBag.ReturnUrl = returnUrl;
            return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult Index(Login login, string returnUrl = "")
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(login.CPF, login.Password))
				{
					Session["login"] = login;
					return RedirectToLocal(returnUrl);
				}
				ModelState.AddModelError("error", TestCorrection.Resources.Resources.LoginError);
			}
			return View(login);
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Login", null);
		}

		#region Helpers

		private ActionResult RedirectToLocal(string returnUrl = "")
		{
			if (returnUrl != "" && Url.IsLocalUrl(returnUrl) && !returnUrl.Contains("Login"))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "MainSite");
			}
		}

		#endregion
	}
}