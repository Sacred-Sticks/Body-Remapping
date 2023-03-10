using Remapping;
using UnityEngine;

public class AvatarSwappingManager : MonoBehaviour
{
    [SerializeField] private EventLinkObject eventTarget;
    
    public void LoadAvatar(AvatarMapper.JointLocations mapping)
    {
        var e = new AvatarEventArgs(mapping.leftShoulder, mapping.rightShoulder);
        eventTarget.CallEvent(this, e);
    }
}

public class AvatarEventArgs : ChainEventArgs
{
    public Transform LeftShoulder
    {
        get;
        set;
    }
    public Transform RightShoulder
    {
        get;
        set;
    }

    public AvatarEventArgs(Transform leftShoulder, Transform rightShoulder)
    {
        LeftShoulder = leftShoulder;
        RightShoulder = rightShoulder;
    }
}
