using System.Web;
using System.Web.Optimization;

namespace BatchJob
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var lessBundle = new Bundle("~/content/site").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.less");

            lessBundle.Transforms.Add(new LessTransform());
            lessBundle.Transforms.Add(new CssMinify());
            bundles.Add(lessBundle);

            bundles.Add(new StyleBundle("~/content/jquerybootstrap").Include(
                      "~/Content/jquery.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerybootstrap").Include
                ("~/Scripts/jquery.bootstrap.js"));

            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/angular", "http://cdn.static.runoob.com/libs/angular.js/1.4.6/angular.min.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
