using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
namespace IdealOnlineBillingNew.App_Start
{
    public class BundleConfig
    {
        //Use ASP.net.web.Optimization Newgut package  

        public static void RegisterBundle(BundleCollection bundles)
        {

            // bundles.Add(new ScriptBundle("~/bundles/js").IncludeDirectory("~/Assets","*.js",true));



            //This is Bundel Name which you can create any path here
            var bundle = new ScriptBundle("~/bundles/js");
            bundle.Include(
                "~/Assets/plugins/jquery/jquery.min.js",
                "~/Assets/plugins/bootstrap/js/bootstrap.js",
                "~/Assets/plugins/bootstrap-select/js/bootstrap-select.js",
                "~/Assets/plugins/jquery-slimscroll/jquery.slimscroll.js",
                "~/Assets/plugins/node-waves/waves.js",
                "~/Assets/js/admin.js",
                "~/Assets/js/select2.min.js",


                "~/Assets/js/demo.js",
                "~/Assets/plugins/jquery-datatable/jquery.dataTables.js",
                "~/Assets/plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js",
                "~/Assets/plugins/jquery-datatable/extensions/export/dataTables.buttons.min.js",
                "~/Assets/plugins/jquery-datatable/extensions/export/buttons.flash.min.js",
                "~/Assets/plugins/jquery-datatable/extensions/export/jszip.min.js",
                "~/Assets/plugins/jquery-datatable/extensions/export/pdfmake.min.js",
                "~/Assets/plugins/jquery-datatable/extensions/export/vfs_fonts.js",
                "~/Assets/plugins/jquery-datatable/extensions/export/buttons.html5.min.js",
                "~/Assets/plugins/jquery-datatable/extensions/export/buttons.print.min.js"
                );
            bundles.Add(bundle);

            bundles.Add(new ScriptBundle("~/Application/js").IncludeDirectory("~/Scripts", "*.js",true));
            bundles.Add(new ScriptBundle("~/Application/js").IncludeDirectory("~/ApplicationJS", "*.js",true));

            //bundles.Add(new StyleBundle("~/bundle/css").IncludeDirectory("~/Assets","*.css",true));

            //var bundleApplication = new ScriptBundle("~/Application/js");
            //bundleApplication.Include(
            //    // "~/Scripts/jquery-3.5.1.js",
            //    //"~/Scripts/jquery.dataTables.min.js",
            //   "~/ApplicationJS/ProductCategory.js"
            //   );

            //bundles.Add(bundleApplication);
            BundleTable.EnableOptimizations = true;
        }
    }
}