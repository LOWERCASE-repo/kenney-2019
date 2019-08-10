using UnityEngine;
using System.Collections;

public class Colorizer : MonoBehaviour {
  
  [SerializeField]
  private GameObject player;
  [SerializeField]
  private GameObject[] bots;
  
  private void Start() {
    // PlayerPrefs.SetString("Color", "Blue");
  }
  
  private void Update() {
    Debug.Log(PlayerPrefs.GetString("Color"));
  }
  
  private void OnApplicationQuit() {
    // blease include this in jam games
    PlayerPrefs.DeleteAll();
  }
  
}
