<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$mail = $_POST["email"];

//"SELECT * FROM  user WHERE mail='" + mail + "';"

try {
    $stmt = $conn->prepare("SELECT * FROM  public.user WHERE mail='" . $mail . "';");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();
    $data = $stmt->fetch();
    
    if ($stmt->rowCount() == 1) {
        echo $data['id_user'] . "#" . $data['mail'] . "#" . $data['password'] . "#" . $data['height'] . "#" . $data['weight'] . "#" . $data['maxfc'] . "#" . $data['maxw'];
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
