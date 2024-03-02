<?php // connecting to the database

    $DB_HOST = "localhost";
    $DB_USER = "root"; 
    $DB_PASS = ""; 
    $DB_NAME = "it173p_db";

    $con=mysqli_connect($DB_HOST,$DB_USER,$DB_PASS,$DB_NAME);

    if (!$con)
    {
        die( "Unable to select database");
    }

?>