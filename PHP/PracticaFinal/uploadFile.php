<?php

//Agafem el fitxer que ens han passat el client
//https://stackoverflow.com/questions/10237983/upload-to-php-server-from-c-sharp-client-application
$uploads_dir = './files'; //Directory to save the file that comes from client application.
if ($_FILES["file"]["error"] == UPLOAD_ERR_OK) {
    $tmp_name = $_FILES["file"]["tmp_name"];
    $name = $_FILES["file"]["name"];
    move_uploaded_file($tmp_name, "$uploads_dir/$name");
}
