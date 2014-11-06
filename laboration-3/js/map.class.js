function Map(canvas) {
	/**
	 * Info window provided by the Google Map.
	 * @type {Google InfoWindow}
	 */
	this.infoWindow = null;

	/**
	 * Reference to the Google map.
	 * @type {Google Map}
	 */
	this.googleMap;

	/**
	 * Array containing all the markers on the map.
	 * @type {Array}
	 */
	this.markers = [];

	/**
	 * Canvas for the map.
	 * @type {HTML node}
	 */
	this.canvas = canvas;

	/**
	 * JS fix.
	 * @type {Map}
	 */
	var that = this;

	/**
	 * Creates a Google map centered on Sweden.
	 * @return {Void}
	 */
	this.createMap = function() {
		var options = {
			zoom: 5,
			center: new google.maps.LatLng(62, 15),
			mapTypeId: google.maps.MapTypeId.ROADMAP
		};

		// Create a Google Map.
		this.googleMap = new google.maps.Map(this.canvas, options);

		// Info Window.
		this.infoWindow = new google.maps.InfoWindow();
	};

	/**
	 * Creates a Google Maps Marker and sets it on
	 * the map.
	 * @param {Float} lat
	 * @param {Float} lng
	 * @param {String} content
	 */
	this.setMarker = function(lat, lng, content) {
		// LatLng object from Google.
		var latLng = new google.maps.LatLng(lat, lng);

		// Create a Marker object.
		var marker = new google.maps.Marker({
			position: latLng,
			map: this.googleMap
		});

		// Add the new marker to the array.
		this.markers.push(marker);

		// Add an event listener to the marker.
		google.maps.event.addListener(marker, 'click', function () {
			that.infoWindow.setOptions({ content: content });
			that.infoWindow.open(this.map, marker);
		});
	};

	/**
	 * Deletes all markers from the map.
	 * @return {Void}
	 */
	this.deleteMarkers = function() {
		for (var i = 0; i < this.markers.length; i++) {
			this.markers[i].setMap(null);
		}
	};
};