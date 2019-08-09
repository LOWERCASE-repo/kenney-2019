using UnityEngine;
using System.Collections;

public class Bullet : Entity {
  
  [SerializeField]
  private Animator animator;
  
  internal void Init(Vector2 dir) {
    rb.velocity = dir;
  }
  
  private void FixedUpdate() {
    Move(rb.velocity);
  }
  
  private void OnTriggerEnter2D() {
    animator.SetTrigger("Hit"); // flight into impact
  }
}
