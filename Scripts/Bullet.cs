using UnityEngine;
using System.Collections;

public class Bullet : Entity {
  
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private Rigidbody2D player;
  
  private bool thrown;
  
  internal void Shoot(Vector2 dir) {
    rb.velocity = dir;
  }
  
  private void Start() {
    base.Start();
    string assetName = LayerMask.LayerToName(gameObject.layer) + "/ (" + (1 + (int)(Random.value * 70f)) + ")";
    spriteRenderer.sprite = Resources.Load<Sprite>(assetName);
    gameObject.AddComponent<PolygonCollider2D>();
    rb.velocity = Random.insideUnitCircle;
  }
  
  private void FixedUpdate() {
    Vector2 dir = player.position - rb.position;
    Move(Predict(player.position, player.velocity - rb.velocity, acc * rb.drag) - rb.position);
    Rotate(dir);
  }
  
  private void OnCollisionEnter2D() {
    // death anim
    // can only ever collide with enemies
  }
}
