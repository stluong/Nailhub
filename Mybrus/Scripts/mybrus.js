﻿
TNT.Language || (TNT.Language = (function ($) {
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

TNT.Cart || (TNT.Cart = (function ($) {
    return {
        Add: function (xproduct) {
            TNT.Service.GCall(TNT.Common.Settings("input#url-Cart-Add").val(), xproduct)
            .Success(function (r) {
                TNT.Common.Alert("Your item was added to cart!", { type: "alert-success", timeOut: 3000});
                //update quick cart
                $("header#topNav").find("li#quickCart").load(TNT.Common.Settings("input#url-Cart-Quick").val());
            });
        }
        , Remove: function (xproduct, thss) {
            TNT.Service.GCall(TNT.Common.Settings("input#url-Cart-Remove").val(), xproduct)
            .Success(function (r) {
                TNT.Common.Alert("Your item was removed to cart!", { type: "alert-success", timeOut: 3000 });
                //update quick cart
                $("header#topNav").find("li#quickCart").load(TNT.Common.Settings("input#url-Cart-Quick").val());
                //update cart detail
                var $cartDetail = $("body").find("div#shoppingCart");
                $cartDetail.find("div#cartContent").html("<div class='my-spinner'></div>");

                $cartDetail.load(TNT.Common.Settings("input#url-Cart-Detail").val());
            });
        }
    }
}(jQuery)));




