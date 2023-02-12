using UnityEngine;

public class AvatarMapper : MonoBehaviour
{
    [SerializeField] private BodyMeasurements avatarMeasurements;

    [SerializeField] private JointLocations avatarJoints;

    [System.Serializable] private struct JointLocations
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

    public void MeasureAvatar()
    {
        // Easily Accessible Vector3 Positions
        Vector3 head = avatarJoints.Head.position;
        Vector3 leftHand = avatarJoints.LeftHand.position;
        Vector3 leftShoulder = avatarJoints.LeftShoulder.position;
        Vector3 leftLeg = avatarJoints.LeftLeg.position;
        Vector3 leftFoot = avatarJoints.LeftFoot.position;
        Vector3 rightFoot = avatarJoints.rightFoot.position;
        Vector3 rightLeg = avatarJoints.RightLeg.position;
        Vector3 rightShoulder = avatarJoints.RightShoulder.position;
        Vector3 rightHand = avatarJoints.RightHand.position;

        // Centerpoints for height calculations
        Vector3 shoulders = new(head.x, (leftShoulder.y + rightShoulder.y) / 2, head.z);
        Vector3 waist = new(head.x, (leftLeg.y + rightLeg.y) / 2, head.z);
        Vector3 feet = new(head.x, (leftFoot.y + rightFoot.y) / 2, head.z);

        avatarMeasurements.leftArmLength = (leftShoulder - leftHand).sqrMagnitude;
        avatarMeasurements.rightArmLength = (rightShoulder - rightHand).sqrMagnitude;
        avatarMeasurements.shoulderWidth = (leftShoulder - rightShoulder).sqrMagnitude;
        avatarMeasurements.shoulderHeight = (shoulders - feet).sqrMagnitude;
        avatarMeasurements.waistHeight = (waist - feet).sqrMagnitude;
        avatarMeasurements.waistWidth = (leftLeg - rightLeg).sqrMagnitude;
        avatarMeasurements.leftLegLength = (leftLeg - leftFoot).sqrMagnitude;
        avatarMeasurements.rightLegLength = (rightLeg- rightFoot).sqrMagnitude;
        avatarMeasurements.height = (head - feet).sqrMagnitude;

        Debug.Log("Avatar Measured");
    }
}
