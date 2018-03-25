using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rideable : MonoBehaviour {

    private void Update()
    {
        foreach (Transform child in transform) {
            child.Translate(new Vector3(transform.position.x, child.transform.position.y, child.transform.position.z));
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        print("called");
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
                other.transform.parent = transform;
            //other.transform.GetComponent<Rigidbody2D>();
                print("called");
        }
    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.transform.parent = null;
    }
}
