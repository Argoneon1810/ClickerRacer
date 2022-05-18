using UnityEngine;
using Cinemachine;

public class DynamicFollowPlayer : MonoBehaviour, IPlayerInjector {
    CinemachineVirtualCamera mCamera;

    private void Awake() {
        mCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void AssignPlayer(JumpableController Player) {
        mCamera.Follow = Player.transform;
        mCamera.LookAt = Player.transform;
    }
}
