using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Use to reference variables that many scripts will access
 */
public static class Globals {

    public static GameObject lightPlayer;  //the Light player controlled by Arrow Keys
    public static GameObject shadowPlayer; //the Shadow player controlled by WASD
    public static GameObject castLight;    //the primary light given off by the Light player
}
