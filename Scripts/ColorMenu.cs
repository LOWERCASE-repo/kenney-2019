using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorMenu : MonoBehaviour {
  
  [SerializeField]
  private Text text;
  [SerializeField]
  private Camera cam;
  [SerializeField]
  private Color[] camColors;
  [SerializeField]
  private Color[] textColors;
  private int index;
  
  [SerializeField]
  private Animator textAnim;
  
  internal void SetColor(string colorName) {
    if (locked) return;
    switch (colorName) {
      case "Green": index = 1; break;
      case "Yellow": index = 2; break;
      case "Red": index = 3; break;
      case "Blue": index = 4; break;
    }
  }
  
  internal void ResetColor() {
    if (locked) return;
    index = 0;
  }
  private bool locked;
  internal void LockColor(string colorName) {
    SetColor(colorName);
    PlayerPrefs.SetString("Color", colorName);
    locked = true;
    textAnim.SetTrigger("Ready");
  }
  
  private void Update() {
    if (!locked) {
      text.color = Color.Lerp(text.color, textColors[index], 0.1f);
      cam.backgroundColor = Color.Lerp(cam.backgroundColor, camColors[index], 0.1f);
    }
  }
  
  private void OnApplicationQuit() {
    // blease include this in jam games
    PlayerPrefs.DeleteAll();
  }
}

