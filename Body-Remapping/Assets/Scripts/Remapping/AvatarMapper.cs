using System;
using UnityEngine;
public class AvatarMapper : MonoBehaviour
{

    [SerializeField] private BodyMeasurements avatarMeasurements;

    [SerializeField] private JointLocations avatarJoints;

    public void MeasureAvatar()
    {
        // Easily Accessible Vector3 Positions
        var head = avatarJoints.Head.position;
        var leftHand = avatarJoints.LeftHand.position;
        var leftShoulder = avatarJoints.LeftShoulder.position;
        var leftLeg = avatarJoints.LeftLeg.position;
        var leftFoot = avatarJoints.LeftFoot.position;
        var rightFoot = avatarJoints.rightFoot.position;
        var rightLeg = avatarJoints.RightLeg.position;
        var rightShoulder = avatarJoints.RightShoulder.position;
        var rightHand = avatarJoints.RightHand.position;

        // Centerpoints for height calculations
        Vector3 shoulders = new Vector3(head.x, (leftShoulder.y + rightShoulder.y) / 2, head.z);
        Vector3 waist = new Vector3(head.x, (leftLeg.y + rightLeg.y) / 2, head.z);
        Vector3 feet = new Vector3(head.x, (leftFoot.y + rightFoot.y) / 2, head.z);

        avatarMeasurements.leftArmLength = (leftShoulder - leftHand).sqrMagnitude;
        avatarMeasurements.rightArmLength = (rightShoulder - rightHand).sqrMagnitude;
        avatarMeasurements.shoulderWidth = (leftShoulder - rightShoulder).sqrMagnitude;
        avatarMeasurements.shoulderHeight = (shoulders - feet).sqrMagnitude;
        avatarMeasurements.waistHeight = (waist - feet).sqrMagnitude;
        avatarMeasurements.waistWidth = (leftLeg - rightLeg).sqrMagnitude;
        avatarMeasurements.leftLegLength = (leftLeg - leftFoot).sqrMagnitude;
        avatarMeasurements.rightLegLength = (rightLeg - rightFoot).sqrMagnitude;
        avatarMeasurements.height = (head - feet).sqrMagnitude;

        Debug.Log("Avatar Measured");
    }

    [Serializable] private struct JointLocations
    {

        public Transform Head;
        public Transform LeftHand;
        public Transform LeftShoulder;
        public Transform LeftLeg;
        public Transform LeftFoot;
        public Transform rightFoot;
        public Transform RightLeg;
        public Transform RightShoulder;
        public Transform RightHand;

    }

}
