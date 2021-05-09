<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$mail = $_POST["email"];
$password = $_POST["password"];
$height = $_POST["height"];
$weight = $_POST["weight"];

/*$mail = "TestUser";
$password = "PasswordTest";
$height = 120;
$weight = 55;*/

//"SELECT * FROM  user WHERE mail='" + mail + "';"

try {
    $stmt = $conn->prepare("INSERT INTO public.user (mail, password, height, weight, maxfc, maxw) VALUES ('$mail', '$password', $height, $weight, 0, 0);");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();
    //$data = $stmt->fetch();
    
    if ($stmt->rowCount() == 1) {

        $stmt = $conn->prepare("SELECT id_user FROM public.user WHERE mail = '$mail';");
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();
        $data = $stmt->fetch();
        echo $data['id_user'];
        exit();
    } else {
        echo "-1";
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
