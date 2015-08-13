
TNT.Language || (TNT.Language = (function ($) {
    var $spanLanguage = $("span#spanLanguage", "body")
        , $dnmLanguage = $("button#dnmLanguage", "body")
        , $ulLanguage = $("ul#ulLanguage", "body")
    ;
    $ulLanguage.find("a").each(function (i, e) {
        $(this).bind("click", function () {
            debugger;
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

    function _refreshQuickCart() {
        //update quick cart
        $("header#topNav").find("li#quickCart").load(TNT.Common.Settings("input#url-Cart-Quick").val());
    }
    function _refreshDetailCart() {
        //update cart detail
        var $cartDetail = $("body").find("div#shoppingCart");
        $cartDetail.find("div#cartContent").html("<div class='my-spinner'></div>");

        $cartDetail.load(TNT.Common.Settings("input#url-Cart-Detail").val());
    }

    return {
        Add: function (xproduct) {
            xproduct.size = $("select#ddlSize").val();
            xproduct.quantity = $("input#product_qty").val();
            TNT.Service.GCall(TNT.Common.Settings("input#url-Cart-Add").val(), xproduct)
            .Success(function (r) {
                TNT.Common.Alert("Your item was added to cart!", { type: "alert-success", timeOut: 3000});
                _refreshQuickCart();
            });
        }
        , Remove: function (xproduct) {
            TNT.Service.GCall(TNT.Common.Settings("input#url-Cart-Remove").val(), xproduct)
            .Success(function (r) {
                TNT.Common.Alert("Your item was removed to cart!", { type: "alert-success", timeOut: 3000 });
                _refreshQuickCart();
                _refreshDetailCart();
            });
        }
        , Update: function (xproducts) {
            $("div#cartContent").find("input[name='qty']").each(function (i, e) {
                xproducts[i].quantity = $(e).val();
            });

            TNT.Service.PCall(TNT.Common.Settings("input#url-Cart-Update").val(), { prods: xproducts })
            .Success(function (r) {
                TNT.Common.Alert("Your shopping cart was updated!", { type: "alert-success", timeOut: 3000 });
                _refreshQuickCart();
                _refreshDetailCart();
            });
        }
    }
}(jQuery)));

TNT.Product || (TNT.Product = function ($) {
    var $scope = $("table#tblSaleReport", "body")
        , actUrl = TNT.Common.Settings("input#url-Prod-UpdateTracking").val()
    ;

    return {
        UpdateTracking: function (thss) {
            var $thss = $(thss)
                , $txtTrackingNo = $thss.prevUntil("div")
            ;
            if ($.trim($txtTrackingNo.val()).length > 0) {
                var $td = $thss.closest("td")
                , $preTd = $td.prev("td.tdStatus")
                , para = {
                    orderId: $txtTrackingNo.attr("attr-id")
                    , trackingNo: $txtTrackingNo.val()
                }
                ;

                TNT.Service.PCall(TNT.Common.Settings("input#url-Prod-UpdateTracking").val(), para)
                    .Success(function (data) {
                        $td
                            .html($txtTrackingNo.val())
                        ;
                        $preTd.html("Shipped");

                    })
                ;
            }
            else {
                $txtTrackingNo
                    .addClass("txt-required")
                    .focus()
                ;
            }
            
        }
        , Crud: function (xprod, thss) {
            if ($.isNumeric(thss)) {
                //delete
                BootstrapDialog.confirm("Are you sure to delete it?", function (ys) {
                    if (ys) {
                        xprod.code = "XOAXOAXOA";
                        TNT.Service.PCall(TNT.Common.Settings("input#url-Prod-Crud").val(), xprod)
                            .Success(function (r) {
                                TNT.Common.Alert("Your product was removed!", { type: "alert-success", timeOut: 3000 });
                                location.reload(true);
                            })
                        ;
                    }                   
                })
            }
            else {
                var $scopeEdit = $(xprod.productid > 0 ? "div#prodEdit" : "div#prodAdd", "body")
                    , $thss = $(thss)
                    , $spinner = $thss.next()
                    , urlUpload = TNT.Common.Settings("input#url-Prod-Upload").val()
                    , url = TNT.Common.Settings("input#url-Prod-Crud").val()
                ;
                xprod.code = $scopeEdit.find("input#txtCode").val();
                xprod.brandid = $scopeEdit.find("select#ddlBrand").val();
                xprod.name = $scopeEdit.find("input#txtName").val();
                xprod.description = $scopeEdit.find("textarea#txtDesc").val();
                xprod.price = $scopeEdit.find("input#txtPrice").val();
                var sizes = [];
                $scopeEdit.find("input[name='chkSize']:checked").each(function (i, e) {
                    sizes.push($(e).val());
                });
                xprod.Sizes = sizes;

                var $file = $scopeEdit.find("#fileImage")
                    , files = $file.get(0).files
                    , frmData = new FormData();
                ;
                if (files.length > 0) {
                    var file = files[0];
                    xprod.image = file.name;
                    frmData.append("uploadingImage", file);
                }
                else {
                    xprod.image = "";
                }

                $thss.hide();
                $spinner.removeClass("hide");

                TNT.Service.FileUpload(urlUpload, frmData)
                    .Success(function (r) {
                        TNT.Service.PCall(url, xprod)
                            .Success(function (r) {
                                $thss.show();
                                $spinner.addClass("hide");
                                TNT.Common.Alert("Your product was updated!", { type: "alert-success", timeOut: 3000 });
                                if (xprod.image != "") {
                                    if (xprod.productid > 0) {
                                        location.reload(true);
                                    }
                                    else {
                                        window.location.replace(TNT.Common.Settings("input#url-Prod-Index").val());
                                    }
                                }
                            })
                        ;
                    })
                ;
            }
        }
        , DeleteImage: function (thss) {
            var $thss = $(thss)
                , prdId = $thss.attr("attr-pid")
                , img = $thss.attr("attr-img")
                , para = {
                    prdid: prdId
                    , img: img
                }
            ;

            TNT.Service.GCall(TNT.Common.Settings("input#url-Prod-DeleteImage").val(), para)
                .Success(function (r) {
                    TNT.Common.Alert("Image was deleted!", { type: "alert-success", timeOut: 3000 });
                    $thss.parent().remove();
                })
            ;
        }
        , SetImage: function (thss) {
            var $thss = $(thss)
                , prdId = $thss.attr("attr-pid")
                , img = $thss.attr("attr-img")
                , para = {
                    prdid: prdId
                    , img: img
                }
            ;
            TNT.Service.GCall(TNT.Common.Settings("input#url-Prod-SetImage").val(), para)
                .Success(function (r) {
                    TNT.Common.Alert("Image was default!", { type: "alert-success", timeOut: 3000 });
                })
            ;
        }
    }

}(jQuery));



