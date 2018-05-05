using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTopTrigger : MonoBehaviour {
    /**
     * This class goes on a trigger on top of objects that the player should move with while on top of.
     * If the object has a Dynamic Rigidbody, its children that also have Rigidbodies will not move with it.
     * Code here makes the player a child of the object if it has a Kinematic Rigidbody, and moves the player
     *   with the object manually if the object has a Dynamic Rigidbody.
     */

    public List<PlayerController> players; //list of players in the trigger
    private Vector3 lastPosition; //position of the object last frame
    private bool isDynamic = false; //whether or not the object's Rigidbody is Dynamic
    
	void Start () {
        lastPosition = transform.parent.position; //initialize position
        //Check if Rigidbody is Dynamic
        RigidbodyType2D rbtype = GetComponentInParent<Rigidbody2D>().bodyType;
        if (rbtype == RigidbodyType2D.Dynamic) isDynamic = true;
    }
	
    /**
     * Moves players in the trigger with the object the trigger is on top of.
     */
	void LateUpdate () {
        //If Rigidbody is Dynamic, players on top of the object are moved the same distance as the object
		if (isDynamic && players.Count != 0) {
            Vector3 diff = transform.parent.position - lastPosition;
            foreach (PlayerController player in players)
                player.transform.position += diff;
        }
        lastPosition = transform.parent.position; //update position
	}

    /**
     * If an object entering the trigger is a player, starts moving them with the object.
     */
    public void OnTriggerEnter2D(Collider2D other) {
        PlayerController obj = other.gameObject.GetComponent<PlayerController>(); //check if collider is player
        if (obj && !players.Contains(obj)) {
            players.Add(obj); //add player to list of players to move
            if (!isDynamic) obj.transform.SetParent(transform.parent); //if Rigidbody is Kinematic, make the player a child
        }
    }

    /**
     * If an object exiting the trigger is a player, stops moving them with the object.
     */
    public void OnTriggerExit2D(Collider2D other) {
        if (players.Count != 0) { //no point if there are no players on top of the object already
            PlayerController obj = other.gameObject.GetComponent<PlayerController>(); //check if collider is player
            if (obj && players.Contains(obj)) {
                if (!isDynamic) obj.transform.SetParent(null); //if Rigidbody is Kinematic, remove the player as a child
                players.Remove(obj); //remove player from list of players to move
            }
        }
    }
}
