let BookingService = {
    usersMap: {},
    eventsMap: {},

    init: function () {
        const user = Utils.parseJwt(localStorage.getItem("user_token"))?.user;
        this.userRole = user?.role;

        this.loadUsers(() => {
            this.loadEvents(() => {
                this.loadBookings();
            });
        });

        $("#bookingForm").on("submit", function (e) {
            e.preventDefault();
            const id = $("#bookingId").val();
            const data = {
                user_id: $("#bookingUser").val(),
                event_id: $("#bookingEvent").val()
            };

            if (id) {
                RestClient.put(`bookings/${id}`, data, () => {
                    toastr.success("Booking updated.");
                    $("#bookingModal").modal("hide");
                    BookingService.loadBookings();
                });
            } else {
                RestClient.post("bookings", data, () => {
                    toastr.success("Booking created.");
                    $("#bookingModal").modal("hide");
                    BookingService.loadBookings();
                });
            }
        });
    },

    loadBookings: function () {
        RestClient.get("bookings", (bookings) => {
            if ($.fn.DataTable.isDataTable("#bookingsTable")) {
                $("#bookingsTable").DataTable().destroy();
            }

            const tbody = $("#bookingsTable tbody").empty();

            bookings.forEach(b => {
                const userName = this.usersMap[b.user_id] || "Unknown";
                const eventTitle = this.eventsMap[b.event_id] || "Unknown";

                let row = `<tr>
                    <td>${b.id}</td>
                    <td>${userName}</td>
                    <td>${eventTitle}</td>`;

                if (this.userRole === "organizer") {
                    row += `<td>
                        <button class="btn btn-sm btn-warning" onclick="BookingService.openEditModal(${b.id}, ${b.user_id}, ${b.event_id})">Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="BookingService.delete(${b.id})">Delete</button>
                    </td>`;
                } else {
                    row += "<td>-</td>";
                }

                row += "</tr>";
                tbody.append(row);
            });

            $("#bookingsTable").DataTable();
        });
    },

    openAddModal: function () {
        $("#bookingForm")[0].reset();
        $("#bookingId").val('');
        $("#bookingModal .modal-title").text("Add Booking");
        $("#bookingModal").modal("show");
    },

    openEditModal: function (id, user_id, event_id) {
        $("#bookingId").val(id);
        $("#bookingUser").val(user_id);
        $("#bookingEvent").val(event_id);
        $("#bookingModal .modal-title").text("Edit Booking");
        $("#bookingModal").modal("show");
    },

    delete: function (id) {
        if (confirm("Are you sure you want to delete this booking?")) {
            RestClient.delete(`bookings/${id}`, {}, () => {
                toastr.success("Booking deleted.");
                BookingService.loadBookings();
            });
        }
    },

    loadUsers: function (callback) {
        RestClient.get("users", (users) => {
            const select = $("#bookingUser").empty();
            users.forEach(user => {
                this.usersMap[user.id] = user.name;
                select.append(`<option value="${user.id}">${user.name}</option>`);
            });
            if (callback) callback();
        });
    },

    loadEvents: function (callback) {
        RestClient.get("events", (events) => {
            const select = $("#bookingEvent").empty();
            events.forEach(event => {
                this.eventsMap[event.id] = event.title;
                select.append(`<option value="${event.id}">${event.title}</option>`);
            });
            if (callback) callback();
        });
    }
};
