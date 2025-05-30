let PeopleService = {
    init: function () {
        this.loadUsers();

        $("#userForm").validate({
            rules: {
                userName: { required: true },
                userEmail: { required: true, email: true },
                userPassword: {
                    required: function () {
                        return $("#userId").val() === ""; // obavezno samo kad se dodaje korisnik
                    },
                    minlength: 6
                },
                userRole: { required: true }
            },
            messages: {
                userName: { required: "Name is required" },
                userEmail: {
                    required: "Email is required",
                    email: "Invalid email format"
                },
                userPassword: {
                    required: "Password is required",
                    minlength: "Password must be at least 6 characters"
                },
                userRole: { required: "Role is required" }
            },
            submitHandler: function (form) {
                const id = $("#userId").val();
                const data = {
                    name: $("#userName").val(),
                    email: $("#userEmail").val(),
                    role: $("#userRole").val()
                };

                const password = $("#userPassword").val();
                if (password || id === "") {
                    data.password = password;
                }

                const callback = () => {
                    toastr.success(`User ${id ? "updated" : "created"} successfully.`);
                    $("#userModal").modal("hide");
                    PeopleService.loadUsers();
                };

                if (id) {
                    RestClient.put(`users/${id}`, data, callback);
                } else {
                    RestClient.post("users", data, callback);
                }
            }
        });
    },

    loadUsers: function () {
        RestClient.get("users", (users) => {
            if ($.fn.DataTable.isDataTable("#usersTable")) {
                $("#usersTable").DataTable().destroy();
            }

            const tbody = $("#usersTable tbody").empty();

            users.forEach(user => {
                let row = `<tr>
                    <td>${user.id}</td>
                    <td>${user.name}</td>
                    <td>${user.email}</td>
                    <td>${user.role}</td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="PeopleService.openEditModal(${user.id}, '${user.name}', '${user.email}', '${user.role}')">Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="PeopleService.delete(${user.id})">Delete</button>
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
        $("#userRole").val(role);
        $("#userPassword").val('');
        $("#userModal .modal-title").text("Edit User");
        $("#userModal").modal("show");
    },

    delete: function (id) {
        if (confirm("Are you sure you want to delete this user?")) {
            RestClient.delete(`users/${id}`, {}, () => {
                toastr.success("User deleted.");
                PeopleService.loadUsers();
            });
        }
    }
};