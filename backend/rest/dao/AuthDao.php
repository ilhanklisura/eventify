<?php
require_once __DIR__ . '/UserDao.php';

class AuthDao extends UserDao {
    public function get_user_by_email($email) {
        return $this->get_by_email($email);
    }
}
