<?php
require_once 'BaseService.php';
require_once __DIR__ . '/../dao/UserDao.php';
use Firebase\JWT\JWT;

class AuthService extends BaseService {
    private $user_dao;

    public function __construct() {
        $this->user_dao = new UserDao();
        parent::__construct($this->user_dao);
    }

    public function register($data) {
        if (!isset($data['email']) || !filter_var($data['email'], FILTER_VALIDATE_EMAIL)) {
            return ['success' => false, 'error' => 'Valid email is required'];
        }
        if (!isset($data['password']) || strlen($data['password']) < 6) {
            return ['success' => false, 'error' => 'Password must be at least 6 characters'];
        }
        if ($this->user_dao->get_by_email($data['email'])) {
            return ['success' => false, 'error' => 'Email already exists'];
        }

        $data['password'] = password_hash($data['password'], PASSWORD_BCRYPT);
        $data['role'] = 'attendee'; // default role
        $user = parent::add($data);
        unset($user['password']);

        return ['success' => true, 'data' => $user];
    }

    public function login($data) {
        $user = $this->user_dao->get_by_email($data['email']);

        if (!$user || !password_verify($data['password'], $user['password'])) {
            return ['success' => false, 'error' => 'Invalid email or password'];
        }

        unset($user['password']);

        $token = JWT::encode([
            'user' => $user,
            'iat' => time(),
            'exp' => time() + (60 * 60 * 24) // valid for 24h
        ], Config::JWT_SECRET(), 'HS256');

        return ['success' => true, 'data' => array_merge($user, ['token' => $token])];
    }
}
