<?php

include 'dbConn.php';

$conn = PDOConn::connect();

try {
    $stmt = $conn->prepare("SELECT * FROM public.route where userid = 0;");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();
    
    while ($data = $stmt->fetch()) {
        echo $data['id_route'] . " ";
        echo $data['userid'] . " ";
        echo $data['name'] . " ";
        echo $data['description'] . " ";
        echo "<br>";
        
        $fichero = './downloads\\' . $data['name'] . ".gpx";

        // Escribe el contenido al fichero
        file_put_contents($fichero, $data['file']);
    }

    $download_dir = './downloads';
    //Netejem uploads_dir
    $files = scandir($download_dir);
    foreach ($files as $file) { // iterate files
        if (is_file($download_dir . "\\" . $file)) {
            unlink($download_dir . "\\" . $file); // delete file
        }
    }
} catch (PDOException $e) {
    echo "ReadOne failed: " . $e->getMessage();
}
