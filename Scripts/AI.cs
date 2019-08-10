using UnityEngine;
using System.Collections;

public class AI : Ghost {
  
  /*
  */
  
  [SerializeField]
  private Rigidbody2D[] targets;
  private int targetIndex;
  
  protected override void Start() {
    base.Start();
  }
  
  protected override void Update() {
    base.Update();
    if ((targets[targetIndex].position - rb.position).sqrMagnitude < 300) {
      if (Random.value < 0.02) {
        Bullet bullet = bullets[index];
        bullet.Throw(targets[targetIndex].position - rb.position);
        index = (index + 1) % bullets.Length;
      }
    }
  }
  
  
}

