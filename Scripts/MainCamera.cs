using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
  
  private Vector2 mousePos;
  [SerializeField]
  private Transform player;
  [SerializeField]
  private float lerpMag;
  [SerializeField]
  private Camera camera;
  
  private float zoom;
  private void Start() {
    zoom = camera.orthographicSize;
    camera.orthographicSize = 0.01f;
  }
  
  protected void Update() {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.position = Vector2.Lerp(player.position, mousePos, lerpMag);
    transform.position -= Vector3.forward * 10f;
    camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoom, 0.2f);
  }
}
