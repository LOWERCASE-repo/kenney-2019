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
  
  private Collider2D collider;
  
  internal Vector2 throwDir;
  private bool hit;
  
  internal void Throw(Vector2 throwDir) {
    this.throwDir = throwDir;
    repeller.layer = gameObject.layer;
    trailRenderer.emitting = true;
    animator.SetTrigger("Throw");
    rb.velocity = Vector2.zero;
  }
  
  internal void Fade() { // called by animator
    gameObject.SetActive(false);
  }
  
  private void StartFade() {
    trailRenderer.emitting = false;
    rb.angularDrag = 2f;
    rb.drag = 2f;
    animator.SetTrigger("Fade");
    Destroy(collider);
  }
  
  private void OnEnable() {
    base.Start();
    // unlock more over time?
    string assetName = LayerMask.LayerToName(player.gameObject.layer) + "/ (" + (1 + (int)(Random.value * 140f - Mathf.Epsilon)) + ")";
    spriteRenderer.sprite = Resources.Load<Sprite>(assetName);
    spriteRenderer.sortingOrder = (Random.value < 0.5) ? -1 : 1;
    Destroy(GetComponent<PolygonCollider2D>());
    collider = gameObject.AddComponent<PolygonCollider2D>();
    rb.velocity = Random.insideUnitCircle;
    repeller.layer = LayerMask.NameToLayer("Repeller");
    trailRenderer.emitting = false;
    throwDir = Vector2.zero;
    animator.ResetTrigger("Fade");
    hit = false;
  }
  
  private void FixedUpdate() {
    Vector2 dir = player.position - rb.position;
    if (throwDir == Vector2.zero) {
      Move(dir);
      Rotate(dir);
    } else if (!hit) {
      rb.drag = 1f;
      // accel = throwAccel;
      Move(throwDir); // might have to ram
      rb.angularVelocity = rb.velocity.sqrMagnitude;
    } else {
      Move(rb.velocity);
    }
    if (!player.gameObject.activeSelf && collider != null) {
      StartFade();
    }
  }
  
  private void OnCollisionEnter2D(Collision2D collision) {
    if (throwDir == Vector2.zero) {
      if (collision.gameObject.name != "Arena") {
        StartFade();
      }
    } else {
      hit = true;
      StartFade();
    }
    trailRenderer.emitting = false;
  }
}
