"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Utility = /** @class */ (function () {
    function Utility() {
    }
    // Redirect della pagina
    Utility.redirect = function (url, router) {
        router.navigate([]).then(function (res) { window.open(url, '_self'); });
    };
    return Utility;
}());
exports.default = Utility;
//# sourceMappingURL=utility.js.map