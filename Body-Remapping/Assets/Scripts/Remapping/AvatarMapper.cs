using System;
using InputManagement;
using UnityEngine;
namespace Remapping
{
    public class AvatarMapper : MonoBehaviour
    {
        [Serializable]
        public struct JointLocations
        {
            public Transform leftHand;
            public Transform leftShoulder;
            public Transform rightShoulder;
            public Transform rightHand;
            public Transform leftHip;
            public Transform rightHip;
        }
        
        [SerializeField] private string avatarDataKey;
        
        [SerializeField] private DirectionalFloat avatarMeasurements;

        [SerializeField] private JointLocations avatarJoints;

        private void Start()
        {
            EventManager.Trigger(avatarDataKey, new AvatarJointEvent(this, avatarJoints.leftShoulder, avatarJoints.rightShoulder));
        }

        public void MeasureAvatar()
        {
            // Easily Accessible Vector3 Positions
            var leftHand = avatarJoints.leftHand.position;
            var leftShoulder = avatarJoints.leftShoulder.position;
            var rightShoulder = avatarJoints.rightShoulder.position;
            var rightHand = avatarJoints.rightHand.position;

            avatarMeasurements.Left.Value = (leftShoulder - leftHand).magnitude;
            avatarMeasurements.Right.Value = (rightShoulder - rightHand).magnitude;
        }
    }
}
