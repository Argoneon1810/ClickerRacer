using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankTracker : MonoBehaviour {
    public static RankTracker Instance;

    public float rankCheckInterval = 0.001f;
    public TextMeshProUGUI textmesh;
    [SerializeField] List<JumperController> finalRank;
    List<JumperController> controllers;
    Transform[] beaconTransforms;

    public bool IsLiveRankActive => controllers.Count != 0 ? true : false;

    private void Awake() {
        Instance = Instance ? Instance : this;
    }

    void Start() {
        controllers = FindObjectsOfType<JumperController>().ToList();
        Transform beaconsHolder = FindObjectOfType<TrackBeacons>().transform;
        beaconTransforms = new Transform[beaconsHolder.childCount];
        for(int i = 0; i < beaconsHolder.childCount; ++i)
            beaconTransforms[i] = beaconsHolder.GetChild(i);
        finalRank = new List<JumperController>();
        StartCoroutine(TrackRank(rankCheckInterval));
    }

    public void RequestExcludeFromLiveRank(JumperController controller) {
        controllers.Remove(controller);
        finalRank.Add(controller);
        if(controllers.Count == 0)
            GameStateManager.Instance.ReportFinished(controllers.ToNameOnlyList());
    }

    Tuple<int, float, JumperController> GetNearestPointOnNearestLine(Vector3 point, JumperController controller) {
        int nearestLineIndex = -1;
        float sqrDistanceToEndOfLine = float.PositiveInfinity;

        float lastNearestDistance = float.PositiveInfinity;
        Vector3 lastNearestPoint = Vector3.positiveInfinity;
        
        for(int i = 0; i < beaconTransforms.Length-1; ++i) {
            LineBuilder.Line line = new LineBuilder()
                .CreateByTwoPoints(
                    beaconTransforms[i]  .position,
                    beaconTransforms[i+1].position
                )
                .Build();
            Vector3 currentNearestPoint = line.nearestPoint(point);
            float currentNearestDistance = (currentNearestPoint-point).sqrMagnitude;
            if(currentNearestDistance < lastNearestDistance) {
                lastNearestPoint = currentNearestPoint;
                nearestLineIndex = i;
                lastNearestDistance = currentNearestDistance;
                sqrDistanceToEndOfLine = (line.pointB - currentNearestPoint).sqrMagnitude;
            }
        }

        return new Tuple<int, float, JumperController>(nearestLineIndex, sqrDistanceToEndOfLine, controller);
    }

    IEnumerator TrackRank(float rankCheckInterval) {
        while(controllers.Count > 0) {
            List<Tuple<int, float, JumperController>> nearestPointsToNearestLines = new List<Tuple<int, float, JumperController>>();
            foreach(JumperController controller in controllers)
                nearestPointsToNearestLines.Add(GetNearestPointOnNearestLine(controller.transform.position, controller));
            
            nearestPointsToNearestLines.Sort(
                delegate(Tuple<int, float, JumperController> c1, Tuple<int, float, JumperController> c2) {
                    if(c1.Item1 > c2.Item1) return -1;
                    if(c1.Item1 < c2.Item1) return 1;
                    
                    if(c1.Item2 < c2.Item2) return -1;
                    if(c1.Item2 > c2.Item2) return 1;

                    return 0;
                }
            );

            int counter = 1;
            foreach(Tuple<int, float, JumperController> item in nearestPointsToNearestLines) {
                if(!(item.Item3.TryGetComponent<JumperAI>(out JumperAI ai)))
                    textmesh.text = counter == 1 ? counter + "st" : counter == 2 ? counter + "nd" : counter == 3 ? counter + "rd" : counter + "th";
                ++counter;
            }

            yield return new WaitForSeconds(rankCheckInterval);
        }
    }
}
