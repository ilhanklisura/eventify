<?php

/**
 * @OA\Get(
 *     path="/bookings",
 *     summary="Get all bookings",
 *     tags={"Bookings"},
 *     @OA\Response(response="200", description="List of bookings")
 * )
 */
Flight::route('GET /bookings', function () {
    Flight::json(Flight::booking_service()->get_all());
});

/**
 * @OA\Get(
 *     path="/bookings/{id}",
 *     summary="Get booking by ID",
 *     tags={"Bookings"},
 *     @OA\Parameter(@OA\Schema(type="integer"), name="id", in="path", required=true),
 *     @OA\Response(response="200", description="Booking found")
 * )
 */
Flight::route('GET /bookings/@id', function ($id) {
    Flight::json(Flight::booking_service()->get_by_id($id));
});

/**
 * @OA\Post(
 *     path="/bookings",
 *     summary="Create new booking",
 *     tags={"Bookings"},
 *     @OA\RequestBody(@OA\JsonContent(
 *         required={"user_id","event_id"},
 *         @OA\Property(property="user_id", type="integer"),
 *         @OA\Property(property="event_id", type="integer")
 *     )),
 *     @OA\Response(response="200", description="Booking created")
 * )
 */
Flight::route('POST /bookings', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Booking created successfully",
        "data" => Flight::booking_service()->add($data)
    ]);
});

/**
 * @OA\Put(
 *     path="/bookings/{id}",
 *     summary="Update booking",
 *     tags={"Bookings"},
 *     @OA\Parameter(@OA\Schema(type="integer"), name="id", in="path", required=true),
 *     @OA\RequestBody(@OA\JsonContent(
 *         @OA\Property(property="user_id", type="integer"),
 *         @OA\Property(property="event_id", type="integer")
 *     )),
 *     @OA\Response(response="200", description="Booking updated")
 * )
 */
Flight::route('PUT /bookings/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Booking updated successfully",
        "data" => Flight::booking_service()->update($data, $id)
    ]);
});

/**
 * @OA\Delete(
 *     path="/bookings/{id}",
 *     summary="Delete booking",
 *     tags={"Bookings"},
 *     @OA\Parameter(@OA\Schema(type="integer"), name="id", in="path", required=true),
 *     @OA\Response(response="200", description="Booking deleted")
 * )
 */
Flight::route('DELETE /bookings/@id', function ($id) {
    Flight::booking_service()->delete($id);
    Flight::json(["message" => "Booking deleted successfully"]);
});