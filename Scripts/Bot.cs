using UnityEngine;
using System.Collections;

public class Bot : Ghost {
  
  [SerializeField]
  private Rigidbody2D[] targets;
  private int targetIndex;
  
  [SerializeField]
  private float maxFireRange;
  [SerializeField]
  private float maxFleeRange;
  [SerializeField]
  private float fireRate;
  
  private float fireRange;
  private float fleeRange;
  private float closestRange;
  
  
  private IEnumerator BotThrow() {
    
    yield return new WaitForSecondsRealtime(fireRate);
    StartCoroutine(BotThrow());
  } 
  
  private void BotAct() {
    
    float healthRatio = (health - 1) / 11f;
    fleeRange = maxFleeRange - healthRatio * maxFleeRange;
    fireRange = healthRatio * maxFireRange;
    
    targetIndex = 0;
    for (int i = 0; i < targets.Length; i++) {
      if (targets[targetIndex].gameObject.activeSelf) {
        if ((rb.position - targets[targetIndex].position).sqrMagnitude > (rb.position - targets[i].position).sqrMagnitude) {
          targetIndex = i;
        }
      }
    }
    
    closestRange = (targets[targetIndex].position - rb.position).magnitude;
    Vector2 moveDir = Vector2.zero;
    if (closestRange < fleeRange) {
      moveDir = rb.position - targets[targetIndex].position;
    } else if (rb.velocity != Vector2.zero) {
      moveDir = rb.velocity;
    }
    Move(moveDir + Random.insideUnitCircle);
    
    if (closestRange < fireRange && Random.value > 0.1f) {
      Throw(targets[targetIndex].position);
    }
  }
  
  protected override void Start() {
    base.Start();
    closestRange = Mathf.Infinity;
    StartCoroutine(BotThrow());
  }
  
  private void FixedUpdate() {
    BotAct();
  }
  
  protected override void Update() {
    base.Update();
  }
  
  protected override void OnCollisionEnter2D(Collision2D collision) {
    base.OnCollisionEnter2D(collision);
  }
}

