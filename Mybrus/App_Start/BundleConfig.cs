using System.Web;
using System.Web.Optimization;

namespace Mybrus
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                "~/Scripts/main.js"
                , "~/Scripts/jquery.magnific-popup.js"
                , "~/Scripts/owl.carousel.js"
                , "~/Content/plugins/superslides/dist/jquery.superslides.js"
                , "~/Content/plugins/styleswitcher/styleswitcher.js"
                , "~/Content/plugins/revolution-slider/js/jquery.themepunch.tools.min.js"
                , "~/Content/plugins/revolution-slider/js/jquery.themepunch.revolution.js"
                , "~/Content/plugins/revolution-slider/js/slider_revolution.js"
            ));

            //Css bundles

            bundles.Add(new StyleBundle("~/Content/css_core").Include(
                      "~/Content/bootstrap.css"
                      , "~/Content/font-awesome.css"
                      , "~/Content/OwlCarousel/owl.carousel.css"
                      , "~/Content/OwlCarousel/owl.theme.css"
                      , "~/Content/OwlCarousel/owl.transitions.css"
                      , "~/Content/magnific-popup.css"
                      , "~/Content/animate.css"
                      , "~/Content/superslides.css"
                      , "~/Content/plugins/styleswitcher/styleswitcher.css"
            ));
            bundles.Add(new StyleBundle("~/Content/css_slider").Include(
                      "~/Content/plugins/revolution-slider/css/settings.css"
            ));
            bundles.Add(new StyleBundle("~/Content/css_theme").Include(
                      "~/Content/essentials.css"
                      , "~/Content/layout.css"
                      , "~/Content/layout-responsive.css"
                      , "~/Content/shop.css"
            ));
            bundles.Add(new StyleBundle("~/Content/css_themestyle").Include(
                      "~/Content/color_scheme/orange.css"
                      , "~/Content/color_scheme/orange.css"
                      , "~/Content/color_scheme/red.css"
                      , "~/Content/color_scheme/pink.css"
                      , "~/Content/color_scheme/yellow.css"
                      , "~/Content/color_scheme/darkgreen.css"
                      , "~/Content/color_scheme/darkblue.css"
                      , "~/Content/color_scheme/blue.css"
                      , "~/Content/color_scheme/brown.css"
                      , "~/Content/color_scheme/lightgrey.css"
            ));
        }
    }
}
