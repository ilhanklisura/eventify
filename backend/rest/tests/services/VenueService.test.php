<?php
require_once __DIR__ . '/../../services/VenueService.php';

$service = new VenueService();

echo "<b>➕ ADD Venue</b><br>";
$item = $service->add(["name" => "Test Venue", "location" => "Test Location"]);
print_r($item);
echo "<br>";

echo "<b>📥 GET ALL Venues</b><br>";
print_r($service->get_all());
echo "<br>";

echo "<b>🔍 GET Venue BY ID</b><br>";
print_r($service->get_by_id($item['id']));
echo "<br>";

echo "<b>✏️ UPDATE Venue</b><br>";
$item = $service->update(array_merge($item, ["name" => "Updated Venue"]), $item['id']);
print_r($item);
echo "<br>";

echo "<b>❌ DELETE Venue</b><br>";
$service->delete($item['id']);
print_r($service->get_all());
echo "<br>";
