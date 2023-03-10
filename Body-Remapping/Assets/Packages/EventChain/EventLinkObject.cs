using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event Link", menuName = "Events/Link")]
public class EventLinkObject : ScriptableObject
{
    public delegate void ChainLink(object sender, ChainEventArgs e);

    public event ChainLink ChainLinkEvent;

    public void CallEvent(object sender, ChainEventArgs e)
    {
        ChainLinkEvent?.Invoke(sender, e);
    }
}

public class ChainEventArgs : EventArgs
{
    
}