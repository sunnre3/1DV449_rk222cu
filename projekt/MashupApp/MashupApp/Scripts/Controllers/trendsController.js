app.controller('trendsController', function ($scope, $q, Restangular, storageService) {
	// At init set loading to true.
	$scope.loading = true;

	// Check if there are data stored on client and
	// if there are, verify that they are correct
	// and up to date.
	if (storageService.exists('trends') &&
		storageService.get('trends').length == 10 &&
		moment(storageService.get('trends')[0].expiresAt).isAfter()) {
		// If they are we can process them.
		processTrends(storageService.get('trends'));
	} else {
		// If they are not, retrieve new ones from back-end.
		Restangular.all('trends').getList().then(function (trends) {
			if (trends.length > 0) {
				// And process them instead.
				processTrends(trends);
			}
			// If the request was successful but we simply don't recieve
			// any data there could be something wrong with the API.
			// In this case we instead give $scope whatever is stored in
			// localStorage.
			else {
				if (storageService.exists('trends')) {
					processTrends(storageService.get('trends'));
				}
			}
		}, function errorCallback() {
			if (storageService.exists('trends')) {
				processTrends(storageService.get('trends'));
			}

			else {
				$scope.error = true;
				$scope.loading = false;
			}
		});
	}

	function processTrends(trends) {
		// Give $scope a reference.
		$scope.trends = trends;

		// Set the page title.
		$scope.pageTitle = trends[0].name;

		// Get twitter and instagram data either from
		// localStorage or from back-end.
		getTwitterData(trends[0].name);
		getInstagramData(trends[0].name);

		// Save the trends in localStorage.
		storageService.set('trends', trends);
	}

	function getTwitterData(hashtag) {
		// First check if there are anything stored
		// in the clients localStorage and if there is
		// we check that it's also up to date.
		if (storageService.exists('tweets.' + hashtag) &&
			moment(storageService.get('tweets.' + hashtag)[0].expiresAt).isAfter()) {
			// Give $scope the stored tweets.
			$scope.tweets = storageService.get('tweets.' + hashtag);

			// Set $scope.loading
			$scope.loading = isLoading();
		} else {
			// Make a request to the back-end.
			Restangular.all('tweets/hashtag/' + hashtag).getList().then(function (tweets) {
				if (tweets.length > 0) {
					// Give $scope the retrieved tweets.
					$scope.tweets = tweets;

					// Save the entries in localStorage.
					storageService.set('tweets.' + hashtag, tweets);

					// Set $scope.loading
					$scope.loading = isLoading();
				}
				// If the request was successful but we simply don't recieve
				// any data there could be something wrong with the API.
				// In this case we instead give $scope whatever is stored in
				// localStorage.
				else {
					if (storageService.exists('tweets.' + hashtag)) {
						$scope.tweets = storageService.get('tweets.' + hashtag);
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
	}

	function getInstagramData(hashtag) {
		// First check if there are anything stored
		// in the clients localStorage and if there is
		// we check that it's also up to date.
		if (storageService.exists('instagrams.' + hashtag) &&
			moment(storageService.get('instagrams.' + hashtag)[0].expiresAt).isAfter()) {
			// Give $scope the stored tweets.
			$scope.instagrams = storageService.get('instagrams.' + hashtag);

			// Set $scope.loading
			$scope.loading = isLoading();
		} else {
			// Make a request to the back-end.
			Restangular.all('instagrams/hashtag/' + hashtag).getList().then(function (instagrams) {
				if (instagrams.length > 0) {
					// Give $scope the retrieved tweets.
					$scope.instagrams = instagrams;

					// Save the entries in localStorage.
					storageService.set('instagrams.' + hashtag, instagrams);

					// Set $scope.loading
					$scope.loading = isLoading();
				}
				// If the request was successful but we simply don't recieve
				// any data there could be something wrong with the API.
				// In this case we instead give $scope whatever is stored in
				// localStorage.
				else {
					if (storageService.exists('instagrams.' + hashtag)) {
						$scope.tweets = storageService.get('instagrams.' + hashtag);
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
	}

	function isLoading() {
		return typeof ($scope.tweets) == 'undefined' && typeof ($scope.instagrams) == 'undefined';
	}
});