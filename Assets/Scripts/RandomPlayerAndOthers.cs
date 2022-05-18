using System.Diagnostics;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class RandomPlayerAndOthers : MonoBehaviour {
    IPlayerInjector[] playerInjectors;
    
    void Awake() {
        var mObjs = FindObjectsOfType<MonoBehaviour>();
        playerInjectors = (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(IPlayerInjector)) select (IPlayerInjector) a).ToArray();
    }

    void Start() {
        var playables = GameObject.FindGameObjectsWithTag("Playable");
        int pseudoRandomIndex = Random.Range(0, playables.Length);
        var controller = playables[pseudoRandomIndex].GetComponent<JumpableController>();
        foreach(IPlayerInjector injector in playerInjectors)
            injector.AssignPlayer(controller);
    }
}