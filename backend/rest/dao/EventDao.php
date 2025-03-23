<?php
require_once __DIR__ . '/BaseDao.php';

class EventDao extends BaseDao {
    public function __construct() {
        parent::__construct("events");
    }

    public function get_all() {
        return $this->query("SELECT * FROM events", []);
    }

    public function get_by_id($id) {
        return $this->query_unique("SELECT * FROM events WHERE id = :id", ["id" => $id]);
    }
}