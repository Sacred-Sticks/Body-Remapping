using System;
using UnityEngine;

public class SwapAvatar : MonoBehaviour
{
    [SerializeField] private string buttonKey;
    [SerializeField] private string avatarSwapKey;

    private void OnEnable()
    {
        EventManager.AddListener(buttonKey, OnButtonPressed);
    }

    private void OnButtonPressed(GameEvent obj)
    {
        if (obj is not PressDetectedEvent args)
            return;
        EventManager.Trigger(avatarSwapKey, new AvatarSwapEvent(this));
    }
}
