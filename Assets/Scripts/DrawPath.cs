using UnityEngine;

public class DrawPath : MonoBehaviour {
    [Header("offset is only for the gizmo. It does not affect an actual beacon position.")]
    [SerializeField] Vector3 offset;
    [SerializeField] float arrowHeadSize;
    [SerializeField] int arrowHeadAngle = 30;

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        for(int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).GetComponent<TrackBeacon>().offset = offset;

            if(i == transform.childCount-1) return;

            Vector3 pointA = transform.GetChild(i).position + offset;
            Vector3 pointB = transform.GetChild(i+1).position + offset;
            
            Gizmos.DrawLine(pointA, pointB);

            Vector3 reversedDirection = (pointA - pointB).normalized;
            Vector3 rotatedDirectionA = Quaternion.AngleAxis(arrowHeadAngle, Vector3.up) * reversedDirection;
            Vector3 rotatedDirectionB = Quaternion.AngleAxis(-arrowHeadAngle, Vector3.up) * reversedDirection;

            Gizmos.DrawLine(pointB, pointB + rotatedDirectionA * arrowHeadSize);
            Gizmos.DrawLine(pointB, pointB + rotatedDirectionB * arrowHeadSize);
        }
    }
}
