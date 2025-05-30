let EventService = {
    categoryMap: {},
    venueMap: {},
    userId: null,

    init: function () {
        const user = Utils.parseJwt(localStorage.getItem("user_token"))?.user;
        this.userRole = user?.role;
        this.userId = user?.id;

        if (this.userRole === "attendee") {
            $(".btn-primary, .btn-warning, .btn-danger").hide();
        }

        Promise.all([this.loadCategories(), this.loadVenues()]).then(() => {
            this.loadEvents();
        });

        $("#eventForm").validate({
            rules: {
                eventTitle: "required",
                eventDescription: "required",
                eventDate: "required",
                eventCategory: "required",
                eventVenue: "required"
            },
            messages: {
                eventTitle: "Title is required",
                eventDescription: "Description is required",
                eventDate: "Date is required",
                eventCategory: "Category is required",
                eventVenue: "Venue is required"
            },
            submitHandler: function (form) {
                const id = $("#eventId").val();
                const data = {
                    title: $("#eventTitle").val(),
                    description: $("#eventDescription").val(),
                    date: $("#eventDate").val(),
                    category_id: $("#eventCategory").val(),
                    venue_id: $("#eventVenue").val(),
                    organizer_id: EventService.userId
                };

                const callback = () => {
                    toastr.success(`Event ${id ? "updated" : "created"} successfully.`);
                    $("#eventModal").modal("hide");
                    EventService.loadEvents();
                };

                if (id) {
                    RestClient.put(`events/${id}`, data, callback);
                } else {
                    RestClient.post("events", data, callback);
                }
            }
        });
    },

    loadEvents: function () {
        RestClient.get("events", (events) => {
            if ($.fn.DataTable.isDataTable("#eventsTable")) {
                $("#eventsTable").DataTable().destroy();
            }

            const tbody = $("#eventsTable tbody").empty();

            events.forEach(event => {
                let row = `<tr>
                    <td>${event.id}</td>
                    <td>${event.title}</td>
                    <td>${event.description}</td>
                    <td>${event.date}</td>
                    <td>${this.categoryMap[event.category_id] || "N/A"}</td>
                    <td>${this.venueMap[event.venue_id] || "N/A"}</td>
                    <td>
                        ${this.userRole === "organizer"
                    ? `<button class="btn btn-warning btn-sm" onclick="EventService.openEditModal(${event.id}, '${event.title}', '${event.description}', '${event.date}', ${event.category_id}, ${event.venue_id})">Edit</button>
                               <button class="btn btn-danger btn-sm" onclick="EventService.confirmDelete(${event.id})">Delete</button>`
                    : "No Access"}
                    </td>
                </tr>`;
                tbody.append(row);
            });

            $("#eventsTable").DataTable();
        });
    },

    openAddModal: function () {
        $("#eventForm")[0].reset();
        $("#eventId").val('');
        $("#eventModal .modal-title").text("Add Event");
        $("#eventModal").modal("show");
    },

    openEditModal: function (id, title, description, date, category_id, venue_id) {
        $("#eventId").val(id);
        $("#eventTitle").val(title);
        $("#eventDescription").val(description);
        $("#eventDate").val(date);
        $("#eventCategory").val(category_id);
        $("#eventVenue").val(venue_id);
        $("#eventModal .modal-title").text("Edit Event");
        $("#eventModal").modal("show");
    },

    confirmDelete: function (id) {
        if (confirm("Are you sure you want to delete this event?")) {
            RestClient.delete(`events/${id}`, {}, () => {
                toastr.success("Event deleted.");
                EventService.loadEvents();
            });
        }
    },

    loadCategories: function () {
        return new Promise((resolve) => {
            RestClient.get("categories", (categories) => {
                const select = $("#eventCategory").empty();
                this.categoryMap = {};
                categories.forEach(cat => {
                    this.categoryMap[cat.id] = cat.name;
                    select.append(`<option value="${cat.id}">${cat.name}</option>`);
                });
                resolve();
            });
        });
    },

    loadVenues: function () {
        return new Promise((resolve) => {
            RestClient.get("venues", (venues) => {
                const select = $("#eventVenue").empty();
                this.venueMap = {};
                venues.forEach(venue => {
                    this.venueMap[venue.id] = venue.name;
                    select.append(`<option value="${venue.id}">${venue.name}</option>`);
                });
                resolve();
            });
        });
    }
};