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
    UnityEvent<GameObject> PersonDieEvent;
    UnityEvent<Vector2> RatHordeSpawnRequestEvent;
    UnityEvent<GameObject> RatHordeSpawnedEvent;
    UnityEvent<Vector2> RatSpawnRequestEvent;
    UnityEvent<GameObject> RatSpawnedEvent;
    UnityEvent<WalkableNode> WitchDespawnEvent;

    //constructor
    private EventManager() {
        if (CharmEvent == null) {
            CharmEvent = new UnityEvent<WalkableNode>();
        }
        if (PersonDieEvent == null) {
            PersonDieEvent = new UnityEvent<GameObject>();
        }
        if (RatHordeSpawnRequestEvent == null) {
            RatHordeSpawnRequestEvent = new UnityEvent<Vector2>();
        }
        if (RatHordeSpawnedEvent == null) {
            RatHordeSpawnedEvent = new UnityEvent<GameObject>();
        }
        if (RatSpawnRequestEvent == null) {
            RatSpawnRequestEvent = new UnityEvent<Vector2>();
        }
        if (RatSpawnedEvent == null) {
            RatSpawnedEvent = new UnityEvent<GameObject>();
        }
        if (WitchDespawnEvent == null) {
            WitchDespawnEvent = new UnityEvent<WalkableNode>();
        }
    }

    public void InvokeCharmEvent(WalkableNode sourceNode) {
        CharmEvent.Invoke(sourceNode);
    }
    public void InvokePersonDieEvent(GameObject person) {
        PersonDieEvent.Invoke(person);
    }
    public void InvokeRatHordeSpawnRequestEvent(Vector2 boardPosition) {
        RatHordeSpawnRequestEvent.Invoke(boardPosition);
    }
    public void InvokeRatHordeSpawnedEvent(GameObject o) {
        RatHordeSpawnedEvent.Invoke(o);
    }
    public void InvokeRatSpawnRequestEvent(Vector2 boardPosition) {
        RatSpawnRequestEvent.Invoke(boardPosition);
    }
    public void InvokeRatSpawnedEvent(GameObject rat) {
        RatSpawnedEvent.Invoke(rat);
    }

    public void InvokeWitchDespawnEvent(WalkableNode node) {
        WitchDespawnEvent.Invoke(node);
    }

    public void RegisterCharmEvent(UnityAction<WalkableNode> onCharm) {
        CharmEvent.AddListener(onCharm);
    }
    public void RegisterPersonDieEvent(UnityAction<GameObject> onDeath) {
        PersonDieEvent.AddListener(onDeath);
    }
    public void RegisterRatHordeSpawnRequestEvent(UnityAction<Vector2> summonRat) {
        RatHordeSpawnRequestEvent.AddListener(summonRat);
    }
    public void RegisterRatHordeSpawnedEvent(UnityAction<GameObject> summonRat) {
        RatHordeSpawnedEvent.AddListener(summonRat);
    }
    public void RegisterRatSpawnRequestEvent(UnityAction<Vector2> summonRat) {
        RatSpawnRequestEvent.AddListener(summonRat);
    }
    public void RegisterRatSpawnedEvent(UnityAction<GameObject> summonRat) {
        RatSpawnedEvent.AddListener(summonRat);
    }
    public void RegisterWitchDespawnEvent(UnityAction<WalkableNode> onDespawn) {
        WitchDespawnEvent.AddListener(onDespawn);
    }

    public void UnregisterCharmEvent(UnityAction<WalkableNode> onCharm) {
        CharmEvent.RemoveListener(onCharm);
    }
    public void UnregisterPersonDieEvent(UnityAction<GameObject> onDeath) {
        PersonDieEvent.RemoveListener(onDeath);
    }
    public void UnregisterRatHordeSpawnRequestEvent(UnityAction<Vector2> summonRat) {
        RatHordeSpawnRequestEvent.RemoveListener(summonRat);
    }
    public void UnregisterRatHordeSpawnedEvent(UnityAction<GameObject> summonRat) {
        RatHordeSpawnedEvent.RemoveListener(summonRat);
    }
    public void UnregisterRatSpawnRequestEvent(UnityAction<Vector2> summonRat) {
        RatSpawnRequestEvent.RemoveListener(summonRat);
    }
    public void UnregisterRatSpawnedEvent(UnityAction<GameObject> summonRat) {
        RatSpawnedEvent.RemoveListener(summonRat);
    }
    public void UnregisterWitchDespawnEvent(UnityAction<WalkableNode> onDespawn) {
        WitchDespawnEvent.RemoveListener(onDespawn);

    }
}
