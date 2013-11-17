<?php

namespace view;

class Producer {
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
			<div id="producers">';

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

		$html .= '
			</div>';

		return utf8_decode($html);
	}
}