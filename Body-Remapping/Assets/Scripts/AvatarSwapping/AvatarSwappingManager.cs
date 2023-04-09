using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int activeAvatarIndex;

    private void Awake()
    {
        takeAvatarData.Event += OnTakeAvatarDataEvent;
        avatarSwapped.Event += LoadAvatar;
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
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

    public void LoadAvatar(object sender, EventArgs e)
    {
        avatarRoots[activeAvatarIndex].SetActive(false);
        activeAvatarIndex++;
        if (activeAvatarIndex >= avatarsData.Count)
            activeAvatarIndex = 0;
        avatarRoots[activeAvatarIndex].SetActive(true);
        changeMapping.Trigger(this, avatarsData[activeAvatarIndex]);
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
