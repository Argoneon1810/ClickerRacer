using UnityEngine;

public class RespawnManager : MonoBehaviour {
    public static RespawnManager Instance;

    void Awake() {
        Instance = Instance ? Instance : this;
    }

    public void Respawn(GameObject fallen) {
        fallen.GetComponent<Respawnable>().Respawn();
    }

    public void UpdateRespawnPointOf(Transform respawnPoint, GameObject target) {
        target.GetComponent<Respawnable>().respawnPoint = respawnPoint;
    }
}
