<?php
require_once __DIR__ . '/BaseDao.php';

class BookingsDao extends BaseDao {
    public function __construct() {
        parent::__construct("bookings");
    }

    public function get_all() {
        return $this->query("SELECT * FROM bookings", []);
    }

    public function get_by_id($id) {
        return $this->query_unique("SELECT * FROM bookings WHERE id = :id", ["id" => $id]);
    }
}