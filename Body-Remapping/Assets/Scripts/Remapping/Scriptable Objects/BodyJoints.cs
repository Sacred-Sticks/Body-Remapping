using UnityEngine;
[CreateAssetMenu(fileName = "Body Joints", menuName = "Remapping/Body Joints")]
public class BodyJoints : ScriptableObject
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

    public void SetupBones(Transform head, Transform leftHand, Transform leftShoulder, Transform leftLeg, Transform rightLeg, Transform rightShoulder, Transform rightHand)
    {
        Head = head;
        LeftHand = leftHand;
        LeftShoulder = leftShoulder;
        LeftLeg = leftLeg;
        RightLeg = rightLeg;
        RightShoulder = rightShoulder;
        RightLeg = rightLeg;
        RightShoulder = rightShoulder;
        RightHand = rightHand;
    }

}
