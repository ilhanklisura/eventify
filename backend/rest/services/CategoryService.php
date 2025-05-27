<?php
require_once 'BaseService.php';
require_once __DIR__ . '/../dao/CategoryDao.php';

class CategoryService extends BaseService {
    public function __construct() {
        parent::__construct(new CategoryDao());
    }

    public function add($entity) {
        if (!isset($entity['name']) || trim($entity['name']) === '') {
            Flight::halt(400, "Category name is required.");
        }

        $existing = $this->dao->get_by_name($entity['name']);
        if ($existing) {
            Flight::halt(409, "Category already exists.");
        }

        return parent::add($entity);
    }

    public function get_all() {
        return $this->dao->get_all();
    }

}
