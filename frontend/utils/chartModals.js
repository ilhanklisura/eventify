$(document).ready(function () {
	// Users per day - Bar Chart
	if ($('#barChart').length) {
		$.getJSON(Constants.PROJECT_BASE_URL + "dashboard/chart/users-per-day", function (data) {
			const labels = data.map(item => item.date);
			const counts = data.map(item => item.count);

			var ctx = document.getElementById('barChart').getContext('2d');
			new Chart(ctx, {
				type: 'bar',
				data: {
					labels: labels,
					datasets: [{
						label: 'New Users Per Day',
						backgroundColor: 'rgba(60,141,188,0.9)',
						borderColor: 'rgba(60,141,188,0.8)',
						data: counts
					}]
				},
				options: {
					responsive: true,
					maintainAspectRatio: false
				}
			});
		});
	}

	// Most popular events - Donut Chart
	if ($('#donutChart').length) {
		$.getJSON(Constants.PROJECT_BASE_URL + "dashboard/chart/events-popular", function (data) {
			const labels = data.map(item => item.title);
			const counts = data.map(item => item.count);
			const backgroundColors = ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de'];

			var ctx = document.getElementById('donutChart').getContext('2d');
			new Chart(ctx, {
				type: 'doughnut',
				data: {
					labels: labels,
					datasets: [{
						data: counts,
						backgroundColor: backgroundColors
					}]
				},
				options: {
					maintainAspectRatio: false,
					responsive: true
				}
			});
		});
	}
});