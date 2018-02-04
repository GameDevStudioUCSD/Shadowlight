using UnityEngine;

/**
 * Snaps the sprite size to the multiples of `size` component.
 *
 * This is useful for tiled objects, such as walls and platforms.
 */
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class SnapSpriteSize : MonoBehaviour {

  // Put 0.0f for an axis to disable snapping for that axis.
  public Vector2 size;

  private void Update() {

    SpriteRenderer sr = GetComponent<SpriteRenderer>();

    Vector2 tmpSize = sr.size;

    if (size.x != 0.0f) {
      tmpSize.x = Mathf.Round(tmpSize.x / size.x) * size.x;
    }

    if (size.y != 0.0f) {
      tmpSize.y = Mathf.Round(tmpSize.y / size.y) * size.y;
    }

    sr.size = tmpSize;
  }
}
