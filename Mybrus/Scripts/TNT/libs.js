/*
 * Who, When: What
 * Steven Luong, 12/31/2014: All libraries
 */

Insiite.Common || (Insiite.Common = (function ($) {
	return {
		Alert: function(msg, /*Default is green*/bgdColor,
			/*
				{
					display: "block",
					opacity: 0.90,
					position: "fixed",
					"text-align": "center",
					width: "100%",
					left: 0,
					top: 0,
					color: "white",					
				}
			 */
			options) {
			var _settings = {
				display: "block",
				opacity: 0.90,
				position: "fixed",
				"text-align": "center",
				width: "100%",
				left: 0,
				top: 0,
				color: "white",
				"background-color": bgdColor || "green"
			};            
			_settings = $.extend(true, _settings, options);
			$("<div class='ui-loader ui-overlay-shadow ui-body-e ui-corner-all'><h3>{0}</h3></div>".format(msg))
				.css(_settings)
				.appendTo($.mobile.pageContainer).delay(1500)
				.fadeOut(400, function () {
					$(this).remove();
				});
		}
		, Modal: function (open, options, $selector) {
			var isClose = ($.type(open) !== Insiite.Enum.Common.undefined && open !== "show");
			options = $.extend(true, {
				text: ""
				, textVisible: true
				, theme: "b"                    
			}, options);

			if ($.type($selector) !== Insiite.Enum.Common.undefined) {
				$selector = $($selector);
				if (!$selector.hasClass("wdn-modal")) {
					$selector.addClass("wdn-modal");
				}
			}
			else {
				if ($("body").find("div.wdn-modal").length < 1) {
					$selector = $("<div class='wdn-modal'/>");
					$("body").append($selector);
				}
			}

			if (isClose) {
				$selector = $selector || $("div.wdn-modal");
				$selector.removeClass("wdn-modal");
				$.mobile.loading("hide");
			}
			else {                
				$.mobile.loading("show", options).addClass("img-loader");
			}            
		}
		, ShowLoading: function () {
			this.Modal();
		}
		, HideLoading: function () {
			this.Modal("hide");
		}
		

	}
})(jQuery));

Insiite.Service || (Insiite.Service = function ($) {
	var _authorization = "Basic {0}";
	var _headers = {
		"Authorization": _authorization.format(_token())
	};
	function _token(/*x=>tokenValue*/value) {
		if (value) {	        
			var tokenValue = value.split("=>")[1];
			//Update headers
			_headers.Authorization = _authorization.format(tokenValue);
			localStorage.setItem(Insiite.Enum.Login.credentialKey, tokenValue);            
		}
		return localStorage.getItem(Insiite.Enum.Login.credentialKey);
	}
	return {
		//Default is basic.
		Headers: function (/*header authorization for Insiite.Service*/headers) {
			return _headers || (_headers = headers);
		}
		//Set, get, token value
		, Token: function (/*x=>tokenValue*/tokenValue) {
			
			return _token(tokenValue);
		}
		//Remove stored token
		, TokenClean: function () {
			localStorage.removeItem(Insiite.Enum.Login.credentialKey);
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
		, Message: {
			friendly: "An error occured, please try later!"
			, error: "Server returned failure."
			, failure: "Cannot call the service"
		}
		//consolidate $.ajax service call generic. Just return back Done, Success. Failure, and Complete are all taking care!
		, Call: function (options) {
			Insiite.Common.ShowLoading();
			var _serviceReponsed = $.ajax(options);
			_serviceReponsed
				.error(function () {
					Insiite.Common.Alert(Insiite.Service.Message.friendly, "red");
				})
				.complete(function () {
					Insiite.Common.HideLoading();
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
						if (Insiite.Service.Success(serverResponse)) {
							callback(serverResponse);                            
						}
						else {
							//Server responsed failure with exception message
							Insiite.Common.Alert(Insiite.Service.Failure(serverResponse), "red");                            
						}
					});
					
				}
				
			}                            
		}
		//Get service
		, GCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
			return Insiite.Service.Call({
				url: url
				, type: "get"
				, data: paras || ""
			});
		}
		//Post service
		, PCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
			return Insiite.Service.Call({
				url: url
				, type: "post"
				, data: paras || ""
			});
		}
		//Authorized get service
		, AGCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
			return Insiite.Service.Call({
				url: url
				, type: "get"
				, headers: _headers
				, data: paras || ""
			});
		}
		//Authorized post service
		, APCall: function (/*Action url*/url, /*parameters: json, or serialized string*/paras) {
			return Insiite.Service.Call({
				url: url
				, type: "post"
				, headers: _headers
				, data: paras || ""
			});
		}
        //Get url paras
        , GetPassedParas: function() {
            var paras =[]
                , hash
            ;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for(var i = 0; i < hashes.length; i++)
            {
                hash = hashes[i].split('=');
                paras.push(hash[0]);
                paras[hash[0]]= hash[1];
            }
            return paras;
        }
	}
}(jQuery));