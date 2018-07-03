using System.Web.Optimization;

namespace AppTracker
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //Styles

            bundles.Add(new StyleBundle("~/Content/TemplateStyles").Include(
                "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                "~/Content/vendor/metisMenu/metisMenu.min.css",
                "~/Content/dist/css/sb-admin-2.css",
                "~/Content/vendor/morrisjs/morris.css",
                "~/Content/vendor/font-awesome/css/font-awesome.min.css",
                "~/Content/dist/css/select2.css",
                "~/Content/dist/css/select2-bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/OurStyles").Include(
                "~/Content/site.css",
                "~/Content/form.css"
                ));

            bundles.Add(new StyleBundle("~/Content/dataTablesCss").Include(
                "~/Content/vendor/datatables/css/dataTables.bootstrap.css"
            ));

            //Scripts

            bundles.Add(new ScriptBundle("~/bundles/TemplateScripts").Include(
              "~/Content/vendor/jquery/jquery.min.js",
              "~/Content/vendor/bootstrap/js/bootstrap.min.js",
              "~/Content/vendor/metisMenu/metisMenu.min.js",
               "~/Content/vendor/raphael/raphael.min.js",
               "~/Content/vendor/morrisjs/morris.min.js",
              "~/Content/data/morris-data.js",
              "~/Content/dist/js/sb-admin-2.js",
              "~/Content/dist/js/select2.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/OurScripts").Include(
                "~/Scripts/scripts.js",
                "~/Scripts/JavaScript.js",
                "~/Scripts/alerts.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/dataTablesJs").Include(
                "~/Content/vendor/datatables/js/jquery.dataTables.min.js",
                "~/Content/vendor/datatables-plugins/dataTables.bootstrap.min.js",
                "~/Content/vendor/datatables-responsive/dataTables.responsive.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                "~/Scripts/respond.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
                ));
        }
    }
}
