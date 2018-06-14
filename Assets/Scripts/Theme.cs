using UnityEngine;

[CreateAssetMenu(fileName = "Theme.asset")]
public class Theme : ScriptableObject {

    public Sprite background;
    public Sprite platform;
    public Sprite wall;

    public void Apply() {
        ApplyBackground();
        ApplyPlatforms();
        ApplyWalls();
    }

    private void ApplyBackground() {
        GameObject backgroundObject = GameObject.FindWithTag("Background");
        if (backgroundObject == null) {
            Debug.LogWarning("Could not find game object tagged background.", this);
            return;
        }
        SpriteRenderer sr = backgroundObject.GetComponent<SpriteRenderer>();
        if (sr == null) {
            Debug.LogWarning("Could not find SpriteRenderer on GameObject with 'Background' tag.", backgroundObject);
            return;
        }
        sr.sprite = background;
    }

    private void ApplyPlatforms() {
        GameObject[] platformObjects = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platformObject in platformObjects) {
            SpriteRenderer sr = platformObject.GetComponent<SpriteRenderer>();
            if (sr == null) {
                Debug.LogWarning("Could not find SpriteRenderer on GameObject with 'Platform' tag.", platformObject);
                continue;
            }
            sr.sprite = platform;
        }
    }

    private void ApplyWalls() {
        GameObject[] wallObjects = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wallObject in wallObjects) {
            SpriteRenderer sr = wallObject.GetComponent<SpriteRenderer>();
            if (sr == null) {
                Debug.LogWarning("Could not find SpriteRenderer on GameObject with 'Wall' tag.", wallObject);
                continue;
            }
            sr.sprite = wall;
        }
    }
}
