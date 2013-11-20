<?php

namespace model;

abstract class Database {
	/**
	 * Database name
	 * @var string
	 */
	private static $dbPath = './data/ProducerDB.sqlite';

	/**
	 * Static representation of the
	 * table name for Producer.
	 * @var string
	 */
	protected static $PRODUCER_TABLE = 'Producer';
	
	/**
	 * Static representation of the
	 * table used for storing dead links.
	 * @var string
	 */
	protected static $DEADLINKS_TABLE = 'DeadLinks';

	/**
	 * Private representation of a database
	 * connection. Cannot be directly used by
	 * children.
	 * @var \PDO
	 */
	private $dbConn;

	private function initDB() {
		try {
			$this->dbConn = new \PDO('sqlite:' . self::$dbPath);
			$this->dbConn->setAttribute(\PDO::ATTR_ERRMODE, \PDO::ERRMODE_EXCEPTION);
		}

		catch(\PDOException $e) {
			die('Ett fel uppstod --> ' . $e->getMessage());
		}
	}

	/**
	 * Private function to create our table.
	 * @return void
	 */
	private function createDB() {
		//Producer table
		$query = 'CREATE TABLE IF NOT EXISTS ' . self::$PRODUCER_TABLE . ' (
			id INTEGER PRIMARY KEY,
			name TEXT,
			url TEXT,
			city TEXT
			)';
		$this->query($query);
		
		//Dead links table
		$query = 'CREATE TABLE IF NOT EXISTS ' . self::$DEADLINKS_TABLE . ' (
			id INTEGER PRIMARY KEY AUTOINCREMENT,
			url TEXT
		)';
		$this->query($query);
	}

	/**
	 * Helper method for executing a query.
	 * If everything is ok we return a result set
	 * and if something went wrong we terminate.
	 * @param  string $query
	 * @return PDO result set
	 */
	protected function query($query) {
		try {
			$sth = $this->dbConn->prepare($query);
			if(!$sth->execute()) {
				die('Det gick inte att köra satsen.');
			}

			//Return result.
			return $sth->fetchAll();
		}

		catch(\PDOException $e) {
			throw $e;
			//die('Ett fel uppstod --> ' . $e->getMessage());
		}
	}

	/**
	 * Creates and/or initates our database connection
	 * and also makes sure our table is set up.
	 */
	public function __construct() {
		$this->initDB();
		$this->createDB();
	}
}