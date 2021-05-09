<?php

include 'dbConn.php';

$conn = PDOConn::connect();

$blocId = $_POST["bloc_id"];
$workoutId = $_POST["workout_id"];
$pot = $_POST["pot"];
$temps = $_POST["temps"];



/* form.AddField("bloc_id", bloc.numBloc);
        form.AddField("workout_id", id);
        form.AddField("pot", bloc.pot);
        form.AddField("temps", bloc.temps);*/

//"SELECT * FROM  user WHERE mail='" + mail + "';"

try {
    $stmt = $conn->prepare("INSERT INTO public.bloc (numbloc, id_workout, pot, temps) VALUES ($blocId, $workoutId, $pot, $temps);");
    $stmt->setFetchMode(PDO::FETCH_ASSOC);
    $stmt->execute();

    if ($stmt->rowCount() == 1) {
        echo "1";
        exit();
    } else {
        echo "0";
        exit();
    }
} catch (PDOException $e) {
    echo "Error: " . $e->getMessage();
}