using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestCorrection.Library;
using TestCorrection.Library.Helper;
using TestCorrection.Library.Security;
using TestCorrection.Model;
using TestCorrection.Model.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Controllers
{
	public class AdminController : BaseController
	{
		private Entities db = new Entities();

		private AccessControl ac = new AccessControl();

		public ActionResult ChangePassword()
		{
			if (ac.GetUser("administrator") == null)
			{
				return RedirectToAction("Index", "Login");
			}
			
			MembershipUser user = ac.GetUser();
			CustomMembershipUser customUser = (CustomMembershipUser)user;

			LoginChangePassword vm = new LoginChangePassword();
			vm.UserId = customUser != null ? customUser.Id : 0;
			vm.CPF = customUser != null ? customUser.CPF : "";
			return View(vm);
		}

		[HttpPost]
		public ActionResult ChangePassword(LoginChangePassword vm)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(vm.CPF, vm.OldPassword))
				{
					ModelState.AddModelError("error", TestCorrection.Resources.Resources.PasswordNotMatchError); 
				}
				else if (vm.NewPassword.Trim().ToLower() == vm.ConfirmPassword.Trim().ToLower())
				{
					ModelState.AddModelError("error", TestCorrection.Resources.Resources.LoginError); 
				}
				else
				{
                    Crypto crypto = new Crypto();
                    String newMd5Password = crypto.GetMd5Hash(vm.NewPassword);
                    User User = new User();
					User = db.User.Find(vm.UserId);                    
					User.Password = newMd5Password;
					db.Entry(User).State = EntityState.Modified;
					db.SaveChanges();
					
					ViewBag.Message = TestCorrection.Resources.Resources.PasswordChangeSucces;
					return View(vm);
				}
			}
			return View(vm);
		}
    
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(LoginRegister vm)
		{
			if (ModelState.IsValid)
			{
				List<String> trigrams = db.User.Select<User, string>(x => x.CPF).ToList<String>();
				if (trigrams.Contains(vm.CPF.Trim().ToLower()))
				{
					//ModelState.AddModelError("error", TestCorrection.Resources.Resources.UserNameIsAlreadyUsedError); 
					ModelState.AddModelError("error", "CPF já cadastrado");
				}
				else
				{
                    Crypto crypto = new Crypto();
                    String md5Password = crypto.GetMd5Hash(vm.Password);

                    User User = new User();
                    User.Name = vm.Name;
                    User.CPF = vm.CPF;
                    User.Email = vm.Email;
                    User.Password = md5Password;
					User.RoleId = 1;//1 - admin, 2 - user
					
					User.CreationDate = DateTime.Now;
					db.Entry(User).State = EntityState.Added;
					db.SaveChanges();
					
					ViewBag.Message = TestCorrection.Resources.Resources.CreateANewAccountSuccess;
					return View(vm);
				}
			}
			return View(vm);
		}
	}
}