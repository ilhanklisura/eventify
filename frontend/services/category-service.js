let CategoryService = {
    init: function () {
        this.loadCategories();

        $("#categoryForm").validate({
            rules: {
                categoryName: {
                    required: true
                }
            },
            messages: {
                categoryName: {
                    required: "Category name is required"
                }
            },
            submitHandler: function (form) {
                const id = $("#categoryId").val();
                const name = $("#categoryName").val().trim();

                const payload = { name };

                if (id) {
                    RestClient.put(`categories/${id}`, payload, function () {
                        toastr.success("Category updated");
                        $("#categoryModal").modal("hide");
                        CategoryService.loadCategories();
                    });
                } else {
                    RestClient.post("categories", payload, function () {
                        toastr.success("Category created");
                        $("#categoryModal").modal("hide");
                        CategoryService.loadCategories();
                    });
                }
            }
        });
    },

    loadCategories: function () {
        RestClient.get("categories", function (response) {
            const categories = Array.isArray(response) ? response : (response?.data ?? []);
            const tbody = $("#categoriesTable tbody").empty();

            categories.forEach(cat => {
                let row = `<tr>
                    <td>${cat.id}</td>
                    <td>${cat.name}</td>
                    <td>
                        <button class="btn btn-sm btn-warning" onclick="CategoryService.openEditModal(${cat.id}, '${cat.name}')">Edit</button>
                        <button class="btn btn-sm btn-danger" onclick="CategoryService.delete(${cat.id})">Delete</button>
                    </td>
                </tr>`;
                tbody.append(row);
            });

            if (!$.fn.DataTable.isDataTable('#categoriesTable')) {
                $('#categoriesTable').DataTable();
            }
        });
    },

    openAddModal: function () {
        $("#categoryId").val('');
        $("#categoryName").val('');
        $("#categoryModal .modal-title").text('Add Category');
        $("#categoryModal").modal('show');
    },

    openEditModal: function (id, name) {
        $("#categoryId").val(id);
        $("#categoryName").val(name);
        $("#categoryModal .modal-title").text('Edit Category');
        $("#categoryModal").modal('show');
    },

    delete: function (id) {
        if (confirm("Are you sure you want to delete this category?")) {
            RestClient.delete(`categories/${id}`, {}, function () {
                toastr.success("Category deleted");
                CategoryService.loadCategories();
            });
        }
    }
};