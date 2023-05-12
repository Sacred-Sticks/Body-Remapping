using InputManagement;
using UnityEngine;
using UnityEngine.Serialization;

public class Avatar : MonoBehaviour
{
    [SerializeField] private DirectionalFloat armLengths;
    [SerializeField] private BodyParts shoulders;
    [Space]
    [SerializeField] private string loadAvatarKey;

    private void Start()
    {
        EventManager.Trigger(loadAvatarKey, new AvatarLoadEvent(this, gameObject, armLengths, shoulders)); 
    }
}

[System.Serializable]
public class BodyParts
{
    [SerializeField] private GameObject leftShoulder;
    [SerializeField] private GameObject rightShoulder;

    public GameObject Left
    {
        get
        {
            return leftShoulder;
        }
    }
    public GameObject Right
    {
        get
        {
            return rightShoulder;
        }
    }
}
