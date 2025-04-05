<?php
require_once 'BaseService.php';
require_once __DIR__ . '/../dao/UserDao.php';

class UserService extends BaseService
{
    public function __construct()
    {
        parent::__construct(new UserDao());
    }

    public function add($entity){
        if (!isset($entity['email']) || !filter_var($entity['email'], FILTER_VALIDATE_EMAIL)) {
            Flight::halt(400, "Invalid or missing email.");
        }

        if (!isset($entity['password']) || strlen($entity['password']) < 6) {
            Flight::halt(400, "Password must be at least 6 characters.");
        }

        // Check for duplicate email
        $existing = $this->dao->get_by_email($entity['email']);
        if ($existing) {
            Flight::halt(409, "Email is already registered.");
        }

        $entity['password'] = password_hash($entity['password'], PASSWORD_DEFAULT);
        return parent::add($entity);
    }

}
