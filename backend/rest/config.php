<?php

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL ^ (E_NOTICE | E_DEPRECATED));

class Config
{
    public static function DB_NAME() {
        return 'eventify';
    }

    public static function DB_PORT() {
        return 3306;
    }

    public static function DB_USER() {
        return 'root';
    }

    public static function DB_PASSWORD() {
        return '12345678';
    }

    public static function DB_HOST() {
        return '127.0.0.1';
    }
}
