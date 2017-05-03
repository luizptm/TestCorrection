using System.Security.Principal;

namespace TestCorrection.Library.Security
{
    public static class SecurityExtentions
    {
        public static CustomPrincipal ToCustomPrincipal(this IPrincipal principal)
        {
			try
			{
				return (CustomPrincipal)principal;
			}
			catch
			{
			}
			return null;
        }
    }
}