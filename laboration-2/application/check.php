<?php
require_once("sec.php");

// check tha POST parameters
$u = $_POST['username'];
$p = $_POST['password'];

// Check if user is OK
if(isUser($u, $p)) {
	// set the session
	sec_session_start();

	$loginInfo = new StdClass;
	$loginInfo->ip = $_SERVER['REMOTE_ADDR'];
	$loginInfo->userAgent = $_SERVER['HTTP_USER_AGENT'];

	$_SESSION['loginInfo'] = $loginInfo;
	$_SESSION['user'] = $u;

	header("Location: mess.php");
}
else {
	// To bad
	header('HTTP/1.1 401 Unauthorized');
}
