using UnityEngine;
using UnityEngine.Serialization;
public class Remapper : MonoBehaviour
{

    [SerializeField] private BodyMeasurements userMeasurements;
    [SerializeField] private BodyMeasurements avatarMeasurements;
    [Space]
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;
    [Space]
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform rightShoulder;
    [Space]
    [SerializeField] private Transform avatarLeftShoulder;
    [SerializeField] private Transform avatarRightShoulder;
    [Space]
    [SerializeField] private Transform leftControllerSimulator;
    [SerializeField] private Transform rightControllerSimulator;

    private void Start()
    {
    }

    private void Update()
    {
        float leftArmScale = avatarMeasurements.leftArmLength.Value / userMeasurements.leftArmLength.Value;
        float rightArmScale = avatarMeasurements.rightArmLength.Value / userMeasurements.rightArmLength.Value;
        leftControllerSimulator.position = ScaleHand(leftController, leftShoulder, leftArmScale, avatarLeftShoulder);
        rightControllerSimulator.position = ScaleHand(rightController, rightShoulder, rightArmScale, avatarRightShoulder);
        leftControllerSimulator.rotation = leftController.rotation;
        rightControllerSimulator.rotation = rightController.rotation;
    }

    private static Vector3 ScaleHand(Transform controller, Transform playerShoulder, float scale, Transform avatarShoulder)
    {
        var direction = (controller.position - playerShoulder.position).normalized;
        float defaultLength = Vector3.Distance(playerShoulder.position, controller.position);
        float scaledLength = defaultLength * scale;
        return avatarShoulder.position + direction * scaledLength;
    }

}
