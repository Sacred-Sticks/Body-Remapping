using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputArguments : EventArgs {
    public InputAction.CallbackContext Context { get; set; }
    public string Binding { get; set; }
    public InputType InputType { get; set; }
}

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    [Serializable] private struct InputActionsData {
        public string Binding;
        public InputType InputType;
        public InputActionType ActionType;
    }
    [SerializeField] private List<InputActionsData> inputActionsData;

    public delegate void OnInput(object sender, PlayerInputArguments e);
    public delegate void OnInputPressed(object sender, PlayerInputArguments e);
    public delegate void OnInputReleased(object sender, PlayerInputArguments e);

    public event OnInput OnInputEvent;
    public event OnInputPressed OnInputPressedEvent;
    public event OnInputReleased OnInputReleasedEvent;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;

        InputAction action;
        foreach(var inputData in inputActionsData) {
            action = new(type: inputData.ActionType, binding: inputData.Binding);
            action.performed += context => CallInputEvents(context: context, inputType: inputData.InputType);
            action.canceled += context => CallInputEvents(context: context, inputType: inputData.InputType);
            SetActionPath(action: action, binding: inputData.Binding);
        }
    }

    private void CallInputEvents(InputAction.CallbackContext context, InputType inputType) {
        PlayerInputArguments args = new() {
            Context = context,
            InputType = inputType,
        };
        OnInputEvent?.Invoke(this, args);
        if (context.performed) {
            OnInputPressedEvent?.Invoke(this, args);
            return;
        }
        if (context.canceled) {
            OnInputReleasedEvent?.Invoke(this, args);
        }
    }

    private void SetActionPath(InputAction action, string binding) {
        action.ChangeBinding(0).WithPath(binding);
        action.Enable();
    }
}
