using System.Collections;
using System.Collections.Generic;
using InputManagement;
using InputManagement.Inputs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string swapAvatarKey;
    [SerializeField] private string loadAvatarKey;
    [SerializeField] private string avatarChangeEventKey;

    private readonly List<GameObject> avatarRoots = new List<GameObject>();
    private readonly List<DirectionalFloat> avatarArmLengths = new List<DirectionalFloat>();
    private readonly List<BodyParts> allShoulders = new List<BodyParts>();
    
    private int loadedAvatarIndex;
    
    private void OnEnable()
    {
        EventManager.AddListener(loadAvatarKey, AddAvatar);
        EventManager.AddListener(swapAvatarKey, SwapAvatar);
    }
    
    private void OnDisable()
    {
        EventManager.RemoveListener(loadAvatarKey, AddAvatar);
        EventManager.RemoveListener(swapAvatarKey, SwapAvatar);
    }

    private void AddAvatar(GameEvent obj)
    {
        if (obj is not AvatarLoadEvent args)
            return;
        if (avatarRoots.Contains(args.AvatarRoot))
            return;
        avatarRoots.Add(args.AvatarRoot);
        avatarArmLengths.Add(args.ArmLengths);
        allShoulders.Add(args.Shoulders);
        args.AvatarRoot.SetActive(false);
    }
    
    private void SwapAvatar(GameEvent obj)
    {
        if (obj is not AvatarSwapEvent args)
            return;
        avatarRoots[loadedAvatarIndex].SetActive(false);
        loadedAvatarIndex++;
        if (loadedAvatarIndex == avatarRoots.Count)
            loadedAvatarIndex = 0;
        avatarRoots[loadedAvatarIndex].SetActive(true);
        EventManager.Trigger(avatarChangeEventKey, new AvatarSwappedEvent(this, avatarArmLengths[loadedAvatarIndex], allShoulders[loadedAvatarIndex]));
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        EventManager.Trigger(swapAvatarKey, new AvatarSwapEvent(this));
    }
}

public class AvatarLoadEvent : GameEvent
{
    public AvatarLoadEvent(object sender, GameObject avatarRoot, DirectionalFloat armLengths, BodyParts shoulders) : base(sender)
    {
        AvatarRoot = avatarRoot;
        ArmLengths = armLengths;
        Shoulders = shoulders;
    }
    
    public GameObject AvatarRoot { get; }
    public DirectionalFloat ArmLengths { get; }
    public BodyParts Shoulders { get; }
}

public class AvatarSwapEvent : GameEvent
{
    public AvatarSwapEvent(object sender) : base(sender)
    {
    }
}

public class AvatarSwappedEvent : GameEvent
{
    public AvatarSwappedEvent(object sender, DirectionalFloat armLengths, BodyParts shoulders) : base(sender)
    {
        ArmLengths = armLengths;
        Shoulders = shoulders;
    }

    public DirectionalFloat ArmLengths { get; }
    public BodyParts Shoulders { get; }
}