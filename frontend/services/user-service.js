var UserService = {
    init: function () {
        const token = localStorage.getItem("user_token");
        if (token && token !== undefined) {
            window.location.replace("index.html");
        }

        // Login form
        $("#login-form").validate({
            submitHandler: function (form) {
                const entity = Object.fromEntries(new FormData(form).entries());
                UserService.login(entity);
            },
        });

        // Register form
        $("#register-form").validate({
            submitHandler: function (form) {
                const entity = Object.fromEntries(new FormData(form).entries());
                UserService.register(entity);
            },
        });
    },

    login: function (entity) {
        $.ajax({
            url: Constants.PROJECT_BASE_URL + "auth/login",
            type: "POST",
            data: JSON.stringify(entity),
            contentType: "application/json",
            dataType: "json",
            success: function (result) {
                localStorage.setItem("user_token", result.data.token);
                window.location.replace("index.html");
            },
            error: function (XMLHttpRequest) {
                toastr.error(XMLHttpRequest?.responseText ?? "Login error");
            },
        });
    },

    register: function (entity) {
        $.ajax({
            url: Constants.PROJECT_BASE_URL + "auth/register",
            type: "POST",
            data: JSON.stringify(entity),
            contentType: "application/json",
            dataType: "json",
            success: function () {
                toastr.success("Registration successful. Please login.");
                window.location.replace("login.html");
            },
            error: function (XMLHttpRequest) {
                toastr.error(XMLHttpRequest?.responseText ?? "Registration error");
            },
        });
    },

    logout: function () {
        localStorage.clear();
        window.location.replace("login.html");
    },

    generateMenuItems: function () {
        const token = localStorage.getItem("user_token");
        const user = Utils.parseJwt(token)?.user;

        if (!user) {
            window.location.replace("login.html");
            return;
        }

        let nav = "";
        let main = "";

        // Shared items
        nav += '<li class="nav-item"><a class="nav-link" href="#dashboard"><i class="nav-icon fas fa-home"></i><p>Dashboard</p></a></li>';
        main += '<section id="dashboard" data-load="tpl/dashboard.html"></section>';

        nav += '<li class="nav-item"><a class="nav-link" href="#events"><i class="nav-icon far fa-calendar-alt"></i><p>Events</p></a></li>';
        main += '<section id="events" data-load="tpl/events.html"></section>';

        nav += '<li class="nav-item"><a class="nav-link" href="#tickets"><i class="nav-icon fas fa-ticket"></i><p>Tickets</p></a></li>';
        main += '<section id="tickets" data-load="tpl/tickets.html"></section>';

        if (user.role === "organizer") {
            nav += '<li class="nav-item"><a class="nav-link" href="#users"><i class="nav-icon fas fa-user"></i><p>Users</p></a></li>';
            nav += '<li class="nav-item"><a class="nav-link" href="#categories"><i class="nav-icon fas fa-list"></i><p>Categories</p></a></li>';
            nav += '<li class="nav-item"><a class="nav-link" href="#bookings"><i class="nav-icon fas fa-check"></i><p>Bookings</p></a></li>';
            nav += '<li class="nav-item"><a class="nav-link" href="#venues"><i class="nav-icon fas fa-location-dot"></i><p>Venues</p></a></li>';

            main += '<section id="users" data-load="tpl/users.html"></section>';
            main += '<section id="categories" data-load="tpl/categories.html"></section>';
            main += '<section id="bookings" data-load="tpl/bookings.html"></section>';
            main += '<section id="venues" data-load="tpl/venues.html"></section>';
        }

        // Logout
        nav += '<li class="nav-item"><a class="nav-link text-danger" onclick="UserService.logout()" href="#"><i class="nav-icon fas fa-sign-out-alt"></i><p>Logout</p></a></li>';

        $("#tabs").html(nav);
        $("#spapp").html(main);
    },

    checkRoleRoute: function () {
        const token = localStorage.getItem("user_token");
        const user = Utils.parseJwt(token)?.user;
        const route = window.location.hash.replace("#", "");

        if (!user) {
            window.location.replace("login.html");
            return;
        }

        const restrictedForAttendee = ["users", "categories", "bookings", "venues"];

        if (user.role === "attendee" && restrictedForAttendee.includes(route)) {
            toastr.error("You are not allowed to access this page!");
            window.location.replace("#dashboard");
        }
    }
};
