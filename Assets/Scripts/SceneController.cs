using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController Instance;

    private void Awake() {
        if(Instance) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void ToMainMenu() {

    }
    
    public void ToGame() {
        
    }

    public void ToRank() {
        SceneManager.LoadScene(
            "RankScene",
            LoadSceneMode.Additive
        );
    }
}