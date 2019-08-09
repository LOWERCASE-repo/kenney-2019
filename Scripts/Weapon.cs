using UnityEngine;

public class Weapon : MonoBehaviour {
  // based on state graph
  
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private GameObject[] bullets;
  private int index;
  
  internal void Shoot() { // animation event
    Bullet bullet = bullets[index];
    bullet.rb.position = transform.position;
    bullets[index].Init(transform.up);
    bullets[index].SetActive(true);
    index = (index + 1) % bullets.Length;
  }
  
  internal void SetTriggered(bool triggered) {
    animator.SetBool("Triggered", triggered);
    // idle into muzzle into recovery
  }
  
  internal void Rotate(Vector2 dir) {
    if (dir.Equals(Vector2.zero)) return;
    float ang = Vector2.SignedAngle(Vector2.up, dir);
    transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
  }
}
