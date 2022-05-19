using UnityEngine;

public class Respawnable : MonoBehaviour {
    public Transform respawnPoint;

    private void Start() {
        if(respawnPoint == null)
            respawnPoint = FindObjectOfType<RespawnPoint>().transform.parent.GetChild(0);
    }

    public void Respawn() {
        Rigidbody mRigidbody = GetComponent<Rigidbody>();

        mRigidbody.velocity = Vector3.zero;
        mRigidbody.angularVelocity = Vector3.zero;

        transform.position = respawnPoint.position;
        transform.LookAt(respawnPoint.position + respawnPoint.forward * 10);
    }
}