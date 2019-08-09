using UnityEngine;
using System.Collections;

// background colour depends on leading ghost

public class Player : Entity {
  
  private Vector2 mousePos;
  
  [SerializeField]
  private Sprite sprite;
  [SerializeField]
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private Bullet[] bullets;
  
  protected override void Start() {
    base.Start();
  }
  
  protected void Update() {
    if (Input.GetButtonDown("Shoot")) {
      Debug.Log("sprite swapped");
    }
  }
  
  protected void FixedUpdate() {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // Move(pos - rb.position);
    Vector2 moveDir = new Vector2(Input.GetAxisRaw("MoveHorz"), Input.GetAxisRaw("MoveVert"));
    Move(moveDir);
  }
}
