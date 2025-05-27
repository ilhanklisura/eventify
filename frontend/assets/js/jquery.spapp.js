(function ($) {
    $.spapp = function (options) {
        var config, routes = {};

        config = $.extend({
            defaultView: $("main#spapp > section:last-child").attr("id"),
            templateDir: "./tpl/",
            pageNotFound: false,
        }, options);

        $("main#spapp > section").each(function () {
            const elm = $(this);
            routes[elm.attr("id")] = {
                view: elm.attr("id"),
                load: elm.data("load"),
                onCreate: function () {},
                onReady: function () {},
            };
        });

        this.route = function (options) {
            $.extend(routes[options.view], options);
        };

        const routeChange = function () {
            const id = location.hash.slice(1);

            // provjerava token
            if (typeof checkAuthRoute === "function") {
                if (!checkAuthRoute(id)) return;
            }

            const route = routes[id];
            const elm = $("#" + id);

            if (!elm || !route) {
                if (config.pageNotFound) {
                    window.location.hash = config.pageNotFound;
                    return;
                }
                console.log(id + " not defined");
                return;
            }

            if (elm.hasClass("spapp-created")) {
                route.onReady();
            } else {
                elm.addClass("spapp-created");
                if (!route.load) {
                    route.onCreate();
                    route.onReady();
                } else {
                    elm.load(config.templateDir + route.load, function () {
                        route.onCreate();
                        route.onReady();
                    });
                }
            }
        };

        this.run = function () {
            window.addEventListener("hashchange", routeChange);
            if (!window.location.hash) {
                window.location.hash = config.defaultView;
            } else {
                routeChange();
            }
        };

        return this;
    };
})(jQuery);