using System.Web;
using System.Web.Optimization;

namespace Proyectotransmilenio
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/template/css").Include(
                      "~/template/vendor/bootstrap/css/bootstrap.min.css",
                      "~/template/vendor/mentisMenu/metisMenu.min.css",
                      "~/template/vendor/datatables-plugins/dataTables.bootstrap.css",
                      "~/template/vendor/datatables-responsive/dataTables.responsive.css",
                      "~/template/dist/css/sb-admin-2.css",
                      "~/template/vendor/morrisjs/morris.css",
                      "~/template/vendor/font-awesome/css/font-awesome.min.css",
                       "~//Content/uploadify.css"));

            bundles.Add(new ScriptBundle("~/template/bootstrap").Include(
                      "~/template/vendor/jquery/jquery.min.js",
                      "~/template/vendor/bootstrap/js/bootstrap.min.js",
                      "~/template/vendor/mentisMenu/metisMenu.min.js",
                      "~/template/vendor/raphael/raphael.min.js",                                         
                      "~/template/vendor/datatables/js/jquery.dataTables.min.js",
                      "~/template/vendor/datatables-plugins/dataTables.bootstrap.min.js",
                      "~/template/vendor/datatables-responsive/dataTables.responsive.js",
                      "~/template/dist/js/sb-admin-2.js",
                      "~/Scripts/jquery.uploadify.v2.1.4.min.js",
                      "~/Scripts/swfobject.js"));
        }
    }
}
