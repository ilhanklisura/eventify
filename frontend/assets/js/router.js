$(document).ready(function () {
	var app = $.spapp({ pageNotFound: "error_404" }); // initialize SPApp

	app.route({ view: "dashboard", load: "dashboard.html" });
	app.route({ view: "users", load: "users.html" });
	app.route({ view: "categories", load: "categories.html" });
	app.route({ view: "events", load: "events.html" });
	app.route({ view: "tickets", load: "tickets.html" });
	app.route({ view: "bookings", load: "bookings.html" });
	app.route({ view: "venues", load: "venues.html" });

	app.run();
});
