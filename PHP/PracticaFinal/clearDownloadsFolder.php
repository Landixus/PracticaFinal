<?php


$downloads_dir = './files';

//Netejem uploads_dir
$files = glob($downloads_dir); // get all file names
foreach ($files as $file) { // iterate files
    if (is_file($file)) {
        unlink($file); // delete file
    }
}
