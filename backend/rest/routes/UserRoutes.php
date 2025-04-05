<?php

Flight::route('GET /users', function () {
    Flight::json(Flight::user_service()->get_all());
});

Flight::route('GET /users/@id', function ($id) {
    Flight::json(Flight::user_service()->get_by_id($id));
});

Flight::route('POST /users', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        'message' => 'User created successfully',
        'data' => Flight::user_service()->add($data)
    ]);
});

Flight::route('PUT /users/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        'message' => 'User updated successfully',
        'data' => Flight::user_service()->update($data, $id)
    ]);
});

Flight::route('DELETE /users/@id', function ($id) {
    Flight::user_service()->delete($id);
    Flight::json(['message' => 'User deleted successfully']);
});
