using UnityEngine;
using System.Collections;

public class Player : Entity {
  
  private Vector2 mousePos;
  
  [SerializeField]
  private Weapon weapon;
  
  
  protected override void Start() {
    base.Start();
  }
  
  protected void Update() {
    Debug.Log(Input.GetButton("Shoot"));
    weapon.SetTriggered(Input.GetButton("Shoot"));
  }
  
  protected void FixedUpdate() {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    // Move(pos - rb.position);
    Vector2 moveDir = new Vector2(Input.GetAxisRaw("MoveHorz"), Input.GetAxisRaw("MoveVert"));
    Move(moveDir);
    weapon.Rotate(mousePos - rb.position);
  }
}
