using UnityEngine;
using Cinemachine;

public class DynamicFollowPlayer : MonoBehaviour, IPlayerInjector {
    [SerializeField] CinemachineVirtualCamera mCamera;

    public void AssignPlayer(JumperController Player) {
        if(mCamera == null)
            mCamera = GetComponent<CinemachineVirtualCamera>();
            
        mCamera.Follow = Player.transform;
        mCamera.LookAt = Player.transform;
    }
}
