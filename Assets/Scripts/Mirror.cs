using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {
    public GameObject castLight;
    public List<GameObject> lightDetectors = new List<GameObject>();

    protected CastLight lightScript;
    protected List<CastLight> sources = new List<CastLight>();

    void Start () {
        //start with light off
        lightScript = gameObject.GetComponent<CastLight>();
        castLight.SetActive(false);
        lightScript.enabled = false;
	}
	
	void Update () {
        //check light sources to see if mirror should be Deactivated
        for (int i = 0; i < sources.Count; i++) {
            CastLight source = sources[i];
            if (!source.visibleTargets.Contains(this.transform)) { //checks if center is still lit
                bool flag = false;
                foreach (GameObject detector in lightDetectors) { //checks if each detector is lit
                    if (source.visibleTargets.Contains(detector.transform)) { //if this detector is lit by this source
                        flag = true;
                        break;
                    }
                }
                if (!flag) { //if no detectors are lit; current light source is not lighting the mirror anymore
                    Deactivate(source);
                    i = -1; //restart loop (necessary when List is changed)
                }
            }
        }
    }

    /**
     *  Turns on this mirror's light scripting.
     *  Called by CastLight.cs when mirror enters the light.
     */
    public void Activate(CastLight source) {
        sources.Add(source); //keep track of what's lighting up this mirror
        castLight.SetActive(true); //turn on mirror's light
        lightScript.enabled = true; //start calculating mesh shape
    }

    /**
     *  Turns off this mirror's light scripting.
     *  Called internally when mirror leaves the light.
     */
    public void Deactivate(CastLight source) {
        sources.Remove(source); //remove light source from list
        if (sources.Count <= 0) {
            castLight.SetActive(false); //turn off light
            lightScript.enabled = false; //stop calculating mesh shape (for efficiency)
        }
    }
}
