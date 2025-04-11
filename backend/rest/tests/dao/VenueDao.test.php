<?php
require_once __DIR__ . '/../../dao/VenueDao.php';
$venueDao = new VenueDao();

echo "<strong>✅ ADD Venue</strong><br>";
$venue = $venueDao->add([
    'name' => 'Operacija Sana 95',
    'location' => 'Zemaljski muzej BiH'
]);
print_r($venue);
echo "<br><br>";

echo "<strong>🔍 GET ALL Venues</strong><br>";
print_r($venueDao->get_all());
echo "<br><br>";

echo "<strong>🔍 GET Venue BY ID</strong><br>";
print_r($venueDao->get_by_id($venue['id']));
echo "<br><br>";

$venue['name'] = 'Updated Venue';
echo "<strong>✅ UPDATE Venue</strong><br>";
print_r($venueDao->update($venue, $venue['id']));
echo "<br><br>";

echo "<strong>❌ DELETE Venue</strong><br>";
$venueDao->delete($venue['id']);
print_r($venueDao->get_all());
echo "<br><br>";
