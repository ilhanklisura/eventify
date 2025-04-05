<?php

Flight::route('GET /venues', function () {
    Flight::json(Flight::venue_service()->get_all());
});

Flight::route('GET /venues/@id', function ($id) {
    Flight::json(Flight::venue_service()->get_by_id($id));
});

Flight::route('POST /venues', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Venue added successfully",
        "data" => Flight::venue_service()->add($data)
    ]);
});

Flight::route('PUT /venues/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Venue updated successfully",
        "data" => Flight::venue_service()->update($data, $id)
    ]);
});

Flight::route('DELETE /venues/@id', function ($id) {
    Flight::venue_service()->delete($id);
    Flight::json(["message" => "Venue deleted successfully"]);
});
