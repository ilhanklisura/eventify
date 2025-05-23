<?php
use Firebase\JWT\JWT;
use Firebase\JWT\Key;

class AuthMiddleware {
    public function verifyToken($token) {
        if (!$token) {
            Flight::halt(401, 'Authentication token is missing.');
        }

        try {
            $decoded = JWT::decode($token, new Key(Config::JWT_SECRET(), 'HS256'));
            Flight::set('user', $decoded->user);
            return true;
        } catch (Exception $e) {
            Flight::halt(401, 'Invalid token: ' . $e->getMessage());
        }
    }

    public function authorizeRole($role) {
        $user = Flight::get('user');
        if ($user->role !== $role) {
            Flight::halt(403, 'Access denied: role required - ' . $role);
        }
    }

    public function authorizeRoles($roles) {
        $user = Flight::get('user');
        if (!in_array($user->role, $roles)) {
            Flight::halt(403, 'Access denied: insufficient role');
        }
    }
}
