(function() {

	// Get the map canvas node.
	var canvas = $('#map')[0];

	// Initiate objects.
	var map = new Map(canvas);
	var sr = new SR(map);

	// Create a map.
	map.createMap();

	// Check all filters.
	$('input:checkbox').each(function() {
		$(this).prop('checked', true);
	})

	// Get all markers initially.
	sr.getMessages();

	// Listen for changes on the filters.
	$('input:checkbox').change(function() {
		// Create an array.
		var filters = [];

		// Loop through all checkboxes and
		// check if it's checked or not.
		$('input:checkbox').each(function() {
			if ($(this).is(':checked')) {
				filters.push($(this).attr('id'));
			}
		});

		console.log(filters);

		// Reload markers.
		sr.getMessages(filters);
	});

})();