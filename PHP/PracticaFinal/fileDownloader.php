<?php

$dir = "./downloads";

$value = $_GET['fileName'];
$path = $dir . "\\" . $value . ".gpx";
if (file_exists($path)) {
    $mm_type = "application/octet-stream";
    header("Pragma: public");
    header("Expires: 0");
    header("Cache-Control: must-revalidate, post-check=0, pre-check=0");
    header("Cache-Control: public");
    header("Content-Description: File Transfer");
    header("Content-Type: " . $mm_type);
    header("Content-Length: " . (string)(filesize($path)));
    header('Content-Disposition: attachment; filename="' . basename($path) . '"');
    header("Content-Transfer-Encoding: binary\n");
    readfile($path);
    exit();
} else {
    print 'Sorry, we could not find requested download file.';
}
