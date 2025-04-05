<?php

require_once __DIR__ . '/../../services/CategoryService.php';

$service = new CategoryService();

// Add
echo "<b>✅ Adding Category:</b><br>";
$entity = $service->add(['name' => 'Service Test Category']);
print_r($entity);
echo "<br>";

// Get All
echo "<b>📦 All Categories:</b><br>";
print_r($service->get_all());
echo "<br>";

// Get by ID
echo "<b>🔍 Get Category by ID:</b><br>";
print_r($service->get_by_id($entity['id']));
echo "<br>";

// Update
echo "<b>✏️ Updating Category:</b><br>";
$entity['name'] = 'Updated Category';
print_r($service->update($entity, $entity['id']));
echo "<br>";

// Delete
echo "<b>🗑️ Deleting:</b><br>";
$service->delete($entity['id']);
print_r($service->get_all());