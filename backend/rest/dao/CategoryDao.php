<?php 
require_once __DIR__ . '/BaseDao.php';

class CategoryDao extends BaseDao {
    public function __construct() {
        parent::__construct("categories");
    }

    public function get_all() {
        return $this->query("SELECT * FROM categories", []);
    }

    public function get_by_id($id) {
        return $this->query_unique("SELECT * FROM categories WHERE id = :id", ["id" => $id]);
    }

    public function get_by_name($name) {
        return $this->query_unique("SELECT * FROM categories WHERE name = :name", ["name" => $name]);
    }
}