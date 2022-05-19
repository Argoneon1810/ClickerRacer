using UnityEngine;

public class LineBuilder {
    public class Line {
        public Vector3 pointA, pointB, lineVector;
        public Vector3 nearestPoint(Vector3 point) {
            Vector3 lineVectorNormalized = lineVector.normalized;
            return pointA + lineVectorNormalized * Vector3.Dot((point - pointA), lineVectorNormalized);
        }
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