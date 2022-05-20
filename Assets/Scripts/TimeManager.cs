using UnityEngine;

public class TimeManager : MonoBehaviour {
    public float GetCurrentTimeScale => Time.timeScale;
    public static TimeManager Instance;
    public float fastForwardAmount = 10;
    float lastTimeScale;

    private void Awake() {
        Instance = Instance ? Instance : this;
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void ResumeFromPause() {
        Time.timeScale = lastTimeScale;
    }

    public void StartFastForward() {
        Time.timeScale = fastForwardAmount;
        lastTimeScale = fastForwardAmount;
    }

    public void StopFastForward() {
        Time.timeScale = 1;
        lastTimeScale = 1;
    }
}