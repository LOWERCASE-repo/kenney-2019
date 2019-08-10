using UnityEngine;
using System.Collections;

public class Bullet : Entity {
  
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private SpriteRenderer spriteRenderer;
  [SerializeField]
  private Rigidbody2D player;
  [SerializeField]
  private GameObject repeller;
  [SerializeField]
  private TrailRenderer trailRenderer;
  
  private Vector2 throwDir;
  private bool hit; // farm those collision fx
  
  internal void Throw(Vector2 throwDir) {
    this.throwDir = throwDir;
    repeller.layer = gameObject.layer;
    trailRenderer.emitting = true;
    animator.SetTrigger("Throw");
  }
  
  internal void Fade() { // called by animator
    
  }
  
  private void OnEnable() {
    base.Start();
    // unlock more over time?
    string assetName = LayerMask.LayerToName(gameObject.layer) + "/ (" + (1 + (int)(Random.value * 140f - Mathf.Epsilon)) + ")";
    spriteRenderer.sprite = Resources.Load<Sprite>(assetName);
    spriteRenderer.sortingOrder = (Random.value < 0.5) ? -1 : 1;
    Destroy(GetComponent<PolygonCollider2D>());
    gameObject.AddComponent<PolygonCollider2D>();
    rb.velocity = Random.insideUnitCircle;
    repeller.layer = LayerMask.NameToLayer("Repeller");
    trailRenderer.emitting = false;
    throwDir = Vector2.zero;
    hit = false;
  }
  
  private void FixedUpdate() {
    Vector2 dir = player.position - rb.position;
    if (throwDir == Vector2.zero) {
      Move(dir);
      Rotate(dir);
    } else {
      rb.drag = 0f;
      // accel = throwAccel;
      Move(throwDir); // might have to ram
      rb.angularVelocity = rb.velocity.sqrMagnitude;
    }
  }
  
  private void OnCollisionEnter2D() {
    // death anim
    // can only ever collide with enemies
    trailRenderer.emitting = false;
  }
  
  private void OnTriggerEnter2D(Collider2D collider) {
    if (throwDir != Vector2.zero && collider.gameObject.layer != gameObject.layer) {
      Debug.Log("kaboom");
    }
  }
  
  private void OnTriggerExit2D(Collider2D collider) {
    if (collider.gameObject.name == "Arena") {
      gameObject.SetActive(false);
    }
  }
}
