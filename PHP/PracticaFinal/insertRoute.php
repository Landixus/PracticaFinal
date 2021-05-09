<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$userID = $_POST["userID"];
$fileName = $_POST["fileName"];
$description = $_POST["description"];
//Sacanajem la carpeta files per agafar els fitxers
$uploads_dir = './files';
$files = scandir($uploads_dir);


/*$userID = 0;
$fileName = "Costes";
$description = "";*/

//Ara que tenim el fitxer a files podem fer l'insert en la bbdd agafant aquest fitxer de la carpeta
//Insertem cada fitxer de la carpeta (En principi només hi ha un)
//Ens hem de saltar el . i .. sinó donarà error
foreach ($files as $key => $value) {
    if ('.' !== $value && '..' !== $value) {
        $fileToInsert = simplexml_load_file("$uploads_dir\\$value")->asXML();
        try {
            //$stmt = $conn->prepare("INSERT INTO public.route (userid, fileName, file) VALUES ($userID, '$fileName, '$fileToInsert');");
            
            $stmt = $conn->prepare("INSERT INTO public.route (userid, name, file, description) VALUES ($userID, '$fileName', '$fileToInsert', '$description');");
            
            $stmt->execute();
            //return $stmt;
            echo "0";
        } catch (PDOException $e) {
            echo "echo 1" . $e->getMessage();
        }
    }
}

//Netejem uploads_dir
$files = scandir($uploads_dir);
foreach ($files as $file) { // iterate files
    if (is_file($uploads_dir . "\\" . $file)) {
        unlink($uploads_dir . "\\" . $file); // delete file
    }
}
