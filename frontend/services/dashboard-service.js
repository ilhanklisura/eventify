const Dashboard = {
    loadStats: function () {
        $.ajax({
            url: Constants.PROJECT_BASE_URL + 'dashboard/stats',
            method: 'GET',
            success: function (data) {
                $('#totalEvents').text(data.totalEvents);
                $('#totalTickets').text(data.totalTicketsSold);
                $('#totalUsers').text(data.totalUsers);
                $('#cancelledEvents').text(data.cancelledTickets);
            },
            error: function () {
                console.error('Failed to load dashboard stats');
            }
        });
    }
};

$(document).ready(function () {
    Dashboard.loadStats();
});
