<?php

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL ^ (E_NOTICE | E_DEPRECATED));

class Config
{
    public static function DB_NAME() {
        return Config::get_env("DB_NAME", "eventify");
    }

    public static function DB_PORT() {
        return Config::get_env("DB_PORT", 3306);
    }

    public static function DB_USER() {
        return Config::get_env("DB_USER", 'root');
    }

    public static function DB_PASSWORD() {
        return Config::get_env("DB_PASSWORD", '12345678');
    }

    public static function DB_HOST() {
        return Config::get_env("DB_HOST", '127.0.0.1');
    }

    public static function JWT_SECRET() {
        return Config::get_env("JWT_SECRET", "653a7e33c68f046b0ef8ffca0493b89abc3ba68fc0c11842e3822c0510297189");
    }

    public static function get_env($name, $default){
        $value = getenv($name);
        return ($value !== false && trim($value) !== '') ? $value : $default;
    }
}