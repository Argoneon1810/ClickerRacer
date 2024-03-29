using UnityEngine;

public class CheckPoint : MonoBehaviour {
    [SerializeField] Transform respawnPoint;

    void OnTriggerEnter(Collider other) {
        RespawnManager.Instance.UpdateRespawnPointOf(respawnPoint, other.gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
