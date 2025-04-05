<?php

Flight::route('GET /bookings', function () {
    Flight::json(Flight::booking_service()->get_all());
});

Flight::route('GET /bookings/@id', function ($id) {
    Flight::json(Flight::booking_service()->get_by_id($id));
});

Flight::route('POST /bookings', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Booking created successfully",
        "data" => Flight::booking_service()->add($data)
    ]);
});

Flight::route('PUT /bookings/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Booking updated successfully",
        "data" => Flight::booking_service()->update($data, $id)
    ]);
});

Flight::route('DELETE /bookings/@id', function ($id) {
    Flight::booking_service()->delete($id);
    Flight::json(["message" => "Booking deleted successfully"]);
});
