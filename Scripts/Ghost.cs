using UnityEngine;
using System.Collections;

// background colour depends on leading ghost

public abstract class Ghost : Entity {
  
  [SerializeField]
  protected Scorekeeper scorekeeper;
  
  protected Vector2 mousePos;
  [SerializeField]
  protected Bullet[] bullets;
  protected int index;
  
  [SerializeField]
  protected Transform face;
  [SerializeField]
  protected Transform body;
  
  [SerializeField]
  protected SpriteRenderer spriteRenderer;
  [SerializeField]
  protected Sprite nootNoot;
  [SerializeField]
  protected Sprite happy;
  [SerializeField]
  private Sprite neutral;
  [SerializeField]
  private Sprite sad;
  [SerializeField]
  private AudioSource audioSource;
  
  [SerializeField]
  protected Animator animator;
  
  protected int health;
  
  internal void Fade() { // called by animator
    gameObject.SetActive(false);
  }
  
  private float lastPlayed;
  
  protected void Throw(Vector2 pos) {
    Bullet bullet = bullets[index];
    bullet.Throw(pos - bullet.rb.position);
    index = (index + 1) % bullets.Length;
    if (Time.time - lastPlayed > 0.5f) {
      string assetName = "Music/" + LayerMask.LayerToName(gameObject.layer) + "/(" + (1 + (int)(Random.value * 15f - Mathf.Epsilon)) + ")";
      audioSource.PlayOneShot(Resources.Load<AudioClip>(assetName));
      lastPlayed = Time.time;
    }
  }
  
  private IEnumerator Regen() {
    bool hurt = false;
    foreach (Bullet bullet in bullets) {
      if (!bullet.gameObject.activeSelf) {
        hurt = true;
      }
    }
    if (hurt) {
      yield return new WaitForSecondsRealtime(0.5f);
      for (int i = 0; i < bullets.Length; i++) {
        Bullet bullet = bullets[(index + i) % bullets.Length];
        if (!bullet.gameObject.activeSelf) {
          bullet.transform.position = rb.position;
          bullet.gameObject.SetActive(true);
          break;
        }
      }
    }
    yield return null;
    StartCoroutine(Regen());
  }
  
  protected override void Start() {
    base.Start();
    StartCoroutine(Regen());
    foreach (Bullet bullet in bullets) {
      bullet.gameObject.layer = gameObject.layer;
    }
  }
  
  protected virtual void Update() {
    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    face.localPosition = new Vector2(0f, -0.2f) + Vector2.ClampMagnitude(rb.velocity, 1f) * 0.2f;
    face.localPosition = new Vector2(face.localPosition.x, face.localPosition.y * 1.2f);
    Quaternion rotation = Quaternion.AngleAxis(-30f * rb.velocity.x / speed, Vector3.forward);
    body.rotation = rotation;
    face.rotation = rotation;
    
    health = 0;
    foreach (Bullet bullet in bullets) {
      if (bullet.gameObject.activeSelf && bullet.throwDir == Vector2.zero) {
        health++;
      }
    }
    if (Time.timeScale < 1f) {
      if (health > 6) {
        spriteRenderer.sprite = happy;
      } else if (health > 4) {
        spriteRenderer.sprite = neutral;
      } else {
        spriteRenderer.sprite = sad;
      }
    }
  }
  
  private bool sentDeath;
  protected virtual void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.name != "Arena") {
      animator.SetTrigger("Die");
      spriteRenderer.sprite = sad;
      if (!sentDeath) {
        scorekeeper.SetColor(LayerMask.LayerToName(collision.gameObject.layer));
        sentDeath = true;
      }
    }
  }
}
