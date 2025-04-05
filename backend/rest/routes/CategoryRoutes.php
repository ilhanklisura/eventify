<?php

Flight::route('GET /categories', function () {
    Flight::json(Flight::category_service()->get_all());
});

Flight::route('GET /categories/@id', function ($id) {
    Flight::json(Flight::category_service()->get_by_id($id));
});

Flight::route('POST /categories', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Category added successfully",
        "data" => Flight::category_service()->add($data)
    ]);
});

Flight::route('PUT /categories/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Category updated successfully",
        "data" => Flight::category_service()->update($data, $id)
    ]);
});

Flight::route('DELETE /categories/@id', function ($id) {
    Flight::category_service()->delete($id);
    Flight::json(["message" => "Category deleted successfully"]);
});
