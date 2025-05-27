let Utils = {
    datatable: function (table_id, columns, data, pageLength = 15) {
        if ($.fn.DataTable.isDataTable("#" + table_id)) {
            $("#" + table_id).DataTable().clear().destroy();
        }

        $("#" + table_id).DataTable({
            data: data,
            columns: columns,
            pageLength: pageLength,
            lengthMenu: [5, 10, 15, 25, 50, 100],
            responsive: true,
        });
    },

    parseJwt: function (token) {
        if (!token) return null;
        try {
            const payload = token.split('.')[1];
            return JSON.parse(atob(payload));
        } catch (e) {
            console.error("Invalid token", e);
            return null;
        }
    }
};
