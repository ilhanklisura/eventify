let VenueService = {
    init: function () {
        this.loadVenues();

        $("#venueForm").validate({
            rules: {
                venueName: {
                    required: true
                },
                venueLocation: {
                    required: true
                }
            },
            messages: {
                venueName: {
                    required: "Venue name is required"
                },
                venueLocation: {
                    required: "Venue location is required"
                }
            },
            submitHandler: function (form) {
                const id = $("#venueId").val();
                const data = {
                    name: $("#venueName").val(),
                    location: $("#venueLocation").val()
                };

                const callback = () => {
                    toastr.success(`Venue ${id ? "updated" : "created"} successfully.`);
                    $("#venueModal").modal("hide");
                    VenueService.loadVenues();
                };

                if (id) {
                    RestClient.put(`venues/${id}`, data, callback);
                } else {
                    RestClient.post("venues", data, callback);
                }
            }
        });
    },

    loadVenues: function () {
        RestClient.get("venues", function (venues) {
            const tbody = $("#venuesTable tbody").empty();

            venues.forEach(venue => {
                let row = `<tr>
                    <td>${venue.id}</td>
                    <td>${venue.name}</td>
                    <td>${venue.location}</td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="VenueService.openEditModal(${venue.id}, '${venue.name}', '${venue.location}')">Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="VenueService.delete(${venue.id})">Delete</button>
                    </td>
                </tr>`;
                tbody.append(row);
            });

            if (!$.fn.DataTable.isDataTable('#venuesTable')) {
                $('#venuesTable').DataTable();
            }
        });
    },

    openAddModal: function () {
        $("#venueForm")[0].reset();
        $("#venueId").val('');
        $("#venueModal .modal-title").text("Add Venue");
        $("#venueModal").modal("show");
    },

    openEditModal: function (id, name, location) {
        $("#venueId").val(id);
        $("#venueName").val(name);
        $("#venueLocation").val(location);
        $("#venueModal .modal-title").text("Edit Venue");
        $("#venueModal").modal("show");
    },

    delete: function (id) {
        if (confirm("Are you sure you want to delete this venue?")) {
            RestClient.delete(`venues/${id}`, {}, function () {
                toastr.success("Venue deleted");
                VenueService.loadVenues();
            });
        }
    }
};