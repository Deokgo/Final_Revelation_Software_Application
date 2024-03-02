<?php // this php file is used if there is an existing player info (saving progress for the nth time)

    include_once('connects.php');
    $player_id = $_POST['player_id'];
    $player_position = $_POST['player_position'];
    $paper_collected = $_POST['paper_collected'];
    $key_collected = $_POST['key_collected'];
    $remaining_health = $_POST['remaining_health'];

    $result = mysqli_query($con,"INSERT INTO Player (player_id, player_position, paper_collected, key_collected, remaining_health) VALUES('$player_id','$player_position', '$paper_collected', '$key_collected', '$remaining_health')");
    echo "Player Information Saved!";

?>