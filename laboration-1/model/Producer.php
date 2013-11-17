<?php

namespace model;

class Producer {
	/**
	 * @var int
	 */
	public $id;

	/**
	 * @var string
	 */
	public $name;

	/**
	 * @var string
	 */
	public $url;

	/**
	 * @var string
	 */
	public $city;

	/**
	 * Instanciate a Producer object.
	 * @param int $id
	 * @param string $name
	 * @param string $url
	 * @param string $city
	 */
	public function __construct($id, $name, $url = "", $city) {
		$this->id = $id;
		$this->name = $name;
		$this->url = $url;
		$this->city = $city;
	}
}