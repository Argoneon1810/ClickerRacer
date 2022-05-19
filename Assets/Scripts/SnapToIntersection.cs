using UnityEngine;

[ExecuteInEditMode]
public class SnapToIntersection : MonoBehaviour {
    [SerializeField] Transform a, b;
    [SerializeField] Vector3 offset;

    private void Update() {
        if( LineIntersection(
            out Vector3 pointOfIntersection,
            new LineBuilder().CreateByTwoPoints(a.position, a.position + a.forward).Build(),
            new LineBuilder().CreateByTwoPoints(b.position, b.position + b.forward).Build())
        ) {
            MoveSelfToPoint(pointOfIntersection + offset);
        }
    }

    private void MoveSelfToPoint(Vector3 pointOfIntersection) {
        transform.position = pointOfIntersection;
    }

    public static bool LineIntersection(
        out Vector3 intersection,
        LineBuilder.Line lineA,
        LineBuilder.Line lineB
    ) {
        Vector3 lineVec3 = lineB.pointA - lineA.pointA;
        Vector3 crossVec1and2 = Vector3.Cross(lineA.lineVector, lineB.lineVector);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineB.lineVector);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parallel
        if( Mathf.Abs(planarFactor) < float.Epsilon
                && crossVec1and2.sqrMagnitude > float.Epsilon
        ) {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) 
                    / crossVec1and2.sqrMagnitude;
            intersection = lineA.pointA + (lineA.lineVector * s);
            return true;
        } else {
            intersection = Vector3.zero;
            return false;
        }
    }
}
