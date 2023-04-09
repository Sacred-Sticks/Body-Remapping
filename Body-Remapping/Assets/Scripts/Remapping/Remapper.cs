using System;
using InputManagement;
using ReferenceVariables;
using UnityEngine;
using UnityEngine.Serialization;

public class Remapper : MonoBehaviour
{
    [SerializeField] private Transform headset;
    [Space]
    [SerializeField] private DirectionalFloat userMeasurements;
    [SerializeField] private DirectionalFloat avatarMeasurements;
    [Space]
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;
    [Space]
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform rightShoulder;
    [Space]
    [SerializeField] private Transform leftControllerSimulator;
    [SerializeField] private Transform rightControllerSimulator;
    [Space]
    [SerializeField] private Transform avatarLeftShoulder;
    [SerializeField] private Transform avatarRightShoulder;
    [Space]
    [Header("Received Events")]
    [SerializeField] private EventBus remapAvatar;

    private void Awake()
    {
        remapAvatar.Event += OnEventCalled;
    }

    private void OnEventCalled(object sender, EventArgs e)
    {
        if (e is not AvatarJointEventArgs args)
            return;
        avatarLeftShoulder = args.LeftShoulder;
        avatarRightShoulder = args.RightShoulder;
    }

    private void Update()
    {
        var offset = headset.position;
        offset.y = 0;
        float leftArmScale = avatarMeasurements.Left.Value / userMeasurements.Left.Value;
        float rightArmScale = avatarMeasurements.Right.Value / userMeasurements.Right.Value;
        leftControllerSimulator.position = ScaleHand(leftController, leftShoulder, leftArmScale, avatarLeftShoulder) + offset;
        rightControllerSimulator.position = ScaleHand(rightController, rightShoulder, rightArmScale, avatarRightShoulder) + offset;
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
