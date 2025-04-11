<?php

require './vendor/autoload.php';

// Import All Services
require_once 'rest/services/UserService.php';
require_once 'rest/services/CategoryService.php';
require_once 'rest/services/EventService.php';
require_once 'rest/services/TicketService.php';
require_once 'rest/services/VenueService.php';
require_once 'rest/services/BookingService.php';

// Register Services
Flight::register('user_service', 'UserService');
Flight::register('category_service', 'CategoryService');
Flight::register('event_service', 'EventService');
Flight::register('ticket_service', 'TicketService');
Flight::register('venue_service', 'VenueService');
Flight::register('booking_service', 'BookingService');

// Import Routes
require_once 'rest/routes/UserRoutes.php';
require_once 'rest/routes/CategoryRoutes.php';
require_once 'rest/routes/EventRoutes.php';
require_once 'rest/routes/TicketRoutes.php';
require_once 'rest/routes/VenueRoutes.php';
require_once 'rest/routes/BookingRoutes.php';

// Start FlightPHP
Flight::start();
