using System.Web.Optimization;

namespace JC_BookStation
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                            .Include("~/Scripts/jquery-{version}.js")
                            .Include("~/Scripts/modernizr-{version}.js")
                            .Include("~/Scripts/json2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                            .Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                            .Include("~/Scripts/jquery.unobtrusive*")
                            .Include("~/Scripts/jquery.validate*")
                            .Include("~/Scripts/MicrosoftAjax.js*")
                            .Include("~/Scripts/MicrosoftMvc*")
                            .Include("~/Scripts/methods_pt.js")
                            .Include("~/Scripts/jquery.maskMoney.js")
                            .Include("~/Scripts/jquery.maskedinput-{version}.js")
                            .Include("~/MyJS/setfocusenter.js")
                            .Include("~/MyJS/example.js")
                            .Include("~/MyJS/CustomValidation.js"));
                            //.Include("~/Scripts/jquery.mask.min.js")

            bundles.Add(new ScriptBundle("~/bundles/knockout")
                            .Include("~/Scripts/knockout-{version}.js")
                            .Include("~/Scripts/knockout-{version}.debug.js")
                            .Include("~/MyJS/formatar.js"));
                            //.Include("~/Scripts/knockout.validation.js")
                            //.Include("~/Scripts/knockout.validation.debug.js")
                            //.Include("~/Scripts/knockout.mapping-latest.debug.js")
                            //.Include("~/Scripts/knockout.viewmodel.{version}.js")
                            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                            .Include("~/Scripts/bootstrap.min.js")
                            .Include("~/Scripts/bootbox.min.js")
                            //.Include("~/Scripts/bootstrap-datepicker.js")
                            //.Include("~/Scripts/locales/bootstrap-datepicker.pt-BR.js")
                            //.Include("~/Scripts/typeahead.jquery.js")
                            .Include("~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/website")
                            .Include("~/Content/bootstrap.css")
                            .Include("~/Content/blog.css")
                            .Include("~/Content/carrousel.css")
                            .Include("~/Content/justified-nav.css")
                            .Include("~/Content/StyleSheet.css"));

            bundles.Add(new StyleBundle("~/Content/css")
                            .Include("~/Content/bootstrap.min.css")
                            .Include("~/Content/bootstrap_cerulean.min.css")
                            //.Include("~/Content/bootstrap_spacelab.min.css")
                            .Include("~/Content/jasny-bootstrap.min.css")
                            //.Include("~/Content/bootstrap-datepicker.css")
                            //.Include("~/Content/bootstrap-datetimepicker.min.css")
                            .Include("~/Content/PagedList.css")
                            .Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                            .Include("~/Content/themes/base/jquery-ui-1.10.4.custom.css"));
                            //.Include("~/Content/themes/base/jquery.ui.core.css")
                            //.Include("~/Content/themes/base/jquery.ui.resizable.css")
                            //.Include("~/Content/themes/base/jquery.ui.selectable.css")
                            //.Include("~/Content/themes/base/jquery.ui.accordion.css")
                            //.Include("~/Content/themes/base/jquery.ui.autocomplete.css")
                            //.Include("~/Content/themes/base/jquery.ui.button.css")
                            //.Include("~/Content/themes/base/jquery.ui.dialog.css")
                            //.Include("~/Content/themes/base/jquery.ui.slider.css")
                            //.Include("~/Content/themes/base/jquery.ui.tabs.css")
                            //.Include("~/Content/themes/base/jquery.ui.datepicker.css")
                            //.Include("~/Content/themes/base/jquery.ui.progressbar.css")
                            //.Include("~/Content/themes/base/jquery.ui.theme.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }

}
