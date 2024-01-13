//baseado em http://jsfiddle.net/digitalbush/R6MPU/
jQuery(function ($) {
    
    var format = function (value) {
        if (value === '' || isNaN(value)) value = 0;
        var toks = Number(value).toFixed(2).replace('-', '').split('.');
        var display = 'R$ ' + $.map(toks[0].split('').reverse(), function (elm, i) {
            return [(i % 3 === 0 && i > 0 ? '.' : ''), elm];
        }).reverse().join('') + ',' + toks[1];

        return value < 0 ? '-' + display : display;
    };

    ko.subscribable.fn.dinheiro = function () {
        var target = this;

        var writeTarget = function (valor) {
            valor = String(valor).replace('.', '').replace(',', '.').replace(/[^0-9.-]/g, '');
            target(parseFloat(valor));
        };

        var result = ko.computed({
            read: function () {
                return target();
            },
            write: writeTarget
        });

        result.formatted = ko.computed({
            read: function () {
                return format(target());
            },
            write: writeTarget
        });

        result.isNegative = ko.computed(function () {
            return target() < 0;
        });

        return result;
    };

    ko.bindingHandlers.date = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(), allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);

            var d = "";
            if (valueUnwrapped) {
                var m = /Date\([\d+-]+\)/gi.exec(valueUnwrapped);
                if (m) {
                    d = String.format("{0:dd/MM/yyyy}", eval("new " + m[0]));
                }
            }
            $(element).text(d);
        }
    };
    ko.bindingHandlers.money = {
        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(), allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);

            var m = "";
            if (valueUnwrapped) {
                m = parseInt(valueUnwrapped);
                if (m) {
                    m = String.format("{0:n0}", m);
                }
            }
            $(element).text(m);
        }
    };

});