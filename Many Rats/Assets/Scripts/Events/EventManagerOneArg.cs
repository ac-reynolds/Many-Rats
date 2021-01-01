using UnityEngine;
using UnityEngine.Events;

//event manager for event of type UnityEvent<T0>.  To specify additional events, extend UnityEvent<T0>
public class EventManagerOneArg<EventType, T0> where EventType : UnityEvent<T0> 
{
    private UnityEvent<T0> _event;

    //singleton class
    private static EventManagerOneArg<EventType, T0> instance = new EventManagerOneArg<EventType, T0>();
    public static EventManagerOneArg<EventType, T0> GetInstance() {
        return instance;
    }
    private EventManagerOneArg() {
        if (_event == null) {
            _event = new UnityEvent<T0>();
        }
    }

    public void InvokeEvent(T0 arg) {
        Debug.Log(typeof(EventType) + " with parameter " + arg);
        _event.Invoke(arg);
    }

    public void AddListener(UnityAction<T0> action) {
        _event.AddListener(action);
    }

    public void RemoveListener(UnityAction<T0> action) {
        _event.RemoveListener(action);
    }
}
