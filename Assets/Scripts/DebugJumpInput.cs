using UnityEngine;
using UnityEngine.UI;

public class DebugJumpInput : MonoBehaviour, PlayerInjector {
    [SerializeField] Slider slider;
    [SerializeField] JumpableController controller;
    [SerializeField] bool invert;

    public void AssignPlayer(JumpableController Player) {
        controller = Player;
    }

    public void OnClick() {
        controller.Jump(invert ? -slider.value : slider.value);
    }
}
