using System;
using System.Web;
using System.Web.Optimization;

namespace TestCorrection
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(
                new ScriptBundle("~/scripts")
                .Include("~/Scripts/app.MainSite.*")
                .Include("~/Scripts/app.MainSite.Admin.js")
                .Include("~/Scripts/gridmvc.js")
                .Include("~/Scripts/gridmvc-ext.js")
                .Include("~/Scripts/gridmvc.customwidgets.js")
                .Include("~/Scripts/PagedList*")
                .Include("~/Scripts/URI.js")
            );

            bundles.Add(
              new ScriptBundle("~/Scripts/globalize")
                .Include("~/Scripts/globalize*")
                .Include("~/Scripts/globalize/*.js")
            );

            bundles.Add(
                new ScriptBundle("~/scripts/jquery")
                .Include("~/Scripts/jquery-3.1.1.js")
            );

            bundles.Add(
                new ScriptBundle("~/scripts/jqueryval")
                .Include("~/Scripts/jquery.validate-vsdocs.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.globalize.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
            );

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(
                new ScriptBundle("~/scripts/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle("~/scripts/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-dialog.js",
                "~/Scripts/respond.js"));

            //***************

            bundles.Add(
                new StyleBundle("~/Styles")
                .Include("~/Styles/ladda-bootstrap/*.css")
                .Include("~/Styles/bootstrap*")
                .Include("~/Styles/font*")
                .Include("~/Styles/Gridmvc*")
                .Include("~/Styles/PagedList.css")
                .Include("~/Styles/Site.css")
                );

            BundleTable.EnableOptimizations = false;//change on production!
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.map");
        }
    }
}
