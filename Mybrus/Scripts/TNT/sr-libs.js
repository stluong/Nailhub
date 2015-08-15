/*
 * Who, When: What
 * Steven Luong, 12/31/2014: All libraries
 */

var TNT = window.TNT || {};

TNT.Common || (TNT.Common = function ($) {
    return {
        Settings: function (findingSelector) {
            return $("body >header#mySettings").find(findingSelector);
        }
        , ImageError: function (thss) {
            thss.onerror = null;
            thss.src = TNT.Common.Settings("input#img-Error").val();
        }
        , FormatMoney: function (elm, currencyType) {
            return currencyType + parseInt(elm).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
        }
		, AlertTo: function (/*Prepend alert to this selector*/$selector, alert,
			/*
				{
					type: "alert-success||alert-info||alert-warning||alert-danger"
					, href: "link"   
                    , timeOut: 5000
				}
			 */
			options) {
		    //seting default setting
		    var _settings = {
		        type: "msgSuccess"
				, href: ""
                , timeOut: 5000
		    }
		    ;
		    //merge with setting parameter
		    _settings = $.extend(true, _settings, options);

		    var _myAlert = ""
				, _waitTime = _settings.timeOut
		    ;
		    if (_settings.href !== "") {
		        _myAlert = "{0}. <a href='{1}' class='clwhite textunderline'>Click for detail</a>".format(alert, _settings.href);
		        _waitTime = 2000 * 60;
		    }
		    else {
		        _myAlert = alert;
		    }

		    $("<div class='alert {0} alert-dismissable text-center'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button><strong>Notice!</strong> {1}.</div>".format(_settings.type, _myAlert))
				.appendTo($selector).delay(_waitTime)
				.fadeOut(400, function () {
				    $(this).remove();
				})
		    ;
		}
        , Alert: function (alert,
			/*
				{
					type: "alert-success||alert-info||alert-warning||alert-danger"
					, href: "link"                    
				}
			 */
			options) {
            TNT.Common.AlertTo($("body>header#topHead"), alert, options);
        }
        //Open dialog
        , Dialog: function ($selector, urlAction, para, newTitle, width, positionTop, isAntiScroll) {
            var $dialog = $("<div class='dialogWrapper dpl-none box-size-content'><div class='dialog-content'><div class='myspinner'></div></div></div>");
            //Check if dialog is not existing
            if ($.type($selector) === InsiiteEnum.String) {
                $selector = $($selector);
                if ($selector.find("div.dialogWrapper").length < 1) {
                    $selector.prepend($dialog);
                }
            }
            else {
                $dialog = $selector;
            }

            var option = {
                autoOpen: false,
                resizable: false,
                title: newTitle ? newTitle : $dialog.attr("title"),
                height: "auto",
                width: width ? width : $(window).width() - 100 < 950 ? $(window).width() - 100 : 950,
                position: ['top', positionTop ? positionTop : 0],
                modal: true,
                create: function (event, ui) {
                    //here we can apply unique styling  
                    //$dialog.parents(".ui-dialog:first").find(".ui-dialog-titlebar").addClass("lightboxtitlebar");
                    //$dialog.parents(".ui-dialog:first").find(".ui-dialog-titlebar-close").addClass("lightboxclose");
                    //$dialog.parents(".ui-dialog:first").find(".ui-icon-closethick").addClass("lightboxclosespan");
                    //$dialog.parents(".ui-dialog:first").find(".ui-dialog-buttonpane").addClass("lightboxButtonPane");
                    //$dialog.parents(".ui-dialog:first").find(".ui-dialog-buttonset").addClass("lightboxbutton");
                    ///*
                    //$(this).parents(".ui-dialog:first").find(".ui-button:first").addClass("lightboxblue");
                    //$(this).parents(".ui-dialog:first").find(".ui-button:last").addClass("lightboxgray");
                    //*/
                    //$dialog.parents(".ui-dialog:first").find(".ui-button-text").hide();
                    //$dialog.parents(".ui-dialog").addClass("lightbox");
                },
                close: function (event, ui) {
                    $dialog.dialog("destroy");
                },
                open: function () {
                    //$(this).dialog("option", "position", "center");
                },
                buttons: {
                }
            };

            //Setup dialog
            $dialog.dialog(option);
            try {
                $dialog.dialog('open');
                _LoadContent();
            }
            catch (ex) {

            }

            function _LoadContent() {
                var $dialogContent = $dialog.find('div.dialog-content');

                if ($.type(urlAction) != "undefined" && urlAction != null && urlAction != '') {
                    //load content from urlaction
                    $dialogContent.css("display", "block");
                    //$dialogContent.html("");
                    $dialogContent.load(
				        urlAction
				        , para ? para : ""
				        , function (response, status, xhr) {
				            //$(this).removeClass("bgspinner");
				            if ($.type(isAntiScroll) != "undefined") {
				                //$dialog.find("div.antiscroll-wrap").antiscroll({ width: $dialog.width(), height: "500px" });
				                //$dialog.find("table.fixedHeader").m_ScrollTable();
				            }
				        }
			        );
                }
                else {
                    if ($.type(isAntiScroll) != "undefined") {
                        //$dialog.find("div.antiscroll-wrap").antiscroll({ width: $dialog.width(), height: "500px" });
                        //$dialog.find("table.fixedHeader").m_ScrollTable();
                    }
                }
            }

        }
        //Initiate data for dropdownlist
        , SetDropdownlist: function ($select, ddlData, selectedIdText) {
            if (ddlData.length > 0) {
                $select = $($select);
                $select.find('option').remove();
                $.each(ddlData, function (i, item) {
                    var option = $('<option>', {
                        value: item.Value
                    }).html(item.Text).appendTo($select);
                });
                _DdlSetSelected($select, selectedIdText);
            };

            function _DdlSetSelected($ddlSelector, selectedIdText) {
                selectedIdText = $.trim(selectedIdText);
                if (selectedIdText) {
                    if ($.isNumeric(selectedIdText)) {
                        $ddlSelector.val(selectedIdText);
                    }
                    else {
                        $ddlSelector.find("option")
                                .filter(function () { return $.trim($(this).text()) == selectedIdText; })
                                .attr('selected', true);
                    }
                }
            }

        }

    }
}(jQuery));

TNT.Service || (TNT.Service = function ($) {
    var _authorization = "Basic {0}"
        , _cridentailKey = "DDAYLAMAXCUARTUI"
    ;
    var _headers = {
        "Authorization": _authorization.format(_token())
    };
    function _token(/*x=>tokenValue*/value) {
        if (value) {
            var tokenValue = value.split("=>")[1];
            //Update headers
            _headers.Authorization = _authorization.format(tokenValue);
            localStorage.setItem(_cridentailKey, tokenValue);
        }
        return localStorage.getItem(_cridentailKey);
    }
    return {
        Message: {
            friendly: "An error occured, please try later!"
			, error: "Server returned failure."
			, failure: "Cannot call the service"
        }
        //Default is basic.
        , Headers: function (/*header authorization for TNT.Service*/headers) {
            return _headers || (_headers = headers);
        }
        //Set, get, token value
		, Token: function (/*x=>tokenValue*/tokenValue) {

		    return _token(tokenValue);
		}
        //Remove stored token
		, TokenClean: function () {
		    localStorage.removeItem(_cridentailKey);
		}
        //Server reponses not FAILURE
		, Success: function (serverResponse) {
		    if ($.trim(serverResponse).indexOf("FAILURE") < 0) {
		        return true;
		    }
		    return false;
		}
        //Get exception message from server response failure if existing
		, Failure: function (/*FAILURE => exception*/serverResponse) {
		    serverResponse = $.trim(serverResponse);
		    if (serverResponse.indexOf("FAILURE") > -1 && serverResponse.indexOf("=>") > -1) {
		        var exMsg = serverResponse.split("=>");
		        if (exMsg.length > 1) {
		            return exMsg[1].trim();
		        }
		    }
		    return "";
		}
        //consolidate $.ajax service call generic. Just return back Done, Success. Failure, and Complete are all taking care!
		, Call: function (options) {
		    var _serviceReponsed = $.ajax(options);
		    _serviceReponsed
				.error(function () {
				    TNT.Common.Alert(TNT.Service.Message.friendly, { type: "alert-warning" });
				})
				.complete(function () {
				    //TNT.Common.Alert(TNT.Service.Message.friendly, { type: "alert-warning" });
				})
		    ;
		    return {
		        Done: function (callback) {
		            _serviceReponsed.success(function (serverResponse) {
		                if ($.trim(serverResponse) === "error") {
		                    TNT.Common.Alert(TNT.Service.Message.friendly, { type: "alert-warning" });
		                }
		                else {
		                    callback(serverResponse);
		                } 
		            });
		        }
				, Success: function (callback, errorCallback) {
				    _serviceReponsed.success(function (serverResponse) {
				        if (TNT.Service.Success(serverResponse)) {
				            if ($.trim(serverResponse) === "error") {
				                TNT.Common.Alert(TNT.Service.Message.friendly, { type: "alert-warning" });
				                errorCallback(serverResponse);
				            }
				            else {
				                callback(serverResponse);
				            }
				        }
				        else {
				            //Server responsed failure with exception message
				            TNT.Common.Alert(TNT.Service.Message.failure, { type: "alert-warning" });
				        }
				    });
				}
		    }
		}
        //Get service
		, GCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
		    return TNT.Service.Call({
		        url: url
				, type: "get"
				, data: paras || ""
		    });
		}
        //Post service
		, PCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
		    return TNT.Service.Call({
		        url: url
				, type: "post"
				, data: paras || ""
		    });
		}
        //Authorized get service
		, AGCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
		    return TNT.Service.Call({
		        url: url
				, type: "get"
				, headers: _headers
				, data: paras || ""
		    });
		}
        //Authorized post service
		, APCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
		    return TNT.Service.Call({
		        url: url
				, type: "post"
				, headers: _headers
				, data: paras || ""
		    });
		}
        //Upload files
        , FileUpload: function (/*Action url*/url, /*parameters: form data, json, or serialized string*/paras) {
            return TNT.Service.Call({
                type: "post",
                url: url,
                contentType: false,
                processData: false,
                data: paras
            });
        }
        //Get url paras on mobile navigation
        , GetPassedParas: function () {
            var paras = []
                , hash
            ;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                paras.push(hash[0]);
                paras[hash[0]] = hash[1];
            }
            return paras;
        }

    }
}(jQuery));

TNT.Stripe || (TNT.Stripe = function ($) {
    var _myStripe
        , _apiKey = "pk_test_wwYvgcyD8v1I6Y1FXprw0WLA"
        , _defaultEmail = "luc.huynh78@gmail.com"
        //, _myContent = "<div class='form-group'>"
        //_myContent += "<input id='chkBopCo' type='checkbox' value='Bop Co' /> Bop co"
        //_myContent += "</div>"

        //_myContent += "<div class='form-group'>"
        //_myContent += "<input id='txtEmail' type='text' placeholder='Email' class='form-control input-large'/>"
        //_myContent += "<p class='help-block'>Enter if you have!</p>"
        //_myContent += "</div>"

        //_myContent += "<div class='form-group'>"
        //_myContent += "<label class='control-label checkbox'>Shipping address</label>"
        //_myContent += "<textarea id='txtShipping' placeholder='Tell me where to ship?' class='form-control input-large'/>"
        //_myContent += "</div>"
    ;

    return {
        //Show stripe checkout form, then call server side action to perform charging
        Checkout: function (object, thss) {
            var $scope = $(thss).parents($("div#scopeBuy"))
                , quantity = $scope.find("input#product_qty").val() || 1
                , size = $scope.find("select#ddlSize").val()
            ;

            object.quantity = quantity;
            object.size = size;

            var total = object.quantity * object.price
                , shpFee = "Free";
            ;

            if (total < 30) {
                total = total + 3.99;
                shpFee = "$3.99";
            }
            BootstrapDialog.show({
                title: "Notice",
                message: $("<div></div>").load(TNT.Common.Settings("input#url-Order-Info").val(), {fee: shpFee}),
                buttons: [{
                    label: "READY TO PAY",
                    action: function (dialog) {
                        var
                            isBopCo = dialog.$modalBody.find("input#chkBopCo").is(":checked")
                            , email = dialog.$modalBody.find("input#txtEmail").val()
                            , $shipTo = dialog.$modalBody.find("textarea#txtShipping")
                        ;
                        if ($.trim($shipTo.val()).length > 0) {
                            dialog.close();

                            object.description = isBopCo ? "Bop co" : "";
                            object.note = $shipTo.val();

                            if (email) {
                                _myStripe = StripeCheckout.configure({
                                    key: _apiKey,
                                    image: TNT.Common.Settings("input#img-Mybrus").val(),
                                    email: email,
                                    token: function (token) {
                                        // Use the token to create the charge with a server-side script.
                                        // You can access the token ID with token.id, token.email and token.card

                                        //var myParas = "stripeToken={0}&stripeEmail={1}&{2}".format(token.id, token.email, $.param(object));
                                        //Or use json object parameters
                                        var myParas = {
                                            stripeToken: token.id
                                            , stripeEmail: token.email
                                            , prod: object
                                        }
                                        TNT.Service.PCall(TNT.Common.Settings("input#url-Charge").val(), myParas)
                                            .Success(function (d) {
                                                TNT.Common.Alert("Your item will be shipped asap!", { type: "alert-success" });
                                            });
                                    }
                                });
                            }
                            else {
                                _myStripe = StripeCheckout.configure({
                                    key: _apiKey,
                                    image: TNT.Common.Settings("input#img-Mybrus").val(),
                                    email: _defaultEmail,
                                    token: function (token) {
                                        // Use the token to create the charge with a server-side script.
                                        // You can access the token ID with token.id, token.email and token.card
                                        //var myParas = "stripeToken={0}&stripeEmail={1}&{2}".format(token.id, token.email, $.param(object));
                                        //Or use json object parameters
                                        var myParas = {
                                            stripeToken: token.id
                                            , stripeEmail: token.email
                                            , prod: object
                                        }
                                        TNT.Service.PCall(TNT.Common.Settings("input#url-Charge").val(), myParas)
                                            .Success(function (d) {
                                                TNT.Common.Alert("Your item will be shipped asap!", { type: "alert-success" });
                                            });
                                    }
                                });
                            }

                            _myStripe.open({
                                name: "MyBrus",
                                description: object.description,
                                amount: total * 100
                            });

                            $(window).on("popstate", function () {
                                _myStripe.close();
                            });
                        }
                        else {
                            $shipTo.tooltip("show");
                        }
                        
                        
                    }
                }, {
                    label: "CANCEL",
                    action: function (dialog) {
                        dialog.close();
                    }
                }]
            });

        }
        //Checkout multiple items
        , Checkouts: function (objects, scope) {
            var $scope = $(scope);
            var total = $scope.attr("attr-total")
                , shpFee = "Free";
            ;

            if (total < 30) {
                total = total + 3.99;
                shpFee = "$3.99";
            }
            BootstrapDialog.show({
                title: "Notice",
                message: $("<div></div>").load(TNT.Common.Settings("input#url-Order-Info").val()), //_myContent,
                buttons: [{
                    label: "READY TO PAY",
                    action: function (dialog) {
                        var
                            bopCo = dialog.$modalBody.find("input#chkBopCo").is(":checked") ? "Bop co" : ""
                            , email = dialog.$modalBody.find("input#txtEmail").val()
                            , $shipTo = dialog.$modalBody.find("textarea#txtShipping")
                        ;

                        if ($.trim($shipTo.val()).length > 0) {
                            dialog.close();
                            //Update objects from ui
                            $scope.find("input[name='qty']").each(function (i, e) {
                                objects[i].quantity = $(e).val();
                                objects[i].description = bopCo;
                                objects[i].note = $shipTo.val();
                            });

                            if (email) {
                                _myStripe = StripeCheckout.configure({
                                    key: _apiKey,
                                    image: TNT.Common.Settings("input#img-Mybrus").val(),
                                    email: email,
                                    token: function (token) {
                                        // Use the token to create the charge with a server-side script.
                                        // You can access the token ID with token.id, token.email and token.card

                                        //var myParas = "stripeToken={0}&stripeEmail={1}&{2}".format(token.id, token.email, $.param(object));
                                        //Or passing json object
                                        var myParas = {
                                            stripeToken: token.id
                                            , stripeEmail: token.email
                                            , prods: objects
                                        }
                                        TNT.Service.PCall(TNT.Common.Settings("input#url-Charges").val(), myParas)
                                            .Success(function (d) {
                                                TNT.Common.Alert("Your item will be shipped asap!", { type: "alert-success" });
                                            });
                                    }
                                });
                            }
                            else {
                                _myStripe = StripeCheckout.configure({
                                    key: _apiKey,
                                    image: TNT.Common.Settings("input#img-Mybrus").val(),
                                    email: _defaultEmail,
                                    token: function (token) {
                                        // Use the token to create the charge with a server-side script.
                                        // You can access the token ID with token.id, token.email and token.card
                                        //var myParas = "stripeToken={0}&stripeEmail={1}&{2}".format(token.id, token.email, $.param(object));
                                        var myParas = {
                                            stripeToken: token.id
                                            , stripeEmail: token.email
                                            , prods: objects
                                        }
                                        TNT.Service.PCall(TNT.Common.Settings("input#url-Charges").val(), myParas)
                                            .Success(function (d) {
                                                TNT.Common.Alert("Your item will be shipped asap!", { type: "alert-success" });
                                            });
                                    }
                                });
                            }

                            _myStripe.open({
                                name: "MyBrus",
                                description: "Shopping cart checkout",
                                amount: total * 100
                            });

                            $(window).on("popstate", function () {
                                _myStripe.close();
                            });
                        }
                        else {
                            $shipTo.tooltip("show");
                        }
                        
                    }
                }, {
                    label: "CANCEL",
                    action: function (dialog) {
                        dialog.close();
                    }
                }]
            });
        }

    }
}(jQuery));
