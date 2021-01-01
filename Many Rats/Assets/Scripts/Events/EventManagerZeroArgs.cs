using UnityEngine;
using UnityEngine.Events;

//event manager for event of type UnityEvent.  To specify additional events, extend UnityEvent
public class EventManagerZeroArgs<EventType> where EventType : UnityEvent
{
    private UnityEvent _event;

    //singleton class
    private static EventManagerZeroArgs<EventType> instance = new EventManagerZeroArgs<EventType>();
    public static EventManagerZeroArgs<EventType> GetInstance() {
        return instance;
    }
    private EventManagerZeroArgs() {
        if (_event == null) {
            _event = new UnityEvent();
        }
    }

    public void InvokeEvent() {
        Debug.Log(typeof(EventType));
        _event.Invoke();
    }

    public void AddListener(UnityAction action) {
        _event.AddListener(action);
    }

    public void RemoveListener(UnityAction action) {
        _event.RemoveListener(action);
    }
}
