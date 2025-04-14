<?php

/**
 * @OA\Get(
 *     path="/events",
 *     summary="Get all events",
 *     tags={"Events"},
 *     @OA\Response(response=200, description="List of events")
 * )
 */
Flight::route('GET /events', function () {
    Flight::json(Flight::event_service()->get_all());
});

/**
 * @OA\Get(
 *     path="/events/{id}",
 *     summary="Get event by ID",
 *     tags={"Events"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="Event found")
 * )
 */
Flight::route('GET /events/@id', function ($id) {
    Flight::json(Flight::event_service()->get_by_id($id));
});

/**
 * @OA\Post(
 *     path="/events",
 *     summary="Create new event",
 *     tags={"Events"},
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             required={"title", "description", "date", "category_id", "venue_id", "organizer_id"},
 *             @OA\Property(property="title", type="string"),
 *             @OA\Property(property="description", type="string"),
 *             @OA\Property(property="date", type="string", format="date-time"),
 *             @OA\Property(property="category_id", type="integer"),
 *             @OA\Property(property="venue_id", type="integer"),
 *             @OA\Property(property="organizer_id", type="integer")
 *         )
 *     ),
 *     @OA\Response(response=200, description="Event created")
 * )
 */
Flight::route('POST /events', function () {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Event added successfully",
        "data" => Flight::event_service()->add($data)
    ]);
});

/**
 * @OA\Put(
 *     path="/events/{id}",
 *     summary="Update event",
 *     tags={"Events"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\RequestBody(
 *         @OA\JsonContent(
 *             @OA\Property(property="title", type="string"),
 *             @OA\Property(property="description", type="string"),
 *             @OA\Property(property="date", type="string", format="date-time"),
 *             @OA\Property(property="category_id", type="integer"),
 *             @OA\Property(property="venue_id", type="integer"),
 *             @OA\Property(property="organizer_id", type="integer")
 *         )
 *     ),
 *     @OA\Response(response=200, description="Event updated")
 * )
 */
Flight::route('PUT /events/@id', function ($id) {
    $data = Flight::request()->data->getData();
    Flight::json([
        "message" => "Event updated successfully",
        "data" => Flight::event_service()->update($data, $id)
    ]);
});

/**
 * @OA\Delete(
 *     path="/events/{id}",
 *     summary="Delete event",
 *     tags={"Events"},
 *     @OA\Parameter(
 *         name="id",
 *         in="path",
 *         required=true,
 *         @OA\Schema(type="integer")
 *     ),
 *     @OA\Response(response=200, description="Event deleted")
 * )
 */
Flight::route('DELETE /events/@id', function ($id) {
    Flight::event_service()->delete($id);
    Flight::json(["message" => "Event deleted successfully"]);
});