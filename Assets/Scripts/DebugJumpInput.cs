using UnityEngine;
using UnityEngine.UI;
using MyBox;

public class DebugJumpInput : MonoBehaviour, IPlayerInjector {
    [SerializeField] bool biasable;
    [SerializeField, ConditionalField("biasable")] Slider slider;
    [SerializeField, ConditionalField("biasable")] bool invert;
    [SerializeField] JumperController controller;

    public void AssignPlayer(JumperController Player) {
        controller = Player;
    }

    public void OnClick() {
        if(slider)
            controller.Jump(invert ? -slider.value : slider.value);
        else
            controller.Jump(0);
    }
}
