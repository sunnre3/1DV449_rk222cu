<?php

/**
* Called from AJAX to add stuff to DB
*/
function addToDB($name, $message, $pid) {
	$db = null;
	
	try {
		$db = new PDO("sqlite:db.db");
		$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	}
	catch(PDOEception $e) {
		die("Something went wrong -> " .$e->getMessage());
	}

	$name = htmlentities($name);
	$message = htmlentities($message);

	$stmt = $db->prepare("INSERT INTO messages (message, name, pid) VALUES (:message, :name, :pid)");
	$vars = array(
		'message' => $message,
		'name' => $name,
		'pid' => $pid
		);
	
	try {
		if(!$stmt->execute($vars)) {
			die("Fel vid insert");
		}
	}
	catch(PDOException $e) {
		die("Something went wrong -> " .$e->getMessage());
	}
}
