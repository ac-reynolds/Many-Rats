using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    //singleton class
    private static EventManager instance = new EventManager();
    public static EventManager GetInstance() {
        return instance;
    }

    //available events
    UnityEvent<WalkableNode> CharmEvent;
    UnityEvent<Vector2> RatSpawnRequestEvent;
    UnityEvent<GameObject> RatSpawnedEvent;
    UnityEvent WitchDespawnEvent;

    //constructor
    private EventManager() {
        if (CharmEvent == null) {
            CharmEvent = new UnityEvent<WalkableNode>();
        }
        if (RatSpawnRequestEvent == null) {
            RatSpawnRequestEvent = new UnityEvent<Vector2>();
        }
        if (RatSpawnedEvent == null) {
            RatSpawnedEvent = new UnityEvent<GameObject>();
        }
        if (WitchDespawnEvent == null) {
            WitchDespawnEvent = new UnityEvent();
        }
    }

    public void InvokeCharmEvent(WalkableNode sourceNode) {
        CharmEvent.Invoke(sourceNode);
    }

    public void InvokeRatSpawnRequestEvent(Vector2 boardPosition) {
        RatSpawnRequestEvent.Invoke(boardPosition);
    }
    public void InvokeRatSpawnedEvent(GameObject rat) {
        RatSpawnedEvent.Invoke(rat);
    }

    public void InvokeWitchDespawnEvent() {
        WitchDespawnEvent.Invoke();
    }

    public void RegisterCharmEvent(UnityAction<WalkableNode> onCharm) {
        CharmEvent.AddListener(onCharm);
    }

    public void RegisterRatSpawnRequestEvent(UnityAction<Vector2> summonRat) {
        RatSpawnRequestEvent.AddListener(summonRat);
    }
    public void RegisterRatSpawnedEvent(UnityAction<GameObject> summonRat) {
        RatSpawnedEvent.AddListener(summonRat);
    }
    public void RegisterWitchDespawnEvent(UnityAction onDespawn) {
        WitchDespawnEvent.AddListener(onDespawn);
    }

    public void UnregisterCharmEvent(UnityAction<WalkableNode> onCharm) {
        CharmEvent.RemoveListener(onCharm);
    }
    public void UnregisterRatSpawnRequestEvent(UnityAction<Vector2> summonRat) {
        RatSpawnRequestEvent.RemoveListener(summonRat);
    }
    public void UnregisterRatSpawnedEvent(UnityAction<GameObject> summonRat) {
        RatSpawnedEvent.RemoveListener(summonRat);
    }
    public void UnregisterWitchDespawnEvent(UnityAction onDespawn) {
        WitchDespawnEvent.RemoveListener(onDespawn);

    }
}
