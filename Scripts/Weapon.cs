using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
  // based on state graph
  
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private GameObject[] bullets; // pooling
  
  internal void Shoot() { // animation event
    Debug.Log("hiotnehuason");
  }
  
  internal void SetTriggered(bool triggered) {
    animator.SetBool("Triggered", triggered);
  }
  
  internal void Rotate(Vector2 dir) {
    if (dir.Equals(Vector2.zero)) return;
    float ang = Vector2.SignedAngle(Vector2.up, dir);
    transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
  }
}
