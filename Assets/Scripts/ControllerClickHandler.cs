using UnityEngine;

public class ControllerClickHandler : MonoBehaviour, IPlayerInjector{
    [SerializeField] JumperController controller;
    [SerializeField] float biasMultiplier = .5f;

    private void Start() {
        InputManager.Instance.OnMouseLeftDown += OnMouseLeftDown;
    }

    void OnMouseLeftDown() {
        controller.Jump(GetHorizontalBias(Input.mousePosition) * biasMultiplier);
    }

    public void AssignPlayer(JumperController Player) {
        controller = Player;
    }

    private float GetHorizontalBias(Vector2 mousePos) => ((Mathf.InverseLerp(0, Screen.width, mousePos.x) - .5f) * 2);
}