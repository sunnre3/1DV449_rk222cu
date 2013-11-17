<?php

namespace controller;

require_once('./model/Scraper.php');
require_once('./view/Producer.php');
require_once('./view/HTMLPage.php');

class App {
	/**
	 * Method for running our application.
	 * It will fetch the data from our source
	 * and return the HTML to display it
	 * on the front-end.
	 * @return string HTML
	 */
	public function run() {
		//Get the producers.
		$model = new \model\Scraper();
		$producers = $model->getProducers();

		//Get the HTML from the
		//view.
		$view = new \view\Producer();
		$content = $view->getProducersHTML($producers);

		//Return the entire page.
		$htmlPage = new \view\HTMLPage();
		return $htmlPage->getPage('Lista av producenter', $content);
	}
}