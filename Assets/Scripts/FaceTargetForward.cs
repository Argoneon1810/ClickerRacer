using UnityEngine;

[ExecuteInEditMode]
public class FaceTargetForward : MonoBehaviour {
    [SerializeField] Transform target;
    void Update() {
        if(target == null) return;
        transform.LookAt(transform.position + target.forward, transform.up);
    }
}
