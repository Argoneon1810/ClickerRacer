using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance;

    private void Awake() {
        Instance = Instance ? Instance : this;
    }

    public void ReportPlayerFinished() {
        InputManager.Instance.blockInput = true;
        if(RankTracker.Instance.IsLiveRankActive)
            TimeManager.Instance.StartFastForward();
    }

    public void ReportFinished(List<string> rank) {
        TimeManager.Instance.StopFastForward();
        // SceneController.Instance.ToRank(rank);
    }
}