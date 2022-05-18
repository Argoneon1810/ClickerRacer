using UnityEngine;

public class FallingDetector : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        RespawnManager.Instance.Respawn(other.gameObject);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
