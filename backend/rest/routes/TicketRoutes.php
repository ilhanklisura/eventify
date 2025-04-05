<?php

Flight::route('GET /tickets', function () {
    Flight::json(Flight::ticket_service()->get_all());
});

Flight::route('GET /tickets/@id', function ($id) {
    Flight::json(Flight::ticket_service()->get_by_id($id));
});

Flight::route('POST /tickets', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Ticket added successfully",
        "data" => Flight::ticket_service()->add($data)
    ]);
});

Flight::route('PUT /tickets/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Ticket updated successfully",
        "data" => Flight::ticket_service()->update($data, $id)
    ]);
});

Flight::route('DELETE /tickets/@id', function ($id) {
    Flight::ticket_service()->delete($id);
    Flight::json(["message" => "Ticket deleted successfully"]);
});
