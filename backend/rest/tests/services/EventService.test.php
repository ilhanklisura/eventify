<?php
require_once __DIR__ . '/../../services/EventService.php';

$service = new EventService();

echo "<b>➕ ADD Event</b><br>";
$item = $service->add([
    "title" => "Test Event",
    "description" => "Description",
    "date" => "2025-05-01 18:00:00",
    "category_id" => 6,
    "venue_id" => 1,
    "organizer_id" => 4
]);
print_r($item);
echo "<br>";

echo "<b>📥 GET ALL Events</b><br>";
print_r($service->get_all());
echo "<br>";

echo "<b>🔍 GET Event BY ID</b><br>";
print_r($service->get_by_id($item['id']));
echo "<br>";

echo "<b>✏️ UPDATE Event</b><br>";
$item = $service->update(array_merge($item, ["title" => "Updated Event"]), $item['id']);
print_r($item);
echo "<br>";

echo "<b>❌ DELETE Event</b><br>";
$service->delete($item['id']);
print_r($service->get_all());
echo "<br>";