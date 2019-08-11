using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Colorizer : MonoBehaviour {
  
  [SerializeField]
  private GameObject player;
  [SerializeField]
  private GameObject[] bots;
  
  [SerializeField]
  private Transform[] ghosts;
  
  private void Awake() {
    List<string> colors = new List<string>(); // not hashset bc enum for rand
    colors.Add("Green");
    colors.Add("Yellow");
    colors.Add("Red");
    colors.Add("Blue");
    List<Vector2> positions = new List<Vector2>();
    positions.Add(new Vector2(30f, 0f));
    positions.Add(new Vector2(-30f, 0f));
    positions.Add(new Vector2(0f, 30f));
    positions.Add(new Vector2(0f, -30f));
    
    string layerName = PlayerPrefs.GetString("Color");
    
    // couldve been avoided with the most basic of layer organization
    player.layer = LayerMask.NameToLayer(layerName);
    colors.Remove(layerName);
    
    for (int i = 0; i < bots.Length; i++) {
      layerName = colors[(int)(colors.Count * (Random.value - Mathf.Epsilon))];
      bots[i].layer = LayerMask.NameToLayer(layerName);
      colors.Remove(layerName);
    }
    
    for (int i = 0; i < ghosts.Length; i++) {
      ghosts[i].position = positions[(int)(positions.Count * (Random.value - Mathf.Epsilon))];
      positions.Remove((Vector2)ghosts[i].position);
    }
  }
  
  private void OnApplicationQuit() {
    // blease include this in jam games
    PlayerPrefs.DeleteAll();
  }
}
