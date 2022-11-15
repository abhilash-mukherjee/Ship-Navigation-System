using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvent;
    [SerializeField]
    protected GameEvent gameEvent;
    public virtual void OnEventRaised()
    {
        Debug.Log($"Event recieved by Listener on {gameObject.name}");
        unityEvent?.Invoke();
    }
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
}