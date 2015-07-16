/*
 * Who, When: What
 * Steven Luong, 12/31/2014: All libraries
 */

var TNT = window.TNT || {};

TNT.Common || (TNT.Common = (function ($) {
    return {
        $Settings: function (findingSelector) {
            return $("body >header#mySettings").find(findingSelector);
        }
        , ImageError: function (thss) {
            thss.onerror = null;
            thss.src = TNT.Common.$Settings("input#img-Error").val();
        }
        , FormatMoney: function (elm, currencyType) {
            return currencyType + parseInt(elm).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
        }
		, AlertTo: function (/*Prepend alert to this selector*/$selector, alert,
			/*
				{
					type: "alert-success||alert-info||alert-warning||alert-danger"
					, href: "link"                    
				}
			 */
			options) {
		    //seting default setting
		    var _settings = {
		        type: "msgSuccess"
					, href: ""
		    }
		    ;
		    var _myAlert = ""
				, _waitTime = 5000
		    ;
		    //merge with setting parameter
		    _settings = $.extend(true, _settings, options);

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
}(jQuery)));

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
		                callback(serverResponse);
		            });
		        }
				, Success: function (callback) {
				    _serviceReponsed.success(function (serverResponse) {
				        if (TNT.Service.Success(serverResponse)) {
				            callback(serverResponse);
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
        //Show stripe checkout form, then call server side action to perform charging
        , StripeCheckout: function (object, thss) {
            
            var myStripe = StripeCheckout.configure({
                key: "pk_test_wwYvgcyD8v1I6Y1FXprw0WLA",
                image: TNT.Common.$Settings("input#img-Mybrus").val(),
                email: "luc.huynh78@gmail.com",
                token: function (token) {
                    // Use the token to create the charge with a server-side script.
                    // You can access the token ID with token.id, token.email and token.card
                    var myParas = "stripeToken={0}&stripeEmail={1}&{2}".format(token.id, token.email, $.param(object));
                    //TNT.Common.$Settings("input#url-Charge").val()
                    TNT.Service.GCall("/home/charge", myParas)
                        .Success(function (d) {
                            TNT.Common.Alert("Your item will be shipped asap!", { type: "alert-success" });
                        });
                }
            })
                , $scope = $(thss).parents($("div#scopeBuy"))
                , quantity = $scope.find("input#product_qty").val() || 1
                , size = $scope.find("select#ddlSize").val()
            ;
            
            object.quantity = quantity;
            object.size = size;

            myStripe.open({
                name: "MyBrus",
                description: object.description,
                amount: object.quantity * object.price * 100
            });

            $(window).on("popstate", function () {
                myStripe.close();
            });
        }
    }
}(jQuery));
