using UnityEngine;
using System.Collections;

// background colour depends on leading ghost

public abstract class Ghost : Entity {
  
  protected Vector2 mousePos;
  [SerializeField]
  protected Bullet[] bullets;
  protected int index;
  
  [SerializeField]
  private Transform face;
  [SerializeField]
  private Transform body;
  
  [SerializeField]
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private Sprite happy;
  [SerializeField]
  private Sprite neutral;
  [SerializeField]
  private Sprite sad;
  
  private IEnumerator Regen() {
    bool hurt = false;
    foreach (Bullet bullet in bullets) {
      if (!bullet.gameObject.activeSelf) {
        hurt = true;
      }
    }
    if (hurt) {
      yield return new WaitForSecondsRealtime(3f);
      for (int i = 0; i < bullets.Length; i++) {
        Bullet bullet = bullets[(index + i) % bullets.Length];
        if (!bullet.gameObject.activeSelf) {
          bullet.transform.position = rb.position;
          bullet.gameObject.SetActive(true);
          break;
        }
      }
    }
    yield return null;
    StartCoroutine(Regen());
  }
  
  protected override void Start() {
    base.Start();
    StartCoroutine(Regen());
  }
  
  protected virtual void Update() {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    face.localPosition = new Vector2(0f, -0.2f) + Vector2.ClampMagnitude(rb.velocity, 1f) * 0.2f;
    face.localPosition = new Vector2(face.localPosition.x, face.localPosition.y * 1.2f);
    Quaternion rotation = Quaternion.AngleAxis(-30f * rb.velocity.x / speed, Vector3.forward);
    body.rotation = rotation;
    face.rotation = rotation;
    
    int health = 0;
    foreach (Bullet bullet in bullets) {
      if (bullet.gameObject.activeSelf) {
        health++;
      }
    }
    Debug.Log(health);
    if (health > 4) {
      spriteRenderer.sprite = happy;
    } else if (health > 2) {
      spriteRenderer.sprite = neutral;
    } else {
      spriteRenderer.sprite = sad;
    }
  }
}
