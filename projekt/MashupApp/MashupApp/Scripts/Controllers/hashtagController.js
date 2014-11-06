app.controller('hashtagController', function ($scope, $q, $routeParams, Restangular, storageService) {
	// At init set loading to true.
	$scope.loading = true;

	// Set the page title.
	$scope.pageTitle = $routeParams.hashtag;

	// Check if there are trends stored on client and
	// if there are, verify that they are correct
	// and up to date.
	if (storageService.exists('trends') &&
		storageService.get('trends').length == 10 &&
		moment(storageService.get('trends')[0].expiresAt).isAfter()) {

		// Assign to $scope.
		$scope.trends = storageService.get('trends');
	} else {
		// If they are not, retrieve new ones from back-end.
		Restangular.all('trends').getList().then(function (trends) {
			if (trends.length > 0) {
				// Assign to $scope.
				$scope.trends = trends;

				// Save the trends in localStorage.
				storageService.set('trends', trends);
			}
			// If the request was successful but we simply don't recieve
			// any data there could be something wrong with the API.
			// In this case we instead give $scope whatever is stored in
			// localStorage.
			else {
				if (storageService.exists('trends')) {
					$scope.trends = storageService.exists('trends');
				}
			}
		}, function errorCallback() {
			if (storageService.exists('trends.')) {
				$scope.trends = storageService.get('trends.');
			}

			else {
				$scope.error = true;
				$scope.loading = false;
			}
		});
	}

	// Get tweets.
	// First check if there are anything stored
	// in the clients localStorage and if there is
	// we check that it's also up to date.
	if (storageService.exists('tweets.' + $routeParams.hashtag) &&
		moment(storageService.get('tweets.' + $routeParams.hashtag)[0].expiresAt).isAfter()) {
		// Give $scope the stored tweets.
		$scope.tweets = storageService.get('tweets.' + $routeParams.hashtag);

		// Set $scope.loading
		$scope.loading = isLoading();
	} else {
		// Make a request to the back-end.
		Restangular.all('tweets/hashtag/' + $routeParams.hashtag).getList().then(function (tweets) {
			if (tweets.length > 0) {
				// Give $scope the retrieved tweets.
				$scope.tweets = tweets;

				// Save the entries in localStorage.
				storageService.set('tweets.' + $routeParams.hashtag, tweets);

				// Set $scope.loading
				$scope.loading = isLoading();
			}
			// If the request was successful but we simply don't recieve
			// any data there could be something wrong with the API.
			// In this case we instead give $scope whatever is stored in
			// localStorage.
			else {
				if (storageService.exists('tweets.' + $routeParams.hashtag)) {
					$scope.tweets = storageService.get('tweets.' + $routeParams.hashtag);
				}
			}
		}, function errorCallback() {
			if (storageService.exists('tweets.' + hashtag)) {
				$scope.tweets = storageService.get('tweets.' + hashtag);
			}

			else {
				$scope.error = true;
				$scope.loading = false;
			}
		});
	}

	// Get instagrams.
	// First check if there are anything stored
	// in the clients localStorage and if there is
	// we check that it's also up to date.
	if (storageService.exists('instagrams.' + $routeParams.hashtag) &&
		moment(storageService.get('instagrams.' + $routeParams.hashtag)[0].expiresAt).isAfter()) {
		// Give $scope the stored tweets.
		$scope.instagrams = storageService.get('instagrams.' + $routeParams.hashtag);

		// Set $scope.loading
		$scope.loading = isLoading();
	} else {
		// Make a request to the back-end.
		Restangular.all('instagrams/hashtag/' + $routeParams.hashtag).getList().then(function (instagrams) {
			if (instagrams.length > 0) {
				// Give $scope the retrieved tweets.
				$scope.instagrams = instagrams;

				// Save the entries in localStorage.
				storageService.set('instagrams.' + $routeParams.hashtag, instagrams);

				// Set $scope.loading
				$scope.loading = isLoading();
			}
			// If the request was successful but we simply don't recieve
			// any data there could be something wrong with the API.
			// In this case we instead give $scope whatever is stored in
			// localStorage.
			else {
				if (storageService.exists('instagrams.' + $routeParams.hashtag)) {
					$scope.instagrams = storageService.get('instagrams.' + $routeParams.hashtag);
				}
			}
		}, function errorCallback() {
			if (storageService.exists('instagrams.' + hashtag)) {
				$scope.instagrams = storageService.get('instagrams.' + hashtag);
			}

			else {
				$scope.error = true;
				$scope.loading = false;
			}
		});
	}

	function isLoading() {
		return typeof ($scope.tweets) == 'undefined' && typeof ($scope.instagrams) == 'undefined';
	}
});