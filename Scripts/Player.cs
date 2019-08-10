using UnityEngine;
using System.Collections;

// background colour depends on leading ghost

public class Player : Ghost {
  
  protected override void Start() {
    base.Start();
  }
  
  protected override void Update() {
    base.Update();
    if (Input.GetButtonDown("Shoot")) {
      Throw(mousePos);
    }
  }
  
  protected void FixedUpdate() {
    Vector2 moveDir = new Vector2(Input.GetAxisRaw("MoveHorz"), Input.GetAxisRaw("MoveVert"));
    Move(moveDir);
  }
  
  protected override void OnCollisionEnter2D(Collision2D collision) {
    base.OnCollisionEnter2D(collision);
  }
  
  private void OnDisable() {
    scorekeeper.EndGame();
  }
}
