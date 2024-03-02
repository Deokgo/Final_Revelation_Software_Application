<?php // this php file is used if the player collides with one game object (papers and key game obj). this is used every time the player collides with a game obj. the name of game obj will be recorded.

    include_once('connects.php');
    $player_id = $_POST['player_id'];
    $game_element_collected = $_POST['game_element_collected'];

    $result = mysqli_query($con,"INSERT INTO Game_Element (player_id, game_element_collected) VALUES('$player_id','$game_element_collected')");
    echo "Game Element Has Been Recorded!";

?>