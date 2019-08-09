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
  
  internal Vector2 throwDir;
  
  internal void Throw(Vector2 throwDir) {
    this.throwDir = throwDir;
    repeller.layer = gameObject.layer;
    trailRenderer.emitting = true;
    animator.SetTrigger("Throw");
  }
  
  private void OnEnable() {
    base.Start();
    string assetName = LayerMask.LayerToName(gameObject.layer) + "/ (" + (1 + (int)(Random.value * 17f - Mathf.Epsilon)) + ")";
    spriteRenderer.sprite = Resources.Load<Sprite>(assetName);
    spriteRenderer.sortingOrder = (Random.value < 0.5) ? -1 : 1;
    Destroy(GetComponent<PolygonCollider2D>());
    gameObject.AddComponent<PolygonCollider2D>();
    rb.velocity = Random.insideUnitCircle;
    repeller.layer = LayerMask.NameToLayer("Repeller");
    trailRenderer.emitting = false;
    throwDir = Vector2.zero;
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
