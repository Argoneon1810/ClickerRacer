using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankTracker : MonoBehaviour {
    public float rankCheckInterval = 0.001f;
    public TextMeshProUGUI textmesh;
    List<string> rank;
    JumperController[] controllers;
    Transform[] beaconTransforms;

    void Start() {
        controllers = FindObjectsOfType<JumperController>();
        Transform beaconsHolder = FindObjectOfType<TrackBeacons>().transform;
        beaconTransforms = new Transform[beaconsHolder.childCount];
        for(int i = 0; i < beaconsHolder.childCount; ++i)
            beaconTransforms[i] = beaconsHolder.GetChild(i);
        rank = new List<string>();
        StartCoroutine(TrackRank(rankCheckInterval));
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
            rank.Add(item.Item3.name);
            if(!(item.Item3.TryGetComponent<JumperAI>(out JumperAI ai)))
                textmesh.text = counter == 1 ? counter + "st" : counter == 2 ? counter + "nd" : counter == 3 ? counter + "rd" : counter + "th";
            ++counter;
        }

        yield return new WaitForSeconds(rankCheckInterval);
        StartCoroutine(TrackRank(rankCheckInterval));
    }
}
