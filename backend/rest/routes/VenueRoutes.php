<?php

/**
 * @OA\Get(
 *     path="/venues",
 *     summary="Get all venues",
 *     tags={"Venues"},
 *     @OA\Response(response=200, description="List of venues")
 * )
 */
Flight::route('GET /venues', function () {
    Flight::json(Flight::venue_service()->get_all());
});

/**
 * @OA\Get(
 *     path="/venues/{id}",
 *     summary="Get venue by ID",
 *     tags={"Venues"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="Venue found")
 * )
 */
Flight::route('GET /venues/@id', function ($id) {
    Flight::json(Flight::venue_service()->get_by_id($id));
});

/**
 * @OA\Post(
 *     path="/venues",
 *     summary="Create new venue",
 *     tags={"Venues"},
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             required={"name", "location"},
 *             @OA\Property(property="name", type="string"),
 *             @OA\Property(property="location", type="string")
 *         )
 *     ),
 *     @OA\Response(response=200, description="Venue created")
 * )
 */
Flight::route('POST /venues', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Venue added successfully",
        "data" => Flight::venue_service()->add($data)
    ]);
});

/**
 * @OA\Put(
 *     path="/venues/{id}",
 *     summary="Update venue",
 *     tags={"Venues"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             @OA\Property(property="name", type="string"),
 *             @OA\Property(property="location", type="string")
 *         )
 *     ),
 *     @OA\Response(response=200, description="Venue updated")
 * )
 */
Flight::route('PUT /venues/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Venue updated successfully",
        "data" => Flight::venue_service()->update($data, $id)
    ]);
});

/**
 * @OA\Delete(
 *     path="/venues/{id}",
 *     summary="Delete venue",
 *     tags={"Venues"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="Venue deleted")
 * )
 */
Flight::route('DELETE /venues/@id', function ($id) {
    Flight::venue_service()->delete($id);
    Flight::json(["message" => "Venue deleted successfully"]);
});
