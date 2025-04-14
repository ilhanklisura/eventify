<?php

/**
 * @OA\Get(
 *     path="/categories",
 *     summary="Get all categories",
 *     tags={"Categories"},
 *     @OA\Response(response="200", description="List of categories")
 * )
 */
Flight::route('GET /categories', function () {
    Flight::json(Flight::category_service()->get_all());
});

/**
 * @OA\Get(
 *     path="/categories/{id}",
 *     summary="Get category by ID",
 *     tags={"Categories"},
 *     @OA\Parameter(@OA\Schema(type="integer"), name="id", in="path", required=true),
 *     @OA\Response(response="200", description="Category found")
 * )
 */
Flight::route('GET /categories/@id', function ($id) {
    Flight::json(Flight::category_service()->get_by_id($id));
});

/**
 * @OA\Post(
 *     path="/categories",
 *     summary="Create category",
 *     tags={"Categories"},
 *     @OA\RequestBody(@OA\JsonContent(
 *         required={"name"},
 *         @OA\Property(property="name", type="string")
 *     )),
 *     @OA\Response(response="200", description="Category added")
 * )
 */
Flight::route('POST /categories', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Category added successfully",
        "data" => Flight::category_service()->add($data)
    ]);
});

/**
 * @OA\Put(
 *     path="/categories/{id}",
 *     summary="Update category",
 *     tags={"Categories"},
 *     @OA\Parameter(@OA\Schema(type="integer"), name="id", in="path", required=true),
 *     @OA\RequestBody(@OA\JsonContent(
 *         @OA\Property(property="name", type="string")
 *     )),
 *     @OA\Response(response="200", description="Category updated")
 * )
 */
Flight::route('PUT /categories/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Category updated successfully",
        "data" => Flight::category_service()->update($data, $id)
    ]);
});

/**
 * @OA\Delete(
 *     path="/categories/{id}",
 *     summary="Delete category",
 *     tags={"Categories"},
 *     @OA\Parameter(@OA\Schema(type="integer"), name="id", in="path", required=true),
 *     @OA\Response(response="200", description="Category deleted")
 * )
 */
Flight::route('DELETE /categories/@id', function ($id) {
    Flight::category_service()->delete($id);
    Flight::json(["message" => "Category deleted successfully"]);
});