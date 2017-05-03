using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using TestCorrection.Model;
using TestCorrection.Model.Model;
using TestCorrection.ViewModels;

namespace TestCorrection.Library.Security
{
    public class CustomMembershipProvider : MembershipProvider
    {
		#region Properties

		private int _cacheTimeoutInMinutes = 30;

        #endregion

        private Entities db = new Entities();

        #region Overrides of MembershipProvider

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to validate. </param><param name="password">The password for the specified user. </param>
        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            User user = null;
            using (var context = new Entities())
            {
                Crypto crypto = new Crypto();
                String md5Password = crypto.GetMd5Hash(password);

                //System.Data.Entity.Infrastructure.DbRawSqlQuery q = db.Database.SqlQuery(typeof(User), "SELECT u.* FROM User u WHERE CPF = @P0", username);
                user = (from u in context.User
                                join r in context.Role on u.RoleId equals r.Id
                                where String.Compare(u.CPF, username, StringComparison.InvariantCulture) == 0
                                && String.Compare(u.Password, md5Password, StringComparison.OrdinalIgnoreCase) == 0
                        select u)
                        .FirstOrDefault();

                //return user != null;
                return user != null;
            }
        }

		/// <summary>
		/// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
		/// </returns>
		/// <param name="username">The name of the user to get information for.</param>
		/// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			var cacheKey = string.Format("UserData_{0}", username);
			if (HttpRuntime.Cache[cacheKey] != null)
				return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

			using (var context = new Entities())
			{
                /*
                System.Data.Entity.Infrastructure.DbRawSqlQuery q = null;
                System.Data.Entity.Infrastructure.DbRawSqlQuery<User> qUser = null;
                q = db.Database.SqlQuery(typeof(User), "SELECT u.*, r.* FROM [User] u, Role r WHERE u.RoleId = r.Id AND CPF = @P0", username);
                qUser = db.Database.SqlQuery<User>("SELECT u.*, r.* FROM [User] u, Role r WHERE u.RoleId = r.Id AND CPF = @P0", username);
                */
                User user = (from u in context.User
                                join r in context.Role on u.RoleId equals r.Id
							    where String.Compare(u.CPF, username, StringComparison.InvariantCulture) == 0
							    select u)
                            .FirstOrDefault();
                
                if (user == null) return null;

				CustomMembershipUser membershipUser = new CustomMembershipUser(user);

				//Store in cache
				HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

				return membershipUser;
			}
		}
 
        #endregion
 
        #region Overrides of MembershipProvider that throw NotImplementedException
 
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }
 
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
 
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
 
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
 
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
 
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
 
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
 
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
 
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }
 
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
 
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }
 
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
 
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
 
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
 
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
 
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
 
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
 
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
 
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
 
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
 
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
 
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
 
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
 
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
 
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
 
        #endregion
    }
}