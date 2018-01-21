using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {
    public GameObject castLight;
    protected CastLight lightScript;
    protected List<CastLight> sources = new List<CastLight>();

    // Use this for initialization
    void Start () {
        lightScript = gameObject.GetComponent<CastLight>();
        castLight.SetActive(false);
        lightScript.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < sources.Count; i++) {
            CastLight source = sources[i];
            if (!source.visibleTargets.Contains(this.transform)) {
                castLight.SetActive(false);
                lightScript.enabled = false;
                sources.Remove(source);
                i = -1;
            }
        }
    }

    public void Activate(CastLight source) {
        sources.Add(source);
        castLight.SetActive(true);
        lightScript.enabled = true;
    }
}
