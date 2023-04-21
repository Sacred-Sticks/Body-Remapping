using System.Collections;
using System.Collections.Generic;
using InputManagement.Inputs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private string swapAvatarKey;
    [SerializeField] private string loadAvatarKey;

    private readonly List<GameObject> avatarRoots = new List<GameObject>();
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
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        avatarRoots[loadedAvatarIndex].SetActive(true);
    }
}

public class AvatarLoadEvent : GameEvent
{
    public AvatarLoadEvent(object sender, GameObject avatarRoot) : base(sender)
    {
        AvatarRoot = avatarRoot;
    }
    
    public GameObject AvatarRoot { get; }
}

public class AvatarSwapEvent : GameEvent
{
    public AvatarSwapEvent(object sender) : base(sender)
    {
    }
}