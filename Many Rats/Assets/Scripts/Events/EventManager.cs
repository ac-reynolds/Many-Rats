using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    //singleton class
    private static EventManager instance = new EventManager();
    public static EventManager GetInstance() {
        return instance;
    }

    //list of events
    UnityEvent<WalkableNode> CharmEvent;
    UnityEvent<Vector2> RatSpawnEvent;

    //constructor
    private EventManager() {
        if (CharmEvent == null) {
            CharmEvent = new UnityEvent<WalkableNode>();
        }
        if (RatSpawnEvent == null) {
            RatSpawnEvent = new UnityEvent<Vector2>();
        }
    }

    public void InvokeCharmEvent(WalkableNode sourceNode) {
        CharmEvent.Invoke(sourceNode);
    }

    public void InvokeRatSpawnEvent(Vector2 boardPosition) {
        RatSpawnEvent.Invoke(boardPosition);
    }

    public void RegisterCharmEvent(UnityAction<WalkableNode> onCharm) {
        CharmEvent.AddListener(onCharm);
    }

    public void RegisterRatSpawnEvent(UnityAction<Vector2> summonRat) {
        RatSpawnEvent.AddListener(summonRat);
    }
    
}
