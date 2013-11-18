<?php

namespace controller;

require_once('./model/Scraper.php');
require_once('./model/ProducerDAL.php');
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
		//First check if the user wants to
		//update the data.
		if(isset($_GET['update'])) {
			//Scrape the source.
			$scraper = new \model\Scraper();
			$producers = $scraper->getProducers();

			//Save them to our database.
			$producerDAL = new \model\ProducerDAL();
			$producerDAL->update($producers);

			//Redirect.
			header('location: ./');
		}

		//Get the producers.
		$model = new \model\ProducerDAL();
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