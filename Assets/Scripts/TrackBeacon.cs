using UnityEngine;

public class TrackBeacon : MonoBehaviour {
    [Header("offset is only for the gizmo. it does not affect an actual beacon position.")]
    public Vector3 offset;
    public float scaleMultiplier = 1;
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(Vector3.zero + offset, scaleMultiplier);
    }
}
