using UnityEngine;

public class GoalPoint : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
