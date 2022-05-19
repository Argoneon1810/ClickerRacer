using System.Collections;
using UnityEngine;

public class JumperAI : MonoBehaviour {
    public JumperController jumperController;
    public Vector2 timeRange;
    public float distanceThresholdForBeaconReachCheck = 3;
    public Vector2 jitterRange;

    private float Jitter => Random.Range(jitterRange.x, jitterRange.y) * Random.Range(0.0f, 1.0f);

    private Transform[] TrackBeacons;
    private int targetIndex;

    void Start() {
        Transform beaconsRoot = FindObjectOfType<TrackBeacons>().transform;
        TrackBeacons = new Transform[beaconsRoot.childCount];
        for(int i = 0; i < beaconsRoot.childCount; ++i)
            TrackBeacons[i] = beaconsRoot.GetChild(i);
        targetIndex = 1;

        StartCoroutine(JumpMocker(Random.Range(timeRange.x, timeRange.y)));
    }

    IEnumerator JumpMocker(float t) {
        yield return new WaitForSeconds(t);
        if(TryGetJumpValue(out float value)) {
            jumperController.Jump(value);
            StartCoroutine(JumpMocker(Random.Range(timeRange.x, timeRange.y)));
            yield return null;
        }
    }

    private bool TryGetJumpValue(out float value) {
        try {
            Vector3 vectorToDestination = TrackBeacons[targetIndex].position - transform.position;

            if(vectorToDestination.sqrMagnitude <= distanceThresholdForBeaconReachCheck)
                ++targetIndex;

            float sign = -Vector3.Dot(Vector3.up, Vector3.Cross(vectorToDestination, transform.forward)).TakeSignOnly();
            float angle = Mathf.Acos(Vector3.Dot(transform.forward, vectorToDestination.normalized)) * Mathf.Rad2Deg;
            angle = angle > 90 ? 90 : angle;

            value = sign * Mathf.InverseLerp(0, 90, angle);
            float jitterForThisJump = Jitter;
            value += jitterForThisJump;

            // Debug.Log(transform.name + "'s angle to target(" + TrackBeacons[targetIndex].name + ") is : " + angle);
            // Debug.Log("value used for jump is : " + value + " with jitter " + jitterForThisJump);
        } catch (System.IndexOutOfRangeException e) {
            Debug.LogError(e.StackTrace);
            value = 0;
            return false;
        }
        return true;
    }
}

public static class Extension {
    public static bool IsNaN(this float value) => float.IsNaN(value);
    public static int TakeSignOnly(this float value) => value > 0 ? 1 : -1;
}