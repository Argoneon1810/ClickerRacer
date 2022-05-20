using UnityEngine;

public class GoalPoint : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        FindObjectOfType<RankTracker>().RequestExcludeFromLiveRank(other.GetComponent<JumperController>());
        if(!(other.TryGetComponent<JumperAI>(out JumperAI ai))) {
            FindObjectOfType<EndOfGameShutter>().RequestDropShutter();
            GameStateManager.Instance.ReportPlayerFinished();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
