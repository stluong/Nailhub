using System.Web.Optimization;

namespace Test
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

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
               "~/Content/js/vendor/jquery-migrate-1.2.1.min.js"
               , "~/Content/js/vendor/bootstrap.min.js"
               , "~/Content/js/vendor/placeholdem.min.js"
               , "~/Content/js/vendor/hoverIntent.js"
               , "~/Content/js/vendor/superfish.js"
               , "~/Content/js/vendor/jquery.actual.min.js"
               , "~/Content/js/vendor/jquery.appear.js"
               , "~/Content/js/vendor/jquerypp.custom.js"
               , "~/Content/js/vendor/jquery.elastislide.js"
               , "~/Content/js/vendor/jquery.flexslider-min.js"
               , "~/Content/js/vendor/jquery.prettyPhoto.js"
               , "~/Content/js/vendor/jquery.easing.1.3.js"
               , "~/Content/js/vendor/jquery.ui.totop.js"
               , "~/Content/js/vendor/jquery.isotope.min.js"
               , "~/Content/js/vendor/jquery.easypiechart.min.js"
               , "~/Content/js/vendor/jflickrfeed.min.js"
               , "~/Content/js/vendor/jquery.sticky.js"
               , "~/Content/js/vendor/owl.carousel.min.js"
               , "~/Content/js/vendor/jquery.nicescroll.min.js"
               , "~/Content/js/vendor/jquery.fractionslider.min.js"
               , "~/Content/js/vendor/jquery.scrollTo-min.js"
               , "~/Content/js/vendor/jquery.localscroll-min.js"
               , "~/Content/js/vendor/jquery.parallax-1.1.3.js"
               , "~/Content/js/vendor/jquery.bxslider.min.js"
               , "~/Content/js/vendor/jquery.funnyText.min.js"
               , "~/Content/js/vendor/jquery.countTo.js"
               , "~/Content/js/vendor/grid.js"
               , "~/Content/twitter/jquery.tweet.min.js"
               , "~/Content/js/plugins.js"
               , "~/Content/js/main.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.min.css"
                , "~/Content/css/animations.css"
                , "~/Content/css/main.css"
            ));
        }
    }
}
