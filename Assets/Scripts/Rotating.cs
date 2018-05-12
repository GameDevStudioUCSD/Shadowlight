using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Rotating : MonoBehaviour {

    public float angle;
    public float speed;

    private bool shouldSpin = false;
    private bool spinning = false;
    private float prevAngle;
    private Rigidbody2D rigidbody2d = null;

    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (shouldSpin && !spinning)
        {
            rigidbody2d.angularVelocity = speed;
            spinning = true;
            prevAngle = rigidbody2d.rotation;
            shouldSpin = false;
        }
        if (spinning)
        {
            rigidbody2d.angularVelocity = speed;
            if (speed > 0)
            {
                if(prevAngle + angle < rigidbody2d.rotation)
                {
                    spinning = false;
                    rigidbody2d.angularVelocity = 0.0f;
                    rigidbody2d.rotation = prevAngle + angle;
                }
            }
            else if(speed < 0)
            {
                if(prevAngle - angle > rigidbody2d.rotation)
                {
                    spinning = false;
                    rigidbody2d.angularVelocity = 0.0f;
                    rigidbody2d.rotation = prevAngle - angle;
                }
            }
            else
            {
                spinning = false;
                rigidbody2d.angularVelocity = 0.0f;
            }
        }
    }

    public void Spin()
    {
        shouldSpin = true;
    }
}
