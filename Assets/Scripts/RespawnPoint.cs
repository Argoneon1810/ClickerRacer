using UnityEngine;

public class RespawnPoint : MonoBehaviour {
    public float scaleMultiplier = 1;
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * scaleMultiplier);
    }
}
