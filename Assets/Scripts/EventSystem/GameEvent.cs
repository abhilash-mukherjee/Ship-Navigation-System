using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Event")]
public class GameEvent : ScriptableObject
{
    private List<EventListener> eventListeners = new List<EventListener>();
    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }
    public void UnregisterListener(EventListener _listener)
    {
        if (eventListeners.Find(p => p == _listener) != null)
        {
            eventListeners.Remove(_listener);
        }
    }
    public void RegisterListener(EventListener _listener)
    {
        if (eventListeners.Find(p => p == _listener) == null)
        {
            eventListeners.Add(_listener);
        }
    }
}
