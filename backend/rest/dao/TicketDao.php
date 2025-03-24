<?php
require_once __DIR__ . '/BaseDao.php';

class TicketDao extends BaseDao {
    public function __construct() {
        parent::__construct("tickets");
    }

    public function get_all() {
        return $this->query("SELECT * FROM tickets", []);
    }

    public function get_by_id($id) {
        return $this->query_unique("SELECT * FROM tickets WHERE id = :id", ["id" => $id]);
    }
}