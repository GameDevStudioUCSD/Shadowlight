using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MovingPlatformGear : MonoBehaviour {
    private Transform parentTransform;

    void Start() {
        parentTransform = transform.parent;
    }

	void Update () {
        transform.localScale = new Vector3(1/parentTransform.localScale.x, 1/parentTransform.localScale.y, 1/parentTransform.localScale.z);
    }
}
