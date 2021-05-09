<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$userId = $_POST["userId"];

//"SELECT * FROM  user WHERE mail='" + mail + "';"

try {
    $stmt = $conn->prepare("SELECT * FROM  public.workout WHERE id_user = $userId;");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();

    $str = "";
    while ($data = $stmt->fetch()) {
        $str .= $data['id_workout'] . "#" . $data['tempstotal'] . "#" . $data['description'] . "#" . $data['name'];
        $str .= "&";
        //Select blocs

        $stmt = $conn->prepare("SELECT * FROM  public.bloc WHERE id_workout = '" . $data['id_workout'] . "';");
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        while ($data = $stmt->fetch()) {
            $str .= $data['numbloc'] . "#" . $data['id_workout'] . "#" . $data['pot'] . "#" . $data['temps'];
            $str .= "%";
        }
        $str .= "@";
    }
    echo $str;
    exit();
} catch (PDOException $e) {
    echo "ReadOne failed: " . $e->getMessage();
}
