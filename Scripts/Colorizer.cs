using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Colorizer : MonoBehaviour {
  
  [SerializeField]
  private GameObject player;
  [SerializeField]
  private GameObject[] bots;
  
  private void Awake() {
    List<string> colors = new List<string>(); // not hashset bc enum for rand
    colors.Add("Green");
    colors.Add("Yellow");
    colors.Add("Red");
    colors.Add("Blue");
    Vector2[] positions = new Vector2[] {
      
    }
    player.layer = LayerMask.NameToLayer(PlayerPrefs.GetString("Color"));
  }
  
  private void OnApplicationQuit() {
    // blease include this in jam games
    PlayerPrefs.DeleteAll();
  }
}
