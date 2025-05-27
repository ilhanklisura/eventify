let VenueService = {
    init: function () {
        this.loadVenues();

        $('#venueForm').on('submit', function (e) {
            e.preventDefault();
            const id = $('#venueId').val();
            const name = $('#venueName').val().trim();
            const location = $('#venueLocation').val().trim();
            if (!name || !location) return toastr.error("Both name and location are required");

            const data = { name, location };

            if (id) {
                // UPDATE
                RestClient.put(`venues/${id}`, data, function () {
                    toastr.success("Venue updated");
                    $('#venueModal').modal('hide');
                    VenueService.loadVenues();
                });
            } else {
                // CREATE
                RestClient.post("venues", data, function () {
                    toastr.success("Venue created");
                    $('#venueModal').modal('hide');
                    VenueService.loadVenues();
                });
            }
        });
    },

    loadVenues: function () {
        RestClient.get("venues", function (response) {
            const venues = Array.isArray(response) ? response : (response?.data ?? []);
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
        $('#venueId').val('');
        $('#venueName').val('');
        $('#venueLocation').val('');
        $('#venueModal .modal-title').text('Add Venue');
        $('#venueModal').modal('show');
    },

    openEditModal: function (id, name, location) {
        $('#venueId').val(id);
        $('#venueName').val(name);
        $('#venueLocation').val(location);
        $('#venueModal .modal-title').text('Edit Venue');
        $('#venueModal').modal('show');
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
