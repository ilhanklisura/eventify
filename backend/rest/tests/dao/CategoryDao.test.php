<?php
require_once __DIR__ . '/../dao/CategoryDao.php';
$categoryDao = new CategoryDao();

echo "<strong>✅ ADD Category</strong><br>";
$category = $categoryDao->add(['name' => 'Test Category']);
print_r($category);
echo "<br><br>";

echo "<strong>🔍 GET ALL Categories</strong><br>";
print_r($categoryDao->get_all());
echo "<br><br>";

echo "<strong>🔍 GET Category BY ID</strong><br>";
print_r($categoryDao->get_by_id($category['id']));
echo "<br><br>";

$category['name'] = 'Updated Category';
echo "<strong>✅ UPDATE Category</strong><br>";
print_r($categoryDao->update($category, $category['id']));
echo "<br><br>";

echo "<strong>❌ DELETE Category</strong><br>";
$categoryDao->delete($category['id']);
print_r($categoryDao->get_all());
echo "<br><br>";