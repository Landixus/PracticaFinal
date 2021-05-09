<?php

/**CREATE TABLE "user" (
ID_User	serial PRIMARY KEY,
mail	VARCHAR(50) NOT NULL UNIQUE,
password	VARCHAR(50) NOT NULL,
height	INTEGER NOT NULL,
weight	INTEGER NOT NULL,
maxFC	INTEGER,
maxW	INTEGER
); */

//INSERT INTO public.user(id_user, mail, password, height, weight, maxfc, maxw)
//VALUES (0, 'marcgsitges@hotmail.com', '1234', 123, 55, 120, 120);

include 'dbConn.php';

$conn = PDOConn::connect();

$mail = $_POST["email"];
$password = $_POST["password"];

try {
    $stmt = $conn->prepare("SELECT mail, password FROM  public.user WHERE mail='" . $mail . "' AND password='" . $password . "';");
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
        echo "$stmt->rowCount()";
        exit();
    }
} catch (PDOException $e) {
    echo "ReadOne failed: " . $e->getMessage();
}
