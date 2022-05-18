using UnityEngine;

public class RespawnManager : MonoBehaviour {
    [SerializeField] Transform respawnPoint;
    public static RespawnManager Instance;

    void Awake() {
        Instance = Instance ? Instance : this;
    }

    public void Respawn(GameObject fallen) {
        Rigidbody fallenRigidBody = fallen.GetComponent<Rigidbody>();
        fallenRigidBody.velocity = Vector3.zero;
        fallenRigidBody.angularVelocity = Vector3.zero;

        fallen.transform.position = respawnPoint.position;
        fallen.transform.LookAt(respawnPoint.position + respawnPoint.forward * 10);
    }

    public void UpdateRespawnPoint(Transform respawnPoint) => this.respawnPoint = respawnPoint;
}
