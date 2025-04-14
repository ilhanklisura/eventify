<?php
require_once __DIR__ . '/BaseDao.php';

class VenueDao extends BaseDao {
    public function __construct() {
        parent::__construct("venues");
    }

    public function get_all() {
        return $this->query("SELECT * FROM venues", []);
    }

    public function get_by_id($id) {
        return $this->query_unique("SELECT * FROM venues WHERE id = :id", ["id" => $id]);
    }

    public function get_by_name_and_location($name, $location) {
        return $this->query_unique(
            "SELECT * FROM venues WHERE name = :name AND location = :location",
            ['name' => $name, 'location' => $location]
        );
    }
}