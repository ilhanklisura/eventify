<?php

/**
 * @OA\OpenApi(
 *   @OA\Info(
 *     title="Eventify API",
 *     version="1.0.0",
 *     description="Official API documentation for the Eventify web platform. This API enables full CRUD operations for managing users, events, tickets, venues, bookings, and categories.",
 *     @OA\Contact(
 *       name="Klisura Ilhan",
 *       email="work@ilhanklisura.com",
 *       url="https://ilhanklisura.com"
 *     )
 *   ),
 *   @OA\Server(
 *     url=LOCALSERVER,
 *     description="Local API Server"
 *   ),
 *  @OA\Server(
 *    url=PRODSERVER,
 *    description="Production API Server"
 *   )
 * )
 * @OA\SecurityScheme(
 *     securityScheme="ApiKey",
 *     type="apiKey",
 *     in="header",
 *     name="Authentication"
 * )
 */
