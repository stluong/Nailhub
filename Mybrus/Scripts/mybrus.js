var Mybrus = window.Mybrus || {};

Mybrus.Language || (Mybrus.Language = (function ($) {
    var $spanLanguage = $("span#spanLanguage", "body")
        , $dnmLanguage = $("button#dnmLanguage", "body")
        , $ulLanguage = $("ul#ulLanguage", "body")
    ;
    $ulLanguage.find("a").each(function (i, e) {
        $(this).bind("click", function () {
            var $img = $(this).find("img");
            if ($img.attr("alt").indexOf("VN") > -1) {
                $.cookie("mybruslang", "vi");
                $spanLanguage.text("Vietnamese");
            }
            else {
                $.cookie("mybruslang", "en")
                $spanLanguage.text("English");

            }
            $dnmLanguage
                .dropdown('toggle')
                .find("img").attr("src", $img.attr("src"))
            ;
            location.reload();
        });
    });

}(jQuery)));

Mybrus.Common || (Mybrus.Common = (function ($) {
    return {
        ImageError: function (thss) {
	        thss.onerror = null;
	        thss.src = $("body").find("input#hdfImgError").val();
        }
    }
}(jQuery)));
