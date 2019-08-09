using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// background colour depends on leading ghost

public class Player : Entity {
  
  private Vector2 mousePos;
  
  [SerializeField]
  private Sprite sprite;
  [SerializeField]
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private Bullet[] bullets;
  private int index;
  [SerializeField]
  private Transform face;
  
  private IEnumerator Regen() {
    bool hurt = false;
    foreach (Bullet bullet in bullets) {
      if (!bullet.gameObject.activeSelf) {
        hurt = true;
      }
    }
    if (hurt) {
      yield return new WaitForSecondsRealtime(2f);
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
  
  protected void Update() {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    face.localPosition = Vector2.ClampMagnitude(rb.velocity, 1f) * 0.2f;
    if (Input.GetButtonDown("Shoot")) {
      Bullet bullet = bullets[index];
      // bullet.Throw(mousePos - bullet.rb.position);
      bullet.Throw(mousePos - rb.position);
      index = (index + 1) % bullets.Length;
    }
  }
  
  protected void FixedUpdate() {
    Vector2 moveDir = new Vector2(Input.GetAxisRaw("MoveHorz"), Input.GetAxisRaw("MoveVert"));
    Move(moveDir);
  }
}
