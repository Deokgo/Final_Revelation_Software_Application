<?php // this php file is used when the player came back after saving the progress. all collected game object is returned in a form of array and these objects should not exist in player's gameplay (since they already collected it)

    include_once('connects.php');
    $player_id = $_POST['player_id'];
    $game_element_collected = $_POST['game_element_collected'];

    $query = "SELECT * FROM Game_Element WHERE player_id = '$player_id'";
    $check=mysqli_query($con,$query);
    $row=mysqli_num_rows($check);

    if($check == FALSE) 
    { 
        echo ".".$row."."; // TODO: better error handling
    }

    while($row=mysqli_fetch_array($check))
    {
        $data[] = $row;
    }

?>