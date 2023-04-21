using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AvatarSwappingManager : MonoBehaviour
{
    [Header("Received Events")]
    [SerializeField] private string avatarDataKey;
    [SerializeField] private string swapAvatarKey;
    [Space]
    [Header("Sending Events")]
    [SerializeField] private string changeMappingKey;

    [SerializeField] private List<AvatarJointEvent> avatarsData = new List<AvatarJointEvent>();
    private readonly List<GameObject> avatarRoots = new List<GameObject>();

    private int activeAvatarIndex;

    private void OnEnable()
    {
        EventManager.AddListener(avatarDataKey, ReceiveAvatarData);
        //EventManager.AddListener(swapAvatarKey, SwapAvatar);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(avatarDataKey, ReceiveAvatarData);
        //EventManager.RemoveListener(swapAvatarKey, SwapAvatar);
    }

    private void SwapAvatar(GameEvent obj)
    {
        avatarRoots[activeAvatarIndex].SetActive(false);
        activeAvatarIndex++;
        if (activeAvatarIndex >= avatarsData.Count)
            activeAvatarIndex = 0;
        avatarRoots[activeAvatarIndex].SetActive(true);
        EventManager.Trigger(changeMappingKey, avatarsData[activeAvatarIndex]);
    }

    private void ReceiveAvatarData(GameEvent obj)
    {
        if (obj is not AvatarJointEvent args)
            return;
        avatarsData.Add(args);
        if (args.Sender is Component component)
            avatarRoots.Add(component.gameObject);
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        foreach (var avatar in avatarRoots)
        {
            avatar.SetActive(false);
        }
        avatarRoots[0].SetActive(true);
    }
}

[Serializable]
public class AvatarJointEvent : GameEvent
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

    public AvatarJointEvent(object sender, Transform leftShoulder, Transform rightShoulder) : base(sender)
    {
        LeftShoulder = leftShoulder;
        RightShoulder = rightShoulder;
    }
}
