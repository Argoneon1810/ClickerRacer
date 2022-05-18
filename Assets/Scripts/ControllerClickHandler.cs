using UnityEngine;

public class ControllerClickHandler : MonoBehaviour, IPlayerInjector{
    [SerializeField] JumpableController controller;
    [SerializeField] float biasMultiplier = .5f;

    private void Start() {
        InputManager.Instance.OnMouseLeftDown += OnMouseLeftDown;
    }

    void OnMouseLeftDown() {
        controller.Jump(GetHorizontalBias(Input.mousePosition) * biasMultiplier);
    }

    private float GetHorizontalBias(Vector2 mousePos) {
        return ((Mathf.InverseLerp(0, Screen.currentResolution.width, mousePos.x) - .5f) * 2);
    }

    public void AssignPlayer(JumpableController Player) {
        controller = Player;
    }

    #region Test Codes
    //Test Code #1
    /*
    private Vector2 MousePosAsUV(Vector2 mousePos) {
        float x = Mathf.InverseLerp(0, Screen.currentResolution.width, mousePos.x);
        float y = Mathf.InverseLerp(0, Screen.currentResolution.height, mousePos.y);
        return new Vector2(x, y);
    }
    */

    //Test Code #2
    /*
    private void ClearLog() {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
    */
    #endregion
}