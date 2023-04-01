using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OnEvent", menuName = "Events/Event Bus")]
public class EventBus : ScriptableObject
{
    public delegate void EventHandler(object sender, EventArgs e);

    public event EventHandler Event;

    public void Trigger(object sender, EventArgs e)
    {
        Event?.Invoke(sender, e);
    }
}