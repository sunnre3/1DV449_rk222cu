function SR(googleMap) {
	/**
	 * URL to the SR API.
	 * @type {String}
	 */
	var apiUrl = 'http://api.sr.se/api/v2/traffic/messages';

	/**
	 * Object reference to the Google map.
	 * @type {Google Map}
	 */
	var map;

	// Set map.
	map = googleMap;

	/**
	 * Gets all traffic related messages from the API.
	 * @param  {String} filter
	 * @param  {Integer} var numberOfMessages
	 * @return {Void}
	 */
	this.getMessages = function(filter, numberOfMessages) {
		var numberOfMessages = numberOfMessages || 100;
		var that = this;

		// Perform the AJAX call using jQuery.
		$.ajax({
			type: 		'GET',
			url:		apiUrl,
			dataType: 	'jsonp',
			data: {
				format: 'json',
				size: 	numberOfMessages
			}
		}).done(function (data) {
			// Messages.
			var messages = data.messages;

			// If a filter as provided we need to
			// filter the messages before we create markers.
			if (typeof filter !== 'undefined') {
				messages = filterMessages(messages, filter);
			}

			// Remove all markers.
			map.deleteMarkers();

			// Loop through the messages and set a marker
			// for each one of them.
			for (var i = 0; i < messages.length; i++) {
				map.setMarker(
					messages[i].latitude,
					messages[i].longitude,
					getContentHTML(messages[i])
				);
			}
		});
	};

	/**
	 * Filter messages by category.
	 * @param  {Object[]} messages
	 * @param  {String[]} filter
	 * @return {Object[]}
	 */
	filterMessages = function(messages, filter) {
		// The array to be returned when we are done.
		var filtered = [];

		for (var i = 0; i < messages.length; i++) {
			for (var s = 0; s < filter.length; s++) {
				// Check if current message matches the filter.
				if (messages[i].category == filter[s]) {
					filtered.push(messages[i]);
				}
			}
		}

		// Return array.
		return filtered;
	}

	/**
	 * Creates a HTML string for the Google marker content.
	 * @param  {Object} message
	 * @return {HTML}
	 */
	getContentHTML = function(message) {
		// Format a date string.
		var date = moment(message.createdate);

		// Category literal from id.
		var category = ['Vägtrafik', 'Kollektivtrafik', 'Planerad störning', 'Övrigt']

		// Return a formatted HTML string containing
		// information about the traffic message.
		return '<div class="content">'
			+ '<h3>' + message.title + '</h3>'
			+ '<p><strong>' + date.locale('sv').format('llll') + '</strong></p>'
			+ '<p>' + message.description
			+ '<p><em>' + category[message.category] + '</em></p>'
		+ '</div>';
	}
};