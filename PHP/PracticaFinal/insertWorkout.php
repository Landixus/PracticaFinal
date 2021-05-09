<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$userId = $_POST["userId"];
$temps = $_POST["tempsTotal"];
$description = $_POST["description"];
$name = $_POST["name"];

/* form.AddField("userId", PaginaPrincipal.user.id);
        form.AddField("tempsTotal", workout.tempsTotal);
        form.AddField("description", workout.description);
        form.AddField("name", workout.name);*/

//"SELECT * FROM  user WHERE mail='" + mail + "';"

try {
    $stmt = $conn->prepare("INSERT INTO public.workout (id_user, tempstotal, description, name) VALUES ($userId,$temps, '$description', '$name');");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();
    
    
    if ($stmt->rowCount() == 1) {
        $stmt = $conn->prepare("SELECT id_workout FROM public.workout WHERE id_user = $userId AND name = '$name';");
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();
        
        if ($stmt->rowCount() == 1) {
            $data = $stmt->fetch();
            echo $data['id_workout'];
            exit();
        }
    } else {
        echo "-1";
        exit();
    }
} catch (PDOException $e) {
    $pos = strpos($e->getMessage(), 'Unique violation');

    // Nótese el uso de ===. Puesto que == simple no funcionará como se espera
    // porque la posición de 'a' está en el 1° (primer) caracter.
    if ($pos === false) {
        echo "ReadOne failed: " . $e->getMessage();
    } else {
        echo "notUnique";
    }
}
