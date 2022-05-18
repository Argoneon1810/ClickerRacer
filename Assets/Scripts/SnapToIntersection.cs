using UnityEngine;

[ExecuteInEditMode]
public class SnapToIntersection : MonoBehaviour {
    public class LineBuilder {
        public class Line {
            public Vector3 pointA, pointB, lineVector;
        }

        private Line line;

        public LineBuilder CreateByPointAndVector(Vector3 point, Vector3 vector) {
            line = (line != null) ? line : new Line();
            line.pointA = point;
            line.lineVector = vector;
            line.pointB = point + vector.normalized;
            return this;
        }

        public LineBuilder CreateByTwoPoints(Vector3 pointA, Vector3 pointB) {
            line = (line != null) ? line : new Line();
            line.pointA = pointA;
            line.pointB = pointB;
            line.lineVector = pointB - pointA;
            return this;
        }

        public Line Build() {
            if((line.pointA == null) || (line.pointB == null) || (line.lineVector == null)) return null;
            return line;
        }
    }

    [SerializeField] Transform a, b;
    [SerializeField] Vector3 offset;

    private void Update() {
        if( LineIntersection(
            out Vector3 pointOfIntersection,
            new LineBuilder().CreateByTwoPoints(a.position, a.position + a.forward).Build(),
            new LineBuilder().CreateByTwoPoints(b.position, b.position + b.forward).Build())
        ) {
            transform.position = pointOfIntersection + offset;
        }
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
