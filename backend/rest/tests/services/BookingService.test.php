<?php
require_once __DIR__ . '/../../services/BookingService.php';

$service = new BookingService();

echo "<b>➕ ADD Booking</b><br>";
$item = $service->add(["event_id" => 1, "user_id" => 3]);
print_r($item);
echo "<br>";

echo "<b>📥 GET ALL Bookings</b><br>";
print_r($service->get_all());
echo "<br>";

echo "<b>🔍 GET Booking BY ID</b><br>";
print_r($service->get_by_id($item['id']));
echo "<br>";

echo "<b>❌ DELETE Booking</b><br>";
$service->delete($item['id']);
print_r($service->get_all());
echo "<br>";
