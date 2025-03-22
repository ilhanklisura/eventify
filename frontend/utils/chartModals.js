$(document).ready(function () {
	// Ensure the DOM is ready before executing Chart.js code

	// Check if the barChart element exists
	if ($('#barChart').length) {
	  var barChartCanvas = document.getElementById('barChart').getContext('2d');

	  var barChartData = {
		 labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
		 datasets: [
			{
			  label: 'Reserved Tickets',
			  backgroundColor: 'rgba(60,141,188,0.9)',
			  borderColor: 'rgba(60,141,188,0.8)',
			  data: [28, 48, 40, 19, 86, 27, 90]
			},
			{
			  label: 'Page Visitors',
			  backgroundColor: 'rgba(210, 214, 222, 1)',
			  borderColor: 'rgba(210, 214, 222, 1)',
			  data: [65, 59, 80, 81, 56, 55, 40]
			}
		 ]
	  };

	  var barChartOptions = {
		 responsive: true,
		 maintainAspectRatio: false
	  };

	  new Chart(barChartCanvas, {
		 type: 'bar',
		 data: barChartData,
		 options: barChartOptions
	  });
	}

	// Check if the donutChart element exists
	if ($('#donutChart').length) {
	  var donutChartCanvas = document.getElementById('donutChart').getContext('2d');

	  var donutData = {
		 labels: ['Predstava - \"Mojoj dragoj BiH\"', 'Srebrenica Memorial', 'Visit Hercegovina - \"Mostar, Ljubuški, Neum\"', 'Sarajevo Derby - \"Sarajevo - Željezničar\"', 'The Industrial City - \"Zenica\"', 'Concert - \"Dragana Mirković\"'],
		 datasets: [
			{
			  data: [700, 500, 400, 600, 300, 100],
			  backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de']
			}
		 ]
	  };

	  var donutOptions = {
		 maintainAspectRatio: false,
		 responsive: true
	  };

	  new Chart(donutChartCanvas, {
		 type: 'doughnut',
		 data: donutData,
		 options: donutOptions
	  });
	}
 });