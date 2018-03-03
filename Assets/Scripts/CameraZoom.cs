using UnityEngine;

/**
 * Dynamically zooms in and out the camera, depending on the characters'
 * positions.
 */
[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour {

  public static CameraZoom instance { get; private set; }

  // Use dynamic zooming.
  public bool dynamicZoom = false;

  // How much boundary should zoom give.
  public float zoomMargin = 3.0f;

  // The minimum zoom.
  public float minZoom = 7.0f;

  // The maximum zoom.
  public float maxZoom = Mathf.Infinity;

  private Transform[] players = null;

    // Component.Camera is obsolete.
#pragma warning disable 0108
  private Camera camera = null;

  private void Awake() {
    // Make sure there is only one of this.
    if (instance != null && instance != this) {
      Destroy(gameObject);
    }
    else {
      instance = this;
    }
  }

  private void Start() {
    camera = GetComponent<Camera>();
  }

  private void Update() {

    if (dynamicZoom) {
      Bounds bounds = FindBounds();

      // Center the camera.
      Vector3 pos = transform.position;
      pos.x = bounds.center.x;
      pos.y = bounds.center.y;
      transform.position = pos;

      // Zoom the camera.
      camera.orthographicSize = bounds.size.y / 2 + zoomMargin;

      float horSize = (bounds.size.x / 2 + zoomMargin) / camera.aspect;
      if (camera.orthographicSize < horSize) {
        camera.orthographicSize = horSize;
      }

      camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoom, maxZoom);
    }
  }

  private void OnDestroy() {
    if (instance == this) {
      instance = null;
    }
  }

  private Bounds FindBounds() {
    if (players == null || players[0] == null || players[1] == null) {
      // No players.
      return new Bounds();
    }

    Bounds bounds = new Bounds(players[0].position, Vector3.zero);
    bounds.Encapsulate(players[1].position);

    return bounds;
  }

  public void RegisterPlayer(Transform playerTransform) {
    if (players == null) {
      players = new Transform[2];
      players[0] = playerTransform;
    }
    else {
      players[1] = playerTransform;
    }
  }
}
