<?php

Flight::route('GET /events', function () {
    Flight::json(Flight::event_service()->get_all());
});

Flight::route('GET /events/@id', function ($id) {
    Flight::json(Flight::event_service()->get_by_id($id));
});

Flight::route('POST /events', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Event added successfully",
        "data" => Flight::event_service()->add($data)
    ]);
});

Flight::route('PUT /events/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Event updated successfully",
        "data" => Flight::event_service()->update($data, $id)
    ]);
});

Flight::route('DELETE /events/@id', function ($id) {
    Flight::event_service()->delete($id);
    Flight::json(["message" => "Event deleted successfully"]);
});
