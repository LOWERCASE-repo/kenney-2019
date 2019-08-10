using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
  
  private Vector2 mousePos;
  [SerializeField]
  private Transform player;
  [SerializeField]
  private float lerpMag;
  [SerializeField]
  private Camera cam;
  
  private float zoom;
  
  private bool frozen;
  
  internal void Freeze() {
    frozen = true;
  }
  
  private void Start() {
    zoom = cam.orthographicSize;
    cam.orthographicSize = 0.01f;
  }
  
  protected void Update() {
    if (frozen) return;
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.position = Vector2.Lerp(player.position, mousePos, lerpMag);
    transform.position -= Vector3.forward * 10f;
    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, 0.2f);
  }
}
