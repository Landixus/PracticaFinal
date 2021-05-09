<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$userId = $_POST["userId"];


try {
    $stmt = $conn->prepare("SELECT * FROM public.route where userid = $userId;");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();
    
    while ($data = $stmt->fetch()) {
        
        echo $data['name'];
        echo "#";
        
        $fichero = 'downloads\\' . $data['name'] . ".gpx";

        // Escribe el contenido al fichero
        file_put_contents($fichero, $data['file']);
    }
} catch (PDOException $e) {
    echo "ReadOne failed: " . $e->getMessage();
}
