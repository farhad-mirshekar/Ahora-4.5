using System.Web;
using System.Web.Optimization;

namespace Ahora.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryvalidate").Include(
                        "~/Scripts/jquery/jquery-validate.js"));
            bundles.Add(new StyleBundle("~/bundles/bootstrap4").Include(
                "~/Content/bootstrap4-rtl/bootstrap4-rtl.css"));
            bundles.Add(new StyleBundle("~/bundles/main").Include(
               "~/Content/main/main.css",
               "~/Content/fontawesome/font-awesome.css",
               "~/Content/main/PagedList.css"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap4js").Include(
                "~/scripts/bootstrap4/popper.min.js",
                "~/scripts/bootstrap4/bootstrap.min.js"
                ));

        }
    }
}