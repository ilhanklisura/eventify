let RestClient = {
    get: function (url, callback, error_callback) {
        $.ajax({
            url: Constants.PROJECT_BASE_URL + url,
            type: "GET",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authentication", localStorage.getItem("user_token"));
            },
            success: function (response) {
                if (callback) callback(response);
            },
            error: function (jqXHR) {
                if (error_callback) error_callback(jqXHR);
                else toastr.error(jqXHR.responseText || "Unexpected error");
            },
        });
    },

    request: function (url, method, data, callback, error_callback) {
        $.ajax({
            url: Constants.PROJECT_BASE_URL + url,
            type: method,
            data: JSON.stringify(data),
            contentType: "application/json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authentication", localStorage.getItem("user_token"));
            },
        })
            .done(function (response) {
                if (callback) callback(response);
            })
            .fail(function (jqXHR) {
                if (error_callback) error_callback(jqXHR);
                else toastr.error(jqXHR.responseText || "Unexpected error");
            });
    },

    post: function (url, data, callback, error_callback) {
        this.request(url, "POST", data, callback, error_callback);
    },
    patch: function (url, data, callback, error_callback) {
        this.request(url, "PATCH", data, callback, error_callback);
    },
    put: function (url, data, callback, error_callback) {
        this.request(url, "PUT", data, callback, error_callback);
    },
    delete: function (url, data, callback, error_callback) {
        this.request(url, "DELETE", data, callback, error_callback);
    },
};
