<?php

namespace model;

require_once('./model/Producer.php');

class Scraper {
	/**
	 * These are the user credentials needed to
	 * log in at our source site.
	 */
	private static $username = 'admin';
	private static $password = 'admin';

	/**
	 * These are the URLs we need.
	 */
	private static $loginUrl = 'http://vhost3.lnu.se:20080/~1dv449/scrape/check.php';
	private static $producersUrl = 'http://vhost3.lnu.se:20080/~1dv449/scrape/secure/producenter.php';
	private static $baseUrl = 'http://vhost3.lnu.se:20080/~1dv449/scrape/secure/';

	/**
	 * cURL handle used to scrape our
	 * source website.
	 * @var cURL handle
	 */
	private $ch;

	/**
	 * Private helper method for setting up
	 * a cURL object for this instance.
	 * @return void
	 */
	private function setupCURL() {
		$this->ch = curl_init();
	}

	/**
	 * Private helper method for logging in
	 * to our source page.
	 * @return void
	 */
	private function login() {
		curl_setopt($this->ch, CURLOPT_URL, self::$loginUrl);
		curl_setopt($this->ch, CURLOPT_POST, 1);
		curl_setopt($this->ch, CURLOPT_POSTFIELDS, 'username=' . self::$username . '&password=' . self::$password);
		curl_setopt($this->ch, CURLOPT_COOKIEJAR, 'cookie.txt');
		curl_setopt($this->ch, CURLOPT_RETURNTRANSFER, 1);
		curl_exec($this->ch);
	}

	/**
	 * Private helper method that searches the
	 * source page for every producer we can find
	 * and gets the link to each and everyone's
	 * seperate page.
	 * @return string[]
	 */
	private function getProducerLinks() {
		//Set URL to the producers page.
		curl_setopt($this->ch, CURLOPT_URL, self::$producersUrl);

		//Get the HTML content.
		$html = curl_exec($this->ch);

		//Parse into DOMDocument.
		$dom = new \DOMDocument();
		$dom->loadHTML($html);

		//Get all producerlinks.
		$xpath = new \DOMXpath($dom);
		$hrefs = $xpath->query('//tr/td/a');

		//Build a string array with
		//all the links in it and
		//return it.
		$ret = array();
		foreach($hrefs as $href) $ret[] = self::$baseUrl . $href->getAttribute('href');
		return $ret;
	}

	/**
	 * Private helper method for reading a producers
	 * page from our source and create a Producer object.
	 * @param  string $href
	 * @return \model\Producer
	 */
	private function getProducerObject($href) {
		//Change cURL url.
		curl_setopt($this->ch, CURLOPT_URL, $href);

		//Get the content of that page.
		$pageContent = curl_exec($this->ch);

		//Make sure the page exists.
		$httpCode = curl_getinfo($this->ch, CURLINFO_HTTP_CODE);
		if($httpCode != 404) {
			//Parse into DOMDocument.
			libxml_use_internal_errors(true);
			$dom = new \DOMDocument();
			$dom->loadHTML($pageContent);

			//Get a xpath object.
			$xpath = new \DOMXpath($dom);

			//Get the ID.
			$matches = array();
			preg_match('/producent_(.*)\./', $href, $matches);
			$id = $matches[1];

			//Get the name.
			$name = $xpath->query('//h1')->item(0)->nodeValue;

			//Get the url.
			$query = $xpath->query('//p/a');
			$url = ($query->length > 0) ? $query->item(0)->getAttribute('href') : "";

			//Get the city.
			preg_match('/Ort: (.*)</', $pageContent, $matches);
			$city = $matches[1];

			return new \model\Producer($id,
									   $name,
									   $url,
									   $city);
		}
	}

	/**
	 * Publicly available method for going
	 * to our source website and retrieve
	 * every producer currently on it and
	 * return them as a DOMnodes.
	 * @return \model\Producer[]
	 */
	public function getProducers() {
		$producers = array();

		//Get the links to every seperate
		//producers page.
		$producerPages = $this->getProducerLinks();

		//Loop through all the individual pages
		//and get a Producer object.
		foreach($producerPages as $href) {
			$producer = $this->getProducerObject($href);
			
			if($producer) $producers[] = $producer;
		}
		
		//Return the array.
		return $producers;
	}

	/**
	 * When we instanciate a Scraper we
	 * want to initiate a cURL object and
	 * perform a login.
	 */
	public function __construct() {
		//Set up cURL.
		$this->setupCURL();

		//Login.
		$this->login();
	}

	/**
	 * On destruct, we make sure the
	 * cURL handle is closed.
	 */
	public function __destruct() {
		curl_close($this->ch);
	}
}