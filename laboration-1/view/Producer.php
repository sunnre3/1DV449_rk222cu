<?php

namespace view;

require_once('./model/ProducerDAL.php');

class Producer {
	/**
	 * The producer DAL is used to fetch all
	 * the URLs in our database that are broken
	 * so we can display them to the user.
	 * @var \model\ProducerDAL
	 */
	private $producerDAL;

	/**
	 * Initiates objects.
	 */
	public function __construct() {
		$this->producerDAL = new \model\ProducerDAL();
	}

	/**
	 * Method that takes an array of
	 * Producers and returns a HTML
	 * string containing all information
	 * about them.
	 * @param  \model\Producer[] $producers
	 * @return string HTML
	 */
	public function getProducersHTML($producers) {
		$html = '
			<div id="producers" class="clearfix">';

		if(empty($producers)) {
			$html .= utf8_encode('
			<div class="producer empty">
				<p>Inga producenter hittades i vår databas.</p>
			</div>
			');
		}

		else {
			//Add each producer which all information that
			//the object has by looping through the array.
			foreach($producers as $producer) {
				$url = (empty($producer->url) || $producer->url == '#') ? 'Hemsida saknas' : '<a href="' . $producer->url . '">' . $producer->url . '</a>';

				$html .= '

					<div id="producer-' . $producer->id . '" class="producer">
						<div class="name">
							' . $producer->name . '
						</div>

						<div class="city">
							' . utf8_encode($producer->city) . '
						</div>

						<div class="url">
							' . $url . '
						</div>
					</div>';
			}

		}

		$html .= '
				</div>';

		//Show the user which links in our source
		//that is broken.
		$brokenLinks = $this->producerDAL->getBrokenLinks();

		//Add the HTML.
		$html .= utf8_encode('
				<div id="broken-links">
					<p>Följande länkar kunde inte hämtas:</p>
					<ol>');
		foreach($brokenLinks as $link) {
			$html .= '
						<li class="broken-link"><a href="' . $link . '">' . $link . '</a>';
		}
		$html .= '
				</ol>
			</div>';

		return utf8_decode($html);
	}
}