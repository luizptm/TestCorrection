using System;
using System.Web.Security;
using TestCorrection.Model;
using TestCorrection.Model.Model;

namespace TestCorrection.Library.Security
{
    public class CustomMembershipUser : MembershipUser
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

		public CustomMembershipUser(User user)
			: base("CustomMembershipProvider", user.Name, user.CPF, user.Email, 
				string.Empty, string.Empty, true, false, user.CreationDate, DateTime.Now, user.LastActivityDate, user.LastPasswordChangedDate, DateTime.Now)
		{
            Id = user.Id;
			Name = user.Name;
            Email = user.Email;
            CPF = user.CPF;
			RoleId = user.RoleId;
			RoleName = user.Role.Name;
		}

        #endregion
    }
}