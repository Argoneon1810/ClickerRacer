using System;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance;

    public event Action OnMouseLeftDown;

    private void Start() {
        Instance = Instance ? Instance : this;
    }
    
    void Update() {
        if(Input.GetMouseButtonDown(0)) OnMouseLeftDown?.Invoke();
    }
}
