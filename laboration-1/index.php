<?php

ini_set('display_errors', 'On');
error_reporting(E_ALL);

require_once('./controller/App.php');

/**
 * FRONT-END CONTROLLER.
 * INITIATES AN APPCONTROLLER,
 * PERFORMS THE METHOD RUN
 * WHICH IN RETURN RETURNS A
 * HTML STRING THAT WE CAN
 * ECHO OUT.
 */

$appController = new \controller\App();
echo $appController->run();