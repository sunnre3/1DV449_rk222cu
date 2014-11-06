// Define the app.
var app = angular.module('app', ['ngRoute', 'restangular'])
.config(function ($routeProvider, RestangularProvider) {
	
	// Configure routes.
	$routeProvider.when('/', {
		controller: 'trendsController',
		templateUrl: 'scripts/views/trends/collection.html'
	}).when('/hashtag/:hashtag', {
		controller: 'hashtagController',
		templateUrl: 'scripts/views/trends/collection.html'
	}).otherwise({
		template: '<div class="alert alert-warning"><h1>404!</h1><p>Ett fel uppstod och det verkar inte finnas någon sida här. '+
			'Var vänlig försök igen.</p><p>Klicka <a href="#/">här</a> för att komma tillbaka till startsidan.</p></div>'
	});

	// Base configuration for RESTangular.
	RestangularProvider.setBaseUrl('/api/');
	RestangularProvider.addResponseInterceptor(function (data, operation, what, url, response, deferred) {
		// .. to look for getList operations
		if (operation === "getList") {
			if (data.data === undefined) {
				return new Array();
			}

			return data.data;
		}

		else {
			return response;
		}
	});

});