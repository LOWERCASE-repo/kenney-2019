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
      Bullet bullet = bullets[index];
      bullet.Throw(mousePos - bullet.rb.position);
      // bullet.Throw(mousePos - rb.position);
      index = (index + 1) % bullets.Length;
    }
  }
  
  protected void FixedUpdate() {
    Vector2 moveDir = new Vector2(Input.GetAxisRaw("MoveHorz"), Input.GetAxisRaw("MoveVert"));
    Move(moveDir);
    int health = 0;
    foreach (Bullet bullet in bullets) {
      if (bullet.gameObject.activeSelf) {
        health++;
        Debug.Log(health);
      }
    }
  }
}
