<?php

namespace model;

require_once('./model/Database.php');

class ProducerDAL extends Database {
	/**
	 * Makes sure that we are set up by
	 * running our parent's constructor.
	 */
	public function __construct() {
		parent::__construct();
	}

	/**
	 * Returns all entries in our database
	 * that are considered broken.
	 * @return string[]
	 */
	public function getBrokenLinks() {
		$brokenLinks = array();

		//Get the rows from our database.
		$query = 'SELECT * FROM ' . parent::$DEADLINKS_TABLE;
		$result = $this->query($query);

		//Add the links to the array.
		foreach($result as $link) {
			$brokenLinks[] = $link['url'];
		}

		return $brokenLinks;
	}

	/**
	 * Returns all producers found in our database
	 * as an array of Producer objects.
	 * @return \model\Producer[]
	 */
	public function getProducers() {
		//Create an array which we can
		//return when we are done.
		$producers = array();

		//Retrieve from database.
		$query = 'SELECT * FROM ' . parent::$PRODUCER_TABLE;
		$result = $this->query($query);

		//Create Producer objects.
		foreach($result as $producer) {
			$producers[] = new Producer($producer['id'],
										$producer['name'],
										$producer['url'],
										$producer['city']);
		}

		//Return array.
		return $producers;
	}

	/**
	 * Takes an array of producers as an
	 * argument and saves them to our database.
	 * @param  \model\Producer[] $producers
	 * @return void
	 */
	public function update($producers) {
		//Remove all existing data.
		$query = 'DELETE FROM ' . parent::$PRODUCER_TABLE;
		$this->query($query);

		$query = 'DELETE FROM ' . parent::$DEADLINKS_TABLE;
		$this->query($query);

		//Add the new data.
		foreach($producers as $producer) {
			//If the producer doesn't have a name
			//the url was dead and we should save it
			//to a seperate table.
			if(empty($producer->name)) {
				$query = "INSERT INTO " . parent::$DEADLINKS_TABLE . " (url) 
				VALUES('{$producer->url}')";
				$this->query($query);
			}

			//If it is a valid object proceed and save it
			//to our normal database.
			else {
				$query = "INSERT INTO " . parent::$PRODUCER_TABLE . " (id, name, url, city) 
				VALUES({$producer->id}, '{$producer->name}', '{$producer->url}', '{$producer->city}')";
				$this->query($query);
			}
		}
	}
}