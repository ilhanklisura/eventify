<?php
require_once 'BaseService.php';
require_once __DIR__ . '/../dao/VenueDao.php';

class VenueService extends BaseService {
    public function __construct() {
        parent::__construct(new VenueDao());
    }

    public function add($entity) {
        if (empty($entity['name']) || empty($entity['location'])) {
            Flight::halt(400, "Name and location are required for venue.");
        }
    
        // Check if venue already exists by name + location
        $existing = $this->dao->get_by_name_and_location($entity['name'], $entity['location']);
        if ($existing) {
            Flight::halt(409, "Venue already exists.");
        }
    
        return parent::add($entity);
    }    
}
