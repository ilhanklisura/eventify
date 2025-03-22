<?php

require __DIR__ . '/vendor/autoload.php';

Flight::route('/', function(){
    echo "Hello from FlightPHP!";
});

Flight::start();
