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
                "~/Content/plugins/jquery.easing.1.3.js"
                , "~/Content/plugins/jquery.cookie.js"
                , "~/Content/plugins/jquery.appear.js"
                , "~/Content/plugins/jquery.isotope.js"
                , "~/Content/plugins/masonry.js"
                , "~/Content/plugins/magnific-popup/jquery.magnific-popup.min.js"
                , "~/Content/plugins/owl-carousel/owl.carousel.min.js"
                , "~/Content/plugins/stellar/jquery.stellar.min.js"
                , "~/Content/plugins/knob/js/jquery.knob.js"
                , "~/Content/plugins/jquery.backstretch.min.js"
                , "~/Content/plugins/superslides/dist/jquery.superslides.min.js"
                , "~/Content/plugins/mediaelement/build/mediaelement-and-player.min.js"
                , "~/Content/plugins/styleswitcher/styleswitcher.js"
                , "~/Content/plugins/revolution-slider/js/jquery.themepunch.tools.min.js"
                , "~/Content/plugins/revolution-slider/js/jquery.themepunch.revolution.min.js"
                , "~/Content/js/slider_revolution.js"
                , "~/Content/plugins/cookie.js"
                , "~/Content/plugins/jquery-confirm/jquery-confirm.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                "~/Content/js/scripts.js"
                , "~/Scripts/TNT/extensions.js"
                , "~/Scripts/TNT/libs.js"
                , "~/Scripts/mybrus.js" 
            ));
            //Css bundles
            bundles.Add(new StyleBundle("~/Content/css_core").Include(
                      "~/Content/plugins/bootstrap/css/bootstrap.min.css"
                      , "~/Content/css/font-awesome.css"
                      , "~/Content/plugins/owl-carousel/owl.carousel.css"
                      , "~/Content/plugins/owl-carousel/owl.theme.css"
                      , "~/Content/plugins/owl-carousel/owl.transitions.css"
                      , "~/Content/plugins/magnific-popup/magnific-popup.css"
                      , "~/Content/css/animate.css"
                      , "~/Content/css/superslides.css"
                      , "~/Content/plugins/styleswitcher/styleswitcher.css"
                      , "~/Content/plugins/jquery-confirm/jquery-confirm.css"
            ));
            bundles.Add(new StyleBundle("~/Content/css_slider").Include(
                      "~/Content/plugins/revolution-slider/css/settings.css"
            ));
            bundles.Add(new StyleBundle("~/Content/css_theme").Include(
                      "~/Content/css/essentials.css"
                      , "~/Content/css/layout.css"
                      , "~/Content/css/layout-responsive.css"
                      , "~/Content/css/shop.css"
                      , "~/Content/css/color_scheme/orange.css"
            ));
            bundles.Add(new StyleBundle("~/Content/css_themestyle").Include(
                      "~/Content/css/color_scheme/orange.css"
                      , "~/Content/css/color_scheme/orange.css"
                      , "~/Content/css/color_scheme/red.css"
                      , "~/Content/css/color_scheme/pink.css"
                      , "~/Content/css/color_scheme/yellow.css"
                      , "~/Content/css/color_scheme/green.css"
                      , "~/Content/css/color_scheme/darkgreen.css"
                      , "~/Content/css/color_scheme/darkblue.css"
                      , "~/Content/css/color_scheme/blue.css"
                      , "~/Content/css/color_scheme/brown.css"
                      , "~/Content/css/color_scheme/lightgrey.css"
            ));
        }
    }
}
