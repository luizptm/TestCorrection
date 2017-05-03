using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestCorrection.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index()
        {
            string data = System.IO.File.ReadAllText(HttpContext.Server.MapPath("/logs/logfileSendings.txt"), System.Text.Encoding.UTF8);
            if (String.IsNullOrEmpty(data))
            {
                data = Resources.Resources.NoDataToShow;
            }
            else
            {
                data = data.Replace("]", "]<br/>");
            }
            return PartialView("Logs", data);
        }
    }
}