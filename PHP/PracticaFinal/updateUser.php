<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$mail = $_POST["email"];
$password = $_POST["password"];
$height = $_POST["height"];
$weight = $_POST["weight"];

//"SELECT * FROM  user WHERE mail='" + mail + "';"

try {
    $stmt = $conn->prepare("UPDATE public.user SET password = '$password', height = $height , weight =  $weight  WHERE id = $id ;");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();
    //$data = $stmt->fetch();
    
    if ($stmt->rowCount() == 1) {
        echo "0";
        exit();
    } elseif ($stmt->rowCount() == 0) {
        echo "1";
        exit();
    } else {
        echo "2";
        exit();
    }
} catch (PDOException $e) {
    echo "ReadOne failed: " . $e->getMessage();
}
