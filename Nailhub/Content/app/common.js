/*
 * Who, When: What
 * StevenLuong, 12/03/2014: Common functions which created based on object literal notation structure
 */
//All common functions
TNT.Common = {
    //Get loading element
    loading: $("div#glbLoading").selector
    //Set all configuration options
    , Config: function (
        /*
         * Json object
         * loading: $selector of loading element
         */
        options
    ) {
        if ($.type(options) != InsiiteEnum.Undefined) {
            for (var option in options) {
                switch (option) {
                    case "loading":
                        this.loading = options[option];
                        break;
                    default:
                        break;
                }
            }
        }
        return this;
    }
    //Show full page loading
    , ShowLoading: function () {
        $(this.loading).show();
    }
    //Hide full page loading
    , HideLoading: function () {
        $(this.loading).hide();
    }
    //Get client information such as browser, os, etc
    , GetClientInfo: function () {
        var unknown = '-';
        // screen
        var screenSize = '';
        if (screen.width) {
            width = (screen.width) ? screen.width : '';
            height = (screen.height) ? screen.height : '';
            screenSize += '' + width + " x " + height;
        }
        //browser
        var nVer = navigator.appVersion
            , nAgt = navigator.userAgent
            , browser = navigator.appName
            , version = '' + parseFloat(navigator.appVersion)
            , majorVersion = parseInt(navigator.appVersion, 10)
            , nameOffset
            , verOffset
            , ix
        ;

        // Opera
        if ((verOffset = nAgt.indexOf('Opera')) != -1) {
            browser = 'Opera';
            version = nAgt.substring(verOffset + 6);
            if ((verOffset = nAgt.indexOf('Version')) != -1) {
                version = nAgt.substring(verOffset + 8);
            }
        }
            // MSIE
        else if ((verOffset = nAgt.indexOf('MSIE')) != -1) {
            browser = 'Microsoft Internet Explorer';
            version = nAgt.substring(verOffset + 5);
        }
            // Chrome
        else if ((verOffset = nAgt.indexOf('Chrome')) != -1) {
            browser = 'Chrome';
            version = nAgt.substring(verOffset + 7);
        }
            // Safari
        else if ((verOffset = nAgt.indexOf('Safari')) != -1) {
            browser = 'Safari';
            version = nAgt.substring(verOffset + 7);
            if ((verOffset = nAgt.indexOf('Version')) != -1) {
                version = nAgt.substring(verOffset + 8);
            }
        }
            // Firefox
        else if ((verOffset = nAgt.indexOf('Firefox')) != -1) {
            browser = 'Firefox';
            version = nAgt.substring(verOffset + 8);
        }
            // MSIE 11+
        else if (nAgt.indexOf('Trident/') != -1) {
            browser = 'Microsoft Internet Explorer';
            version = nAgt.substring(nAgt.indexOf('rv:') + 3);
        }
            // Other browsers
        else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) < (verOffset = nAgt.lastIndexOf('/'))) {
            browser = nAgt.substring(nameOffset, verOffset);
            version = nAgt.substring(verOffset + 1);
            if (browser.toLowerCase() == browser.toUpperCase()) {
                browser = navigator.appName;
            }
        }
        // trim the version string
        if ((ix = version.indexOf(';')) != -1) version = version.substring(0, ix);
        if ((ix = version.indexOf(' ')) != -1) version = version.substring(0, ix);
        if ((ix = version.indexOf(')')) != -1) version = version.substring(0, ix);

        majorVersion = parseInt('' + version, 10);
        if (isNaN(majorVersion)) {
            version = '' + parseFloat(navigator.appVersion);
            majorVersion = parseInt(navigator.appVersion, 10);
        }

        // mobile version
        var mobile = /Mobile|mini|Fennec|Android|iP(ad|od|hone)/.test(nVer);

        // cookie
        var cookieEnabled = (navigator.cookieEnabled) ? true : false;

        if (typeof navigator.cookieEnabled == 'undefined' && !cookieEnabled) {
            document.cookie = 'testcookie';
            cookieEnabled = (document.cookie.indexOf('testcookie') != -1) ? true : false;
        }

        // system
        var os = unknown;
        var clientStrings = [
            { s: 'Windows 3.11', r: /Win16/ },
            { s: 'Windows 95', r: /(Windows 95|Win95|Windows_95)/ },
            { s: 'Windows ME', r: /(Win 9x 4.90|Windows ME)/ },
            { s: 'Windows 98', r: /(Windows 98|Win98)/ },
            { s: 'Windows CE', r: /Windows CE/ },
            { s: 'Windows 2000', r: /(Windows NT 5.0|Windows 2000)/ },
            { s: 'Windows XP', r: /(Windows NT 5.1|Windows XP)/ },
            { s: 'Windows Server 2003', r: /Windows NT 5.2/ },
            { s: 'Windows Vista', r: /Windows NT 6.0/ },
            { s: 'Windows 7', r: /(Windows 7|Windows NT 6.1)/ },
            { s: 'Windows 8.1', r: /(Windows 8.1|Windows NT 6.3)/ },
            { s: 'Windows 8', r: /(Windows 8|Windows NT 6.2)/ },
            { s: 'Windows NT 4.0', r: /(Windows NT 4.0|WinNT4.0|WinNT|Windows NT)/ },
            { s: 'Windows ME', r: /Windows ME/ },
            { s: 'Android', r: /Android/ },
            { s: 'Open BSD', r: /OpenBSD/ },
            { s: 'Sun OS', r: /SunOS/ },
            { s: 'Linux', r: /(Linux|X11)/ },
            { s: 'iOS', r: /(iPhone|iPad|iPod)/ },
            { s: 'Mac OS X', r: /Mac OS X/ },
            { s: 'Mac OS', r: /(MacPPC|MacIntel|Mac_PowerPC|Macintosh)/ },
            { s: 'QNX', r: /QNX/ },
            { s: 'UNIX', r: /UNIX/ },
            { s: 'BeOS', r: /BeOS/ },
            { s: 'OS/2', r: /OS\/2/ },
            { s: 'Search Bot', r: /(nuhk|Googlebot|Yammybot|Openbot|Slurp|MSNBot|Ask Jeeves\/Teoma|ia_archiver)/ }
        ];
        for (var id in clientStrings) {
            var cs = clientStrings[id];
            if (cs.r.test(nAgt)) {
                os = cs.s;
                break;
            }
        }

        var osVersion = unknown;

        if (/Windows/.test(os)) {
            osVersion = /Windows (.*)/.exec(os)[1];
            os = 'Windows';
        }

        switch (os) {
            case 'Mac OS X':
                osVersion = /Mac OS X (10[\.\_\d]+)/.exec(nAgt)[1];
                break;

            case 'Android':
                osVersion = /Android ([\.\_\d]+)/.exec(nAgt)[1];
                break;

            case 'iOS':
                osVersion = /OS (\d+)_(\d+)_?(\d+)?/.exec(nVer);
                osVersion = osVersion[1] + '.' + osVersion[2] + '.' + (osVersion[3] | 0);
                break;
        }

        // flash (you'll need to include swfobject)
        /* script src="//ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js" */
        var flashVersion = 'no check';
        if (typeof swfobject != 'undefined') {
            var fv = swfobject.getFlashPlayerVersion();
            if (fv.major > 0) {
                flashVersion = fv.major + '.' + fv.minor + ' r' + fv.release;
            }
            else {
                flashVersion = unknown;
            }
        }
        var clientInfo = {
            screen: screenSize,
            browser: browser,
            browserVersion: version,
            mobile: mobile,
            os: os,
            osVersion: osVersion,
            cookies: cookieEnabled,
            flashVersion: flashVersion
        };

        return clientInfo;
    }
    //Get IE version
    , GetIEVersion: function () {
        var clientInfo = this.GetClientInfo();
        if (clientInfo.browser.trim().indexOf("Microsoft Internet Explorer") > -1) {
            return clientInfo.browserVersion;
        }
        return -1;
    }
    //Get super click events string
    , GetSuperClick: function (/*Such as click touchstart vclick, etc without any seperators*/events) {
        var superClick = this.IsNothing(events) ? "click touchstart vclick" : events; //computer, ios, and android
        return superClick;
    }
    //Redirect to url
    , RedirectToUrl: function (
        /*Url can be full or short*/url
        , /*Open new windows, or redirect*/ openNew
        , /*Name, or _blank, _parent, _self, _top*/specify
        , /*toolbar, location, status, menubar, scrollbars, resizable, width, height, etc =?*/option
    ){
        if (openNew != "undefined" && openNew) {
            if ($.type(specify) != "undefined") {
                if ($.type(option) != "undefined") {
                    window
                        .open(url, specify, option)
                        .focus()
                    ;
                }
                else {
                    window
                        .open(url, specify)
                        .focus()
                    ;
                }

            }
            else {
                window
                    .open(url)
                    .focus()
                ;
            }
        }
        else {
            this.ShowLoading();
            window.location = url.IsFullUrl ? url : "{0}//{1}{2}".Format(document.location.protocol, document.location.host, url);
        }
        
    }
    //Return text with highlighted searching keywords
    , HighlightSearch: function(keyword, searchText) {
        keyword = keyword.replace(/(\s+)/, "(<[^>]+>)*$1(<[^>]+>)*");
        var pattern = new RegExp("(" + keyword + ")", "gi");

        searchText = textSource.replace(pattern, "<mark>$1</mark>");
        searchText = textSource.replace(/(<mark>[^<>]*)((<[^>]+>)+)([^<>]*<\/mark>)/, "$1</mark>$2<mark>$4");

        return searchText;
    }
    //Set up dropdownlist
    , Dropdownlist: {
        //Initiate dropdownlist
        Init: function ($selector, /*Must be {Id, Name}*/dataSource, selectedIdText) {
            if (dataSource.length > 0) {
                $selector.find('option').remove();
                $.each(dataSource, function (i, item) {
                    var option = $('<option>', {
                        value: item.Id
                    }).html(item.Name).appendTo($selector);
                });
                this.SetSelectedIdText($selector, selectedIdText);
            };
        }
        , SetSelectedIdText: function ($selector, selectedIdText) {
            if (!TNT.Common.IsNothing(selectedIdText)) {
                selectedIdText = $.trim(selectedIdText);
                if ($.isNumeric(selectedIdText)) {
                    $selector.val(selectedIdText);
                }
                else {
                    $selector.find("option")
                            .filter(function () { return $.trim($(this).text()) == selectedIdText; })
                            .attr('selected', true)
                    ;
                }
            }
        }       
    }
    //JQueryUI
    , JUI: {
        //Autocomplete
        Autocomplete: {
            //Set configuration for jquery ui autocomplete
            Config: function (
                /*
                 * zIndex, maxHeight, maxWidthContent, overflow
                 * maxWidthTextbox: set max width for autocomplete textbox
                 */
                options
            ) {
                $(".ui-autocomplete")
                    .css("z-index", !TNT.Common.IsNothing(options[zIndex]) && options[zIndex])
                    .css("max-height", !TNT.Common.IsNothing(options[maxHeight]) && options[maxHeight])
                    .css("max-width", TNT.Common.IsNothing(options[maxWidthContent]) ? "auto" : options[maxWidthContent])
                    .css("overflow", TNT.Common.IsNothing(options[overflow]) ? "auto" : options[overflow])
                ;
                $("span.custom-combobox")
                    .find(".custom-combobox-input.ui-autocomplete-input").css("width", !TNT.Common.IsNothing(options[maxWidthTextbox]) && options[zIndex])
                ;
            }
            // Initiate autocomplete
            , Init: function ($selector, dataSource
                , /*
             *selectorId: Keep selectedId to this $selector element
             * bgSpinner: Spinner class for background
             */options
            ) {
                $selector.autocomplete({
                    source: data
                    , select: function (event, ui) {
                        !TNT.Common.IsNothing(options[selectorId]) && options[selectorId].val(ui.item.Id);
                        $(this).val(ui.item.value);
                        return false;
                    }
                    , search: function (event, ui) {
                        !TNT.Common.IsNothing(options[bgSpinner]) && $(this).addClass(options[bgSpinner]);
                    }
                    , response: function (event, ui) {
                        if (ui.content.length === 0) {
                            !TNT.Common.IsNothing(options[selectorId]) && options[selectorId].val("");
                            !TNT.Common.IsNothing(options[bgSpinner]) && $selector.removeClass(options[bgSpinner]);
                        }
                    }
                    , open: function (event, ui) {
                        setTimeout(function () {
                            !TNT.Common.IsNothing(options[bgSpinner]) && $selector.removeClass(options[bgSpinner]);
                        }, 100);
                    }
                })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    return $("<li>")
                        .append($("<a>").text(item.label))
                        .appendTo(ul);
                };
                //set z-index > TNT.Modal z-index
                $(".ui-autocomplete")
                    .css("z-index", 1001)
                    .css("max-height", 200)
                    .css("overflow", "auto")
                ;
            }
        }
        //Dialog
        , Dialog: {

        }
    }
    //Check object is undefined, or null
    , IsNothing: function(object){
        return $.type(object).Like(TNTEnum.Undefined, TNTEnum.Null);
    }
    //Check the cursor to see whether we need to load more data when a page is scolling down
    , IsScrollMore: function ($selector, /*The distance is between a bottom page and a current cursor's position. Default is 10*/distance) {
        if ($selector.scrollTop() + $selector.innerHeight() >= $selector[0].scrollHeight - this.IsNothing(distance)? 10: distance) {
            return true;
        }
        return false;
    }

}



function Dragscrollable(scrollOnTextElements, nonscrollElements) {
    if (!Modernizr.touch) {
        var option;
        //if ($.type(scrollElements) != "undefined" && $.type(nonscrollElements) != "undefined") {
        //    option = {
        //        scrollOnText: scrollElements
        //        , scrollstoppers: nonscrollElements
        //    };
        //}
        //else if ($.type(scrollElements) != "undefined") {
        //    option = {
        //        scrollOnText: scrollElements
        //    };
        //}
        //else if ($.type(nonscrollElements) != "undefined") {
        //    option = {
        //        scrollstoppers: nonscrollElements
        //    };
        //}
        option = {
            scrollOnText: scrollOnTextElements
            , scrollstoppers: nonscrollElements
        };
        drag.scrollable.enable(option);
    }
}


var waitForFinalEvent = (function () {
    var timers = {};
    return function (callback, ms, uniqueId) {
        if (!uniqueId) {
            uniqueId = "Don't call this twice without a uniqueId";
        }
        if (timers[uniqueId]) {
            clearTimeout(timers[uniqueId]);
        }
        timers[uniqueId] = setTimeout(callback, ms);
    };
})();

function MarconeSplit(stringText, iregPatern, index) {
    ///<summary>Split base on iregPatern
    ///</summary>
    ///<param name = 'stringText'>123456abcd, or acbd123456</param>
    ///<param name = 'iregPatern'>IReg.SplitNumber or something else</param>
    var returnValue = stringText.split(iregPatern);
    if ($.type(index) != "undefined") {
        return returnValue[index];
    }
    else {
        return returnValue;
    }
}

function SubPanel($selector, event) {
    ///<summary>Show sub panel</summary>
    ///<param name = '$selector'>$selector for click event </param>
    var superClick = event ? event : GetSuperClick();
    $selector.bind(superClick, function (e) {
        var ieVersion = GetIEVersion();
        var subPanel;
        var totalChildren = 0;

        if (ieVersion != -1 && ieVersion < 9) {
            subPanel = $(this).find(".subPanel");
            var isVisible = $(subPanel).hasClass("disnone");

            if (isVisible) {
                totalChildren = $(subPanel).find("div.smallbox").length;
                if (totalChildren > 6 && $(subPanel).find("div.thedungfdeerscroll").length === 0) {
                    var curHtml = $(subPanel).html();
                    $(subPanel)
                        .css("max-width", "295px")
                        .css("padding-right", "5px")
                    ;

                    $(subPanel)
                        .html("")
                        .html($("<div class = 'thedungfdeerscroll' style='max-height: 400px; overflow-y: scroll; overflow-x: hidden;'></div>").append(curHtml))
                    ;

                }
                $(subPanel).removeClass("disnone");
            }
            else {
                $(subPanel).addClass("disnone");
            }
        }
        else {
            subPanel = $(this).parent().find(".subPanel"); //$(this).next(".subTile");

            if ($(subPanel).is(":visible")) {
                $(subPanel).fadeOut();
            }
            else {
                totalChildren = $(subPanel).find("div.smallbox").length;

                if (totalChildren > 6 && $(subPanel).find("div.thedungfdeerscroll").length === 0) {
                    var curHtml = $(subPanel).html();
                    $(subPanel)
                        .css("max-width", "295px")
                        .css("padding-right", "5px")
                    ;

                    $(subPanel)
                        .html("")
                        .html($("<div class = 'thedungfdeerscroll' style='max-height: 400px; overflow-y: scroll; overflow-x: hidden;'></div>").append(curHtml))
                    ;

                }
                $(subPanel).fadeIn();
            }
        }
    });
}

function OpenMyDialog(myDialog, urlAction, para, newTitle, width, positionTop, isAntiScroll) {
    ///<summary>
    ///Open content in dialog, setup like
    ///?div id="dialogWrapper"?
    ///     ?div class="dialog-content" /?
    ///     ?div class="dialog-waiting textcenter"?
    ///         ?img src="@Url.Content("~/Content/Images/spinner.gif")" alt="loading"/?
    ///     ?/div?
    ///?/div?
    ///</summary>
    ///<param name="myDialog">Dialog wrapper</param>
    ///<param name="urlAction">Url action</param>
    ///<param name="para">paras for action</param>
    ///<param name="newTitle">Dialog title</param>
    ///<param name="width">Dialog width</param>
    ///<param name="positionTop">Dialog top position</param>
    var option = {
        autoOpen: false,
        resizable: false,
        title: newTitle ? newTitle : $(myDialog).attr("title"),
        height: "auto",
        width: width ? width : $(window).width() - 100 < 950 ? $(window).width() - 100 : 950,
        position: ['top', positionTop ? positionTop : 0],
        modal: true,
        create: function (event, ui) {
            //here we can apply unique styling  
            $(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar").addClass("lightboxtitlebar");
            $(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar-close").addClass("lightboxclose");
            $(this).parents(".ui-dialog:first").find(".ui-icon-closethick").addClass("lightboxclosespan");
            $(this).parents(".ui-dialog:first").find(".ui-dialog-buttonpane").addClass("lightboxButtonPane");
            $(this).parents(".ui-dialog:first").find(".ui-dialog-buttonset").addClass("lightboxbutton");
            /*
            $(this).parents(".ui-dialog:first").find(".ui-button:first").addClass("lightboxblue");
            $(this).parents(".ui-dialog:first").find(".ui-button:last").addClass("lightboxgray");
            */
            $(this).parents(".ui-dialog:first").find(".ui-button-text").hide();
            $(this).parents(".ui-dialog").addClass("lightbox");
        },
        close: function (event, ui) {
            $(this).dialog("destroy");
        },
        open: function () {
            //$(this).dialog("option", "position", "center");
        },
        buttons: {
        }
    };

    $(myDialog).dialog(option);
    try {
        $(myDialog).dialog('open');
        LoadContent();
    }
    catch (ex) {

    }

    function LoadContent() {
        if ($.type(urlAction) != "undefined" && urlAction != null) {
            //load content from urlaction
            var dialogContent = $('.dialog-content', myDialog);
            dialogContent.css("display", "block");
            dialogContent.html("");
            $('.dialog-waiting', myDialog).show();
            dialogContent.load(
                urlAction
                , para ? para : ""
                , function (response, status, xhr) {
                    $('.dialog-waiting', myDialog).hide();
                    if ($.type(isAntiScroll) != "undefined") {
                        $('div.scrolls.antiscroll-wrap', myDialog).antiscroll();
                    }
                }
            );
        }
        else {
            $('.dialog-waiting', myDialog).hide();
            if ($.type(isAntiScroll) != "undefined") {
                $('div.scrolls.antiscroll-wrap', myDialog).antiscroll();
            }
        }
    }

}

function OpenErrorDialog(dlgMessage) {
    $(function () {
        $("#errorDialog").dialog({
            resizable: false,
            height: "auto",
            width: 420,
            modal: true,
            create: function (event, ui) {
                //here we can apply unique styling  
                $(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar").addClass("lightboxerrortitlebar");
                $(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar-close").addClass("lightboxerrorclose");
                $(this).parents(".ui-dialog:first").find(".ui-icon-closethick").addClass("lightboxclosespan");
                $(this).parents(".ui-dialog:first").find(".ui-dialog-buttonpane").addClass("lightboxButtonPane");
                $(this).parents(".ui-dialog:first").find(".ui-dialog-buttonset").addClass("lightboxbutton");
                /* StevenLuong: 04/10/2014 caused showing close after close icon on dialog
                $(this).parents(".ui-dialog:first").find(".ui-button:first").addClass("lightboxblue");
                $(this).parents(".ui-dialog:first").find(".ui-button:last").addClass("lightboxgray");
                */
                $(this).parents(".ui-dialog:first").find(".ui-button-text").hide();
                $(this).parents(".ui-dialog").addClass("lightbox");

            },
            close: function (event, ui) {
                $(this).dialog("destroy");
            },
            open: function () {
                if ($.type(dlgMessage) === "string" && dlgMessage.indexOf("=>") > -1) {
                    $(this).find("div#errMessage").html(dlgMessage.split("=>")[1].trim()).show();
                    $(this).find("div#errDefault").hide();
                }
                else {
                    $(this).find("div#errMessage").hide();
                    $(this).find("div#errDefault").show();
                }
            },
            buttons: {
            }
        });
    });
}

function toUSD(_number) {
    var number = _number.toString(),
    dollars = number.split('.')[0],
    cents = (number.split('.')[1] || '') + '00';
    dollars = dollars.split('').reverse().join('')
        .replace(/(\d{3}(?!$))/g, '$1,')
        .split('').reverse().join('');
    return '$' + dollars + '.' + cents.slice(0, 2);
}

this.IsNumeric = function (evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;

};

function GetPartNumberUrl() {
    var hostUrl = "";

    hostUrl = document.location.protocol + "//" + document.location.host + $('#hidAutoCompletePartNumberUrl').val();

    return hostUrl;
}

function GetRequestUrl(controllerName) {
    var hostUrl = "";

    hostUrl = document.location.protocol + "//" + document.location.host + document.location.pathname;

    var hrefLength = hostUrl.length;
    var backSlashLastIndex = hostUrl.lastIndexOf('/');

    hrefLength = hrefLength - 1;
    if (hrefLength == backSlashLastIndex) {
        hostUrl = hostUrl;
    }
    else {
        hostUrl = hostUrl + "/";
    }

    var findIndex = hostUrl.indexOf(controllerName);
    if (findIndex > -1) {
        hostUrl = document.location.href.substring(0, (findIndex + controllerName.length));
    }
    else {
        hostUrl = hostUrl + controllerName;
    }

    var href = hostUrl;

    hrefLength = hostUrl.length;
    backSlashLastIndex = hostUrl.lastIndexOf('/');

    hrefLength = hrefLength - 1;
    if (hrefLength == backSlashLastIndex) {
        hostUrl = href;
    }
    else {
        hostUrl = href + "/";
    }

    return hostUrl;
}

function ReturnToBackPage() {
    var url = document.referrer;
    window.location = url;
}

function RedirectToErrorPage() {
    var errorRedirectUrl = $('#hidErrorRedirectUrl').val();
    window.location = document.location.protocol + "//" + document.location.host + errorRedirectUrl;
}

function NumberFormat(value, pointFraction) {
    var result;
    if (pointFraction == 0) {
        result = parseInt(value);
    }
    else {
        result = parseFloat(value).toFixed(pointFraction);
    }
    return result;
}

function RedirectToCustomerDetailPage(url) {
    $('#divLoading').show();
    window.location = document.location.protocol + "//" + document.location.host + url;
}

function OpenOrderDetailsDialog(orderNo, url, serverId) {
    $('#divBackOrder').css("display", "block");
    $('#divBackOrder').load(url, { orderNo: orderNo, serverId: $.type(serverId) != "undefined" ? serverId : 1 },
        function (response, status, xhr) {
            if (response == "") {
            }
        });

    $(function () {
        $("#dialog-backOrder").dialog({
            resizable: false,
            height: "auto",
            width: 900,
            modal: true,
            create: function (event, ui) {
                //here we can apply unique styling  
                $(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar").addClass("lightboxtitlebar");
                $(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar-close").addClass("lightboxclose");
                $(this).parents(".ui-dialog:first").find(".ui-icon-closethick").addClass("lightboxclosespan");
                $(this).parents(".ui-dialog:first").find(".ui-dialog-buttonpane").addClass("lightboxButtonPane");
                $(this).parents(".ui-dialog:first").find(".ui-dialog-buttonset").addClass("lightboxbutton");
                $(this).parents(".ui-dialog:first").find(".ui-button:first").addClass("lightboxblue");
                $(this).parents(".ui-dialog:first").find(".ui-button:last").addClass("lightboxgray");
                $(this).parents(".ui-dialog").addClass("lightbox");
            },
            close: function (event, ui) {
                $(this).dialog("destroy");
            },
            buttons: {
            }
        });
    });
}

function formatDate(dateObj, format, isJasonDate) {
    ///<summary>Format data time</summary>
    ///<param name="dateObj">date object</param>
    ///<param name="format">
    /// 1: dd-mm-yyyy
    /// 2: yyyy-mm-dd
    /// 3: dd/mm/yyyy
    /// 4: MM/dd/yyyy HH:mm
    /// 5: MM/dd/yyyy HH:mm:ss
    /// 6: MM/dd/yyyy HH:mm:ss
    /// 7: MM/dd/yyyy     
    ///</param>
    ///<param name="isJasonDate">object is json date</param>
    if (isJasonDate) {
        dateObj = new Date(parseInt(dateObj.substr(6)));
    }
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var curr_date = dateObj.getDate();
    var curr_month = dateObj.getMonth();
    curr_month = curr_month + 1;
    var curr_year = dateObj.getFullYear();
    var curr_min = dateObj.getMinutes();
    var curr_hr = dateObj.getHours();
    var curr_sc = dateObj.getSeconds();

    if (curr_month.toString().length == 1)
        curr_month = '0' + curr_month;
    if (curr_date.toString().length == 1)
        curr_date = '0' + curr_date;
    if (curr_hr.toString().length == 1)
        curr_hr = '0' + curr_hr;
    if (curr_min.toString().length == 1)
        curr_min = '0' + curr_min;
    if (curr_sc.toString().length == 1)
        curr_sc = '0' + curr_sc;

    if (format == 1)//dd-mm-yyyy
    {
        return curr_date + "-" + curr_month + "-" + curr_year;
    }
    else if (format == 2)//yyyy-mm-dd
    {
        return curr_year + "-" + curr_month + "-" + curr_date;
    }
    else if (format == 3)//dd/mm/yyyy
    {
        return curr_date + "/" + curr_month + "/" + curr_year;
    }
    else if (format == 4)// MM/dd/yyyy HH:mm
    {
        return curr_month + "/" + curr_date + "/" + curr_year + " " + curr_hr + ":" + curr_min;
    }
    else if (format == 5)// MM/dd/yyyy HH:mm:ss
    {
        return curr_month + "/" + curr_date + "/" + curr_year + " " + curr_hr + ":" + curr_min + ":" + curr_sc;
    }
    else if (format == 6)// MM/dd/yyyy HH:mm:ss
    {
        return formatDateTo12Hour(curr_month + "/" + curr_date + "/" + curr_year + " " + curr_hr + ":" + curr_min + ":" + curr_sc);
    }
    else if (format == 7)//MM/dd/yyyy
    {
        return curr_month + "/" + curr_date + "/" + curr_year;
    }
}

function formatDateTo12Hour(date) {
    var d = new Date(date);
    var hh = d.getHours();
    var m = d.getMinutes();
    var s = d.getSeconds();
    var dd = "AM";
    var h = hh;
    if (h >= 12) {
        h = hh - 12;
        dd = "PM";
    }
    if (h == 0) {
        h = 12;
    }
    m = m < 10 ? "0" + m : m;

    s = s < 10 ? "0" + s : s;

    /* if you want 2 digit hours:
    h = h<10?"0"+h:h; */

    var pattern = new RegExp("0?" + hh + ":" + m + ":" + s);

    var repalcement = h + ":" + m;
    /* if you want to add seconds
    repalcement += ":"+s;  */
    repalcement += " " + dd;

    return date.replace(pattern, repalcement);
}

function gotoSearch() {
    var url = document.referrer;
    window.location = url;
}

