using System;
using UnityEngine;
using UnityEngine.Serialization;
namespace Remapping
{
    public class AvatarMapper : MonoBehaviour
    {

        [SerializeField] private BodyMeasurements avatarMeasurements;

        [SerializeField] private JointLocations avatarJoints;

        public void MeasureAvatar()
        {
            // Easily Accessible Vector3 Positions
            var leftHand = avatarJoints.leftHand.position;
            var leftShoulder = avatarJoints.leftShoulder.position;
            var rightShoulder = avatarJoints.rightShoulder.position;
            var rightHand = avatarJoints.rightHand.position;

            avatarMeasurements.leftArmLength.Value = (leftShoulder - leftHand).magnitude;
            avatarMeasurements.rightArmLength.Value = (rightShoulder - rightHand).magnitude;
        }

        [Serializable] private struct JointLocations
        {
            public Transform leftHand;
            public Transform leftShoulder;
            public Transform rightShoulder;
            public Transform rightHand;
        }

    }
}
