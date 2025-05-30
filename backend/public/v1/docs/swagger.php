<?php

require __DIR__ . '/../../../vendor/autoload.php';

define('LOCALSERVER', 'http://localhost/eventify/backend/');
define('PRODSERVER', 'https://squid-app-lnxkv.ondigitalocean.app/backend/');

if($_SERVER['SERVER_NAME'] == 'localhost' || $_SERVER['SERVER_NAME'] == '127.0.0.1'){
    define('BASE_URL', 'http://localhost/eventify/backend/');
} else {
    define('BASE_URL', 'https://squid-app-lnxkv.ondigitalocean.app/backend/');
}

$openapi = \OpenApi\Generator::scan([
    __DIR__ . '/doc_setup.php',
    __DIR__ . '/../../../rest/routes'
]);
header('Content-Type: application/json');
echo $openapi->toJson();
?>