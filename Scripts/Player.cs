using UnityEngine;
using System.Collections;

// background colour depends on leading ghost

public class Player : Ghost {
  
  internal bool alive;
  
  protected override void Start() {
    base.Start();
    alive = true;
  }
  
  protected override void Update() {
    base.Update();
    if (Input.GetButtonDown("Shoot")) {
      Throw(mousePos);
    }
    if (Time.timeScale < 1f) {
      spriteRenderer.sprite = nootNoot;
    }
  }
  
  protected void FixedUpdate() {
    Vector2 moveDir = new Vector2(Input.GetAxisRaw("MoveHorz"), Input.GetAxisRaw("MoveVert"));
    Move(moveDir);
  }
  
  protected override void OnCollisionEnter2D(Collision2D collision) {
    base.OnCollisionEnter2D(collision);
    if (collision.gameObject.name != "Arena") {
      alive = false;
    }
  }
  
  private void OnDisable() {
    scorekeeper.EndGame();
  }
}
