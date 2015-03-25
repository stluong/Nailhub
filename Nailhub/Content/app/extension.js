/*
 * Who, When: What
 * StevenLuong, 12/03/2014: All useful extension, start with capital letter!
 */

//String extension
String.prototype.Format = function () {
    var args = arguments;
    return this.replace(/\{\{|\}\}|\{(\d+)\}/g, function (m, n) {
        if (m == "{{") { return "{"; }
        if (m == "}}") { return "}"; }
        return args[n];
    });
};
String.prototype.Like = function () {
    var args = Array.prototype.slice.call(arguments);
    return (new RegExp('\\b' + args.join('\\b|\\b') + '\\b')).test(this);
}
String.prototype.IsFullUrl = function () {
    var reg = new RegExp('^(?:[a-z]+:)?//', 'i');
    return reg.test(this);
}
String.prototype.GetOnlyNumber = function () {
    var ret = this.match(/\d+/);
    return ret ? ret[0] : null;
}

//Number extension
Number.prototype.toMoney = function (currency) {
    currency = currency ? currency : "$";
    if (currency === "0.00") {
        return "${0}".format(currency);
    }
    return currency + "" + this.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
}
