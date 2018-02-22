using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Use to reference variables that many scripts will access
 */
public static class Globals {

    public static GameObject lightPlayer;  //the Light player controlled by Arrow Keys
    public static GameObject shadowPlayer; //the Shadow player controlled by WASD
    public static GameObject darkness;     //the shadow box covering the entire screen
    public static GameObject gameOverScreen; //the game over screen to be made visible upon death
}
