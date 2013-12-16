$(document).ready(function() {
	$('a[data-action="changeProducer"]').on('click', function() {
		var pid = $(this).data('id');
		//console.log("pid --> " +pid);
						
		// Clear and update the hidden stuff
		$( "#mess_inputs").val(pid);
		$( "#mess_p_mess").text("");
		
		// get all the stuff for the producers
		// make ajax call to functions.php with teh data
		$.ajax({
			type: "GET",
		  	url: "functions.php",
		  	data: {function: "producers", pid: pid}
		}).done(function(data) { // called when the AJAX call is ready
			var j = JSON.parse(data);
			
			$("#mess_p_headline").text("Meddelande till " +j.name +", " +j.city);
			
			
			if(j.url !== "") {
				
				$("#mess_p_kontakt").text("Länk till deras hemsida " +j.url);
			}
			else {
				$("#mess_p_kontakt").text("Producenten har ingen webbsida");
			}
			
			if(j.imageURL !== "") {
				$("#p_img_link").attr("href", j.imageURL); 
				$("#p_img").attr("src", j.imageURL); 
			}
			else {
				$("#p_img_link").attr("href", "#"); 
				$("#p_img").attr("src", "img/noimg.jpg"); 
			}
		});
		
		// Get all the messages for the producers through functions.php
		$.ajax({
			type: "GET",
		  	url: "functions.php",
		  	data: {function: "getMessages", pid: pid}
			
		}).done(function(data) {
			if(data !== 'false') {
				//Send the JSON string to the function
				//that renders them.
				renderMessages(data);
			}	
		});
		
		// show the div if its unvisible
		$("#mess_container").show("slow");
	});
	// Called when we click on a producer link - gets the id for the producer 
	
	$("#logout").bind( "click", function() {
		/*
		 * window.location = "index.php";
		 * Ingen riktig utloggning?
		 */
		window.location.href = "functions.php?function=logout";
	});

	$("#add_btn").bind( "click", function() {
		var name_val = $('#name_txt').val();
		var message_val = $('#message_ta').val();
		var pid =  $('#mess_inputs').val();
		// make ajax call to logout
		$.ajax({
			type: "GET",
		  	url: "functions.php",
		  	data: {function: "add", name: name_val, message: message_val, pid: pid}
		}).done(function(data) {
			//When the user has posted a comment,
			//retrieve all the comments again and render them.
			$.ajax({
				type: "GET",
				url: "functions.php",
				data: {function: "getMessages", pid: pid}
			
			}).done(function(data) {
				//Check if there are any messages.
				if(data !== 'false') {
					//Send the JSON string to the function
					//that renders them.
					renderMessages(data);
				}	
			});
		});
	});

	//Function that takes a JSON string with messages in it
	//and renders them.
	function renderMessages(data) {
		var messages = JSON.parse(data);
		messages.reverse();

		console.log(messages);

		$('#mess_p_mess').empty();

		for(var i = 0; i < messages.length; i++) {
			$('#mess_p_mess').append( "<p class='message_container'>" +messages[i].message +"<br />Skrivet av: " +messages[i].name +"</p>");
		}
	}
});