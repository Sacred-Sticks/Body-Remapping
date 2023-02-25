using UnityEngine;
public class Remapper : MonoBehaviour
{

    [SerializeField] private BodyMeasurements userMeasurements;
    [SerializeField] private BodyMeasurements avatarMeasurements;
    [Space]
    [SerializeField] private Transform LeftController;
    [SerializeField] private Transform RightController;
    [Space]
    [SerializeField] private Transform LeftShoulder;
    [SerializeField] private Transform RightShoulder;
    [Space]
    [SerializeField] private Transform AvatarLeftShoulder;
    [SerializeField] private Transform AvatarRightShoulder;
    [Space]
    [SerializeField] private Transform LeftControllerSimulator;
    [SerializeField] private Transform RightControllerSimulator;

    private float leftArmScale;
    private float rightArmScale;

    private void Start()
    {
        leftArmScale = Mathf.Sqrt(avatarMeasurements.leftArmLength) / Mathf.Sqrt(userMeasurements.leftArmLength);
        rightArmScale = Mathf.Sqrt(avatarMeasurements.rightArmLength) / Mathf.Sqrt(userMeasurements.rightArmLength);
    }

    private void Update()
    {
        LeftControllerSimulator.position = ScaleHand(LeftController, LeftShoulder, leftArmScale, AvatarLeftShoulder);
        RightControllerSimulator.position = ScaleHand(RightController, RightShoulder, rightArmScale, AvatarRightShoulder);
        LeftControllerSimulator.rotation = LeftController.rotation;
        RightControllerSimulator.rotation = RightController.rotation;
    }

    private Vector3 ScaleHand(Transform controller, Transform playerShoulder, float scale, Transform avatarShoulder)
    {
        var direction = (controller.position - playerShoulder.position).normalized;
        float defaultLength = Vector3.Distance(playerShoulder.position, controller.position);
        float scaledLength = defaultLength * scale;
        return avatarShoulder.position + direction * scaledLength;
    }

}
