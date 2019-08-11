using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dummy : Ghost {
  
  [SerializeField]
  private ColorMenu colorMenu;
  
  
  protected override void Start() {
    base.Start();
  }
  
  private float lastDestroyed;
  private float waitTime;
  protected override void Update() {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    face.localPosition = new Vector2(0f, -0.2f) + Vector2.ClampMagnitude(mousePos - rb.position, 1f) * 0.2f;
    face.localPosition = new Vector2(face.localPosition.x, face.localPosition.y * 1.2f);
    if (moused) { // dont judge it's cuter ingame
      float ang = Mathf.LerpAngle(body.rotation.eulerAngles.z, 30f * (rb.position.x - mousePos.x) / 1.5f, 0.2f);
      Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);
      body.rotation = rotation;
      face.rotation = rotation;
    } else {
      float ang = Mathf.LerpAngle(body.rotation.eulerAngles.z, 0f, 0.1f);
      Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);
      body.rotation = rotation;
      face.rotation = rotation;
    }
    if (Time.time - lastDestroyed > waitTime) {
      Bullet bullet = bullets[index];
      bullet.animator.SetTrigger("Fade");
      index = (index + 1) % bullets.Length;
      lastDestroyed = Time.time;
      waitTime = 1.2f + Random.value;
    }
  }
  
  private bool moused;
  private void OnMouseEnter() {
    spriteRenderer.sprite = nootNoot;
    moused = true;
    string assetName = "Music/" + LayerMask.LayerToName(gameObject.layer) + "/ (" + (1 + (int)(Random.value * 15f - Mathf.Epsilon)) + ")";
    audioSource.PlayOneShot(Resources.Load<AudioClip>(assetName));
    colorMenu.SetColor(LayerMask.LayerToName(gameObject.layer));
  }
  
  private void OnMouseExit() {
    spriteRenderer.sprite = happy;
    moused = false;
    colorMenu.ResetColor();
  }
  
  private void OnMouseDown() {
    string assetName = "Music/" + LayerMask.LayerToName(gameObject.layer) + "/(" + (1 + (int)(Random.value * 15f - Mathf.Epsilon)) + ")";
    audioSource.PlayOneShot(Resources.Load<AudioClip>(assetName));
    colorMenu.LockColor(LayerMask.LayerToName(gameObject.layer));
  }
}

