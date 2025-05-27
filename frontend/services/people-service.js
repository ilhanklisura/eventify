let PeopleService = {
    init: function () {
        this.loadUsers();

        $("#userForm").on("submit", function (e) {
            e.preventDefault();
            const id = $("#userId").val();
            const data = {
                name: $("#userName").val(),
                email: $("#userEmail").val(),
                password: $("#userPassword").val(),
                role: $("#userRole").val()
            };

            if (id) {
                RestClient.put(`users/${id}`, data, () => {
                    toastr.success("User updated");
                    $("#userModal").modal("hide");
                    PeopleService.loadUsers();
                });
            } else {
                RestClient.post("users", data, () => {
                    toastr.success("User created");
                    $("#userModal").modal("hide");
                    PeopleService.loadUsers();
                });
            }
        });
    },

    loadUsers: function () {
        RestClient.get("users", function (users) {
            if ($.fn.DataTable.isDataTable("#usersTable")) {
                $("#usersTable").DataTable().destroy();
            }

            const tbody = $("#usersTable tbody");
            tbody.empty();

            users.forEach(user => {
                let row = `<tr>
                    <td>${user.id}</td>
                    <td>${user.name}</td>
                    <td>${user.email}</td>
                    <td>${user.role}</td>
                    <td>
                      <button class="btn btn-warning btn-sm" onclick="PeopleService.openEditModal(${user.id}, '${user.name}', '${user.email}', '${user.role}')">Edit</button>
                      <button class="btn btn-danger btn-sm" onclick="PeopleService.delete(${user.id})">Delete</button>
                    </td>
                </tr>`;
                tbody.append(row);
            });

            $("#usersTable").DataTable();
        });
    },

    openAddModal: function () {
        $("#userForm")[0].reset();
        $("#userId").val('');
        $("#userModal .modal-title").text("Add User");
        $("#userModal").modal("show");
    },

    openEditModal: function (id, name, email, role) {
        $("#userId").val(id);
        $("#userName").val(name);
        $("#userEmail").val(email);
        $("#userPassword").val(""); // reset password
        $("#userRole").val(role);
        $("#userModal .modal-title").text("Edit User");
        $("#userModal").modal("show");
    },

    delete: function (id) {
        if (confirm("Are you sure you want to delete this user?")) {
            RestClient.delete(`users/${id}`, {}, function () {
                toastr.success("User deleted.");
                PeopleService.loadUsers();
            });
        }
    }
};
