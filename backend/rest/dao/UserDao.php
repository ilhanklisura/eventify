<?php
require_once __DIR__ . '/BaseDao.php';

class UserDao extends BaseDao {
    public function __construct() {
        parent::__construct("users");
    }

    public function get_all() {
        return $this->query("SELECT * FROM users", []);
    }

    public function get_by_id($id) {
        return $this->query_unique("SELECT * FROM users WHERE id = :id", ["id" => $id]);
    }

    public function get_by_email($email) {
        return $this->query_unique("SELECT * FROM users WHERE email = :email", ["email" => $email]);
    }
}