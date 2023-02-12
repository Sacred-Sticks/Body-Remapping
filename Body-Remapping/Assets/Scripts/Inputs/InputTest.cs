using UnityEngine;

public class InputTest : MonoBehaviour
{
    private void Start() {
        InputManager.Instance.OnInputEvent += RecieveInput;
        InputManager.Instance.OnInputPressedEvent += RecieveInputPressed;
        InputManager.Instance.OnInputReleasedEvent += RecieveInputReleased;
    }

    private void RecieveInput(object sender, PlayerInputArguments e) {
        switch (e.InputType) {
            case InputType.Test_W:
                //Debug.Log("W Modified");
                break;
            case InputType.Test_A:
                //Debug.Log("A Modified");
                break;
            case InputType.Test_S:
                //Debug.Log("S Modified");
                break;
            case InputType.Test_D:
                //Debug.Log("D Modified");
                break;
            default:
                break;
        }
    }

    private void RecieveInputPressed(object sender, PlayerInputArguments e) {
        switch (e.InputType) {
            case InputType.Test_W:
                Debug.Log("W Pressed");
                break;
            case InputType.Test_A:
                Debug.Log("A Pressed");
                break;
            case InputType.Test_S:
                Debug.Log("S Pressed");
                break;
            case InputType.Test_D:
                Debug.Log("D Pressed");
                break;
            default:
                break;
        }
    }

    private void RecieveInputReleased(object sender, PlayerInputArguments e) {
        switch (e.InputType) {
            case InputType.Test_W:
                Debug.Log("W Released");
                break;
            case InputType.Test_A:
                Debug.Log("A Released");
                break;
            case InputType.Test_S:
                Debug.Log("S Released");
                break;
            case InputType.Test_D:
                Debug.Log("D Released");
                break;
            default:
                break;
        }
    }
}
