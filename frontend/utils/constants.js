var Constants = {
	get_api_base_url: function () {
	  if (location.hostname == "localhost") {
		 return "http://localhost:8888/eventify/backend/";
	  } else {
		 return "";
	  }
	},
 };