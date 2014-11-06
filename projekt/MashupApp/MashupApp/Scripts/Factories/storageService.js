app.factory('storageService', function ($rootScope) {
	return {
		get: function (key) {
			return JSON.parse(localStorage.getItem(key));
		},

		set: function (key, data) {
			localStorage.setItem(key, JSON.stringify(data));
		},

		exists: function (key) {
			return localStorage.getItem(key) !== null;
		},

		unset: function (key) {
			localStorage.removeItem(key);
		},

		clearAll: function () {
			localStorage.clear();
		}
	};
});