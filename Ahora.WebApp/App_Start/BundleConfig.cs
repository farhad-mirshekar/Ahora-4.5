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
                        "~/Scripts/jquery/jquery-{version}.js",
                        "~/Scripts/jquery/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap4").Include(
                "~/Content/bootstrap4-rtl/bootstrap4-rtl.css"));

            bundles.Add(new StyleBundle("~/bundles/main").Include(
               "~/Content/main/main.css",
               "~/Content/fontawesome/font-awesome.css",
               "~/Content/main/PagedList.css"));

            bundles.Add(new StyleBundle("~/bundles/jquery-ui").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/theme.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/fancyboxcss").Include(
             "~/Content/fancybox/jquery.fancybox.css",
             "~/Content/fancybox/jquery.fancybox-buttons.css",
             "~/Content/fancybox/jquery.fancybox-thumbs.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap4js").Include(
                "~/scripts/bootstrap4/popper.min.js",
                "~/scripts/bootstrap4/bootstrap.min.js",
                "~/scripts/custom/auto-complate.js",
                "~/scripts/noty/packaged/jquery.noty.packaged.min.js",
                "~/scripts/custom/ajaxcart.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/mainSite").Include(
                "~/scripts/jquery.unobtrusive-ajax.js",
                "~/scripts/custom/jquery.unobtrusive-ajax.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/fancyboxjs").Include(
                "~/scripts/fancybox/jquery.mousewheel-3.0.6.pack.js",
             "~/scripts/fancybox/jquery.fancybox.js",
             "~/scripts/fancybox/jquery.fancybox-buttons.js",
             "~/scripts/fancybox/jquery.fancybox-media.js"));

        }
    }
}