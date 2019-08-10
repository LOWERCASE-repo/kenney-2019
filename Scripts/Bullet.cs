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
    rb.velocity = Vector2.zero;
    rb.drag = 1f;
  }
  
  internal void Fade() { // called by animator
    gameObject.SetActive(false);
  }
  
  private void StartFade() {
    trailRenderer.emitting = false;
    rb.angularDrag = 2f;
    rb.drag = 2f;
    animator.SetTrigger("Fade");
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
    animator.ResetTrigger("Fade");
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
  
  private void OnCollisionEnter2D(Collision2D collision) {
    if (throwDir == Vector2.zero) {
      if (collision.gameObject.name != "Arena") {
        StartFade();
      }
    } else {
      if (collision.gameObject.name == "Arena") {
        StartFade();
      }
    }
    trailRenderer.emitting = false;
  }
}
