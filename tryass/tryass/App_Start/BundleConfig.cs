using System.Web;
using System.Web.Optimization;

namespace tryass
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/datable").Include(
            "~/Scripts/jquery.dataTables.min.js"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new Bundle("~/bundles/datepicker").Include(
                "~/Scripts/moment.min.js",
                "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/mapbox").Include(
                "~/Scripts/map.js"));




            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/bootstrap-datetimepicker.css"));

            bundles.Add(new StyleBundle("~/Content/Logincss").Include(
                "~/Content/LoginFormStyles.css"));


            bundles.Add(new StyleBundle("~/Content/mainCss").Include(
                "~/Content/animate.css",
                "~/Content/bootstrap.min.css",
                "~/Content/owl.carousel.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/font/flaticon.css",
                "~/Content/main.css"
                ));

            bundles.Add(new StyleBundle("~/Content/glassshow").Include(
                "~/Content/Datatable/Glassshow.css",
                "~/Content/Datatable/map-btn.css"
                ));


        }
    }
}
