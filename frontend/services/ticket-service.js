let TicketService = {
    events: {},
    users: {},

    init: function () {
        const user = Utils.parseJwt(localStorage.getItem("user_token"))?.user;
        this.userRole = user?.role;

        if (this.userRole === "attendee") {
            $(".btn-primary, .btn-warning, .btn-danger").hide();
        }

        this.loadEvents(() => {
            this.loadUsers(() => {
                this.loadTickets();
            });
        });

        $("#ticketForm").validate({
            rules: {
                ticketEvent: { required: true },
                ticketUser: { required: true },
                ticketPrice: { required: true, number: true, min: 0 },
                ticketStatus: { required: true }
            },
            messages: {
                ticketEvent: { required: "Event is required" },
                ticketUser: { required: "User is required" },
                ticketPrice: {
                    required: "Price is required",
                    number: "Price must be a number",
                    min: "Price cannot be negative"
                },
                ticketStatus: { required: "Status is required" }
            },
            submitHandler: function (form) {
                const id = $("#ticketId").val();
                const data = {
                    event_id: $("#ticketEvent").val(),
                    user_id: $("#ticketUser").val(),
                    price: $("#ticketPrice").val(),
                    status: $("#ticketStatus").val()
                };

                const callback = () => {
                    toastr.success(`Ticket ${id ? "updated" : "added"} successfully.`);
                    $("#ticketModal").modal("hide");
                    TicketService.loadTickets();
                };

                if (id) {
                    RestClient.put(`tickets/${id}`, data, callback);
                } else {
                    RestClient.post("tickets", data, callback);
                }
            }
        });
    },

    loadTickets: function () {
        RestClient.get("tickets", (tickets) => {
            if ($.fn.DataTable.isDataTable("#ticketsTable")) {
                $("#ticketsTable").DataTable().destroy();
            }

            const tbody = $("#ticketsTable tbody");
            tbody.empty();

            tickets.forEach(ticket => {
                const eventName = this.events[ticket.event_id] || ticket.event_id;
                const userName = this.users[ticket.user_id] || ticket.user_id;

                const statusBadge = {
                    "Available": '<span class="badge badge-success">Available</span>',
                    "Sold": '<span class="badge badge-secondary">Sold</span>',
                    "Cancelled": '<span class="badge badge-danger">Cancelled</span>'
                }[ticket.status] || `<span class="badge badge-light">${ticket.status}</span>`;

                let row = `<tr>
                    <td>${ticket.id}</td>
                    <td>${eventName}</td>
                    <td>${userName}</td>
                    <td>${ticket.price} KM</td>
                    <td>${statusBadge}</td>
                    <td>
                        ${this.userRole === "organizer" ? `
                          <button class="btn btn-warning btn-sm" onclick="TicketService.openEditModal(${ticket.id}, ${ticket.event_id}, ${ticket.user_id}, ${ticket.price}, '${ticket.status}')">Edit</button>
                          <button class="btn btn-danger btn-sm" onclick="TicketService.delete(${ticket.id})">Delete</button>
                        ` : "No Access"}
                    </td>
                </tr>`;
                tbody.append(row);
            });

            $("#ticketsTable").DataTable();
        });
    },

    openAddModal: function () {
        $("#ticketForm")[0].reset();
        $("#ticketId").val('');
        $("#ticketModal .modal-title").text("Add Ticket");
        $("#ticketModal").modal("show");
    },

    openEditModal: function (id, event_id, user_id, price, status) {
        $("#ticketId").val(id);
        $("#ticketEvent").val(event_id);
        $("#ticketUser").val(user_id);
        $("#ticketPrice").val(price);
        $("#ticketStatus").val(status);
        $("#ticketModal .modal-title").text("Edit Ticket");
        $("#ticketModal").modal("show");
    },

    delete: function (id) {
        if (confirm("Are you sure you want to delete this ticket?")) {
            RestClient.delete(`tickets/${id}`, {}, () => {
                toastr.success("Ticket deleted.");
                TicketService.loadTickets();
            });
        }
    },

    loadEvents: function (callback) {
        RestClient.get("events", (events) => {
            const select = $("#ticketEvent").empty();
            events.forEach(event => {
                this.events[event.id] = event.title;
                select.append(`<option value="${event.id}">${event.title}</option>`);
            });
            if (callback) callback();
        });
    },

    loadUsers: function (callback) {
        RestClient.get("users", (users) => {
            const select = $("#ticketUser").empty();
            users.forEach(user => {
                this.users[user.id] = user.name;
                select.append(`<option value="${user.id}">${user.name}</option>`);
            });
            if (callback) callback();
        });
    }
};