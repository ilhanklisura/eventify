<?php

require './vendor/autoload.php';

// CORS headers
header("Access-Control-Allow-Origin: https://seahorse-app-jta9z.ondigitalocean.app");
header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS");
header("Access-Control-Allow-Headers: Content-Type, Authentication");

// Preflight OPTIONS request
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit();
}

// Middleware i dodatni servisi
require_once 'middleware/AuthMiddleware.php';
require_once 'rest/services/AuthService.php';

use Firebase\JWT\JWT;
use Firebase\JWT\Key;

// Glavni servisi
require_once 'rest/services/UserService.php';
require_once 'rest/services/CategoryService.php';
require_once 'rest/services/EventService.php';
require_once 'rest/services/TicketService.php';
require_once 'rest/services/VenueService.php';
require_once 'rest/services/BookingService.php';

// Registracija servisa
Flight::register('auth_service', 'AuthService');
Flight::register('auth_middleware', 'AuthMiddleware');
Flight::register('user_service', 'UserService');
Flight::register('category_service', 'CategoryService');
Flight::register('event_service', 'EventService');
Flight::register('ticket_service', 'TicketService');
Flight::register('venue_service', 'VenueService');
Flight::register('booking_service', 'BookingService');

// Globalna autentifikacija - izuzeci za javne rute
Flight::route('/*', function () {
    $public_routes = [
        '/auth/login',
        '/auth/register',
        '/docs',
        '/docs/',
        '/docs/index.html',
        '/docs/swagger.php',
        '/dashboard/stats'
    ];

    $request_url = Flight::request()->url;
    foreach ($public_routes as $route) {
        if (strpos($request_url, $route) === 0) {
            return true;
        }
    }

    $token = Flight::request()->getHeader("Authentication");
    if (!$token) {
        Flight::halt(401, "Missing token.");
    }

    try {
        Flight::auth_middleware()->verifyToken($token);
        return true;
    } catch (Exception $e) {
        Flight::halt(401, $e->getMessage());
    }
});


// REST rute
require_once 'rest/routes/AuthRoutes.php';
require_once 'rest/routes/UserRoutes.php';
require_once 'rest/routes/CategoryRoutes.php';
require_once 'rest/routes/EventRoutes.php';
require_once 'rest/routes/TicketRoutes.php';
require_once 'rest/routes/VenueRoutes.php';
require_once 'rest/routes/BookingRoutes.php';
require_once 'rest/routes/DashboardRoutes.php';

// Start aplikacije
Flight::start();
