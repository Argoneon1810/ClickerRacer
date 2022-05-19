using UnityEngine;
using System.Linq;

public class ChooseOnePlayerAndSetOthersAsAI : MonoBehaviour {
    IPlayerInjector[] playerInjectors;
    [SerializeField] Vector2 jumpCommandTimeRange = new Vector2(.3f, 2f);
    [SerializeField] Vector2 AIJitterRange;
    
    void Awake() {
        var mObjs = FindObjectsOfType<MonoBehaviour>();
        playerInjectors = (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(IPlayerInjector)) select (IPlayerInjector) a).ToArray();
        
        GameObject[] playables = GameObject.FindGameObjectsWithTag("Playable");

        int pseudoRandomIndex = Random.Range(0, playables.Length);
        JumperController controller = playables[pseudoRandomIndex].GetComponent<JumperController>();

        foreach(IPlayerInjector injector in playerInjectors)
            injector.AssignPlayer(controller);

        int counter = 0;
        foreach(GameObject nonPlayerPlayable in playables) {
            if(counter++ == pseudoRandomIndex) continue;

            var AI = nonPlayerPlayable.AddComponent<JumperAI>();
            AI.timeRange = jumpCommandTimeRange;

            if(AIJitterRange == Vector2.zero)
                AIJitterRange = new Vector2(-.5f, .5f);

            float jitterValueForThisAI = Random.Range(AIJitterRange.x, AIJitterRange.y + float.Epsilon);
            AI.jitterRange = new Vector2(-jitterValueForThisAI, jitterValueForThisAI);

            AI.jumperController = nonPlayerPlayable.GetComponent<JumperController>();
        }
    }
}