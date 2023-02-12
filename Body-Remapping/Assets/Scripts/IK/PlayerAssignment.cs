using UnityEngine;

public class PlayerAssignment : MonoBehaviour {
    [SerializeField] private Transform headset, leftController, rightController, character;

    private void Start() {
        InputManager.Instance.OnInputEvent += CallOnInput;
    }

    private void CallOnInput(object sender, PlayerInputArguments e) {
        switch (e.InputType) {
            case InputType.HMD_Position:
                SetPosition(headset, e.Context.ReadValue<Vector3>());
                break;
            case InputType.HMD_Rotation:
                SetRotation(headset, e.Context.ReadValue<Quaternion>());
                break;
            case InputType.L_Controller_Position:
                SetPosition(leftController, e.Context.ReadValue<Vector3>());
                break;
            case InputType.L_Controller_Rotation:
                SetRotation(leftController, e.Context.ReadValue<Quaternion>());
                break;
            case InputType.R_Controller_Position:
                SetPosition(rightController, e.Context.ReadValue<Vector3>());
                break;
            case InputType.R_Controller_Rotation:
                SetRotation(rightController, e.Context.ReadValue<Quaternion>());
                break;
            default:
                break;
        }
    }

    private void SetPosition(Transform transform, Vector3 position) {
        transform.position = position.x * character.right + position.y * character.up + position.z * character.forward;
    }

    private void SetRotation(Transform transform, Quaternion rotation) {
        transform.rotation = rotation * character.rotation;
    }
}
