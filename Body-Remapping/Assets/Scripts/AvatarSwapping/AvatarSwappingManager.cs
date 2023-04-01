using System;
using System.Collections.Generic;
using Remapping;
using UnityEngine;
using UnityEngine.Serialization;

public class AvatarSwappingManager : MonoBehaviour
{
    [Header("Received Events")]
    [SerializeField] private EventBus takeAvatarData;
    [SerializeField] private EventBus avatarSwapped;
    [Space]
    [Header("Sending Events")]
    [SerializeField] private EventBus changeMapping;

    private readonly List<AvatarJointEventArgs> avatarsData = new List<AvatarJointEventArgs>();
    private readonly List<GameObject> avatarRoots = new List<GameObject>();

    private int index;

    private void Awake()
    {
        takeAvatarData.Event += OnTakeAvatarDataEvent;
        avatarSwapped.Event += SendAvatarData;
    }

    private void Start()
    {
        foreach (var avatar in avatarRoots)
        {
            avatar.SetActive(false);
        }
    }

    private void OnTakeAvatarDataEvent(object sender, EventArgs e)
    {
        if (e is not AvatarJointEventArgs args)
            return;
        avatarsData.Add(args);
        if (sender is Component component)
            avatarRoots.Add(component.gameObject);
    }

    public void SendAvatarData(object sender, EventArgs e)
    {
        changeMapping.Trigger(this, avatarsData[index]);
        index++;
        if (index >= avatarsData.Count)
            index = 0;
    }
}

public class AvatarJointEventArgs : EventArgs
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

    public AvatarJointEventArgs(Transform leftShoulder, Transform rightShoulder)
    {
        LeftShoulder = leftShoulder;
        RightShoulder = rightShoulder;
    }
}
