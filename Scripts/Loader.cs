using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
  
  
  internal void LoadGame() {
    SceneManager.LoadScene("SampleScene");
  }
}
