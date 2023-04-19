using UnityEngine;
using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviour
{
    private static readonly Dictionary<string, Action<GameEvent>> gameEvents = new Dictionary<string, Action<GameEvent>>();

    public static void AddListener(string key, Action<GameEvent> listener)
    {
        if (!gameEvents.ContainsKey(key))
            gameEvents[key] = new Action<GameEvent>(listener);
        else
            gameEvents[key] += listener;
    }

    public static void RemoveListener(string key, Action<GameEvent> listener)
    {
        if (gameEvents.ContainsKey(key))
        {
            gameEvents[key] -= listener;
            return;
        }
        Debug.LogWarning($"No Game Event Found at Key \"{key}\"");
    }

    public static void Trigger(string key, GameEvent arguments)
    {
        if (gameEvents.ContainsKey(key))
        {
            gameEvents[key].Invoke(arguments);
        }
    }
}

public class GameEvent
{
    public GameEvent(object sender)
    {
        Sender = sender;
    }

    public object Sender { get; }
}
