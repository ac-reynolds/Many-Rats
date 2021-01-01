using UnityEngine;
using UnityEngine.Events;

public class CarriageLoadingSuccessfulEvent : UnityEvent { }

//passes despawned GameObject
public class DespawnCarriageEvent : UnityEvent<GameObject> { }
public class DespawnPersonEvent : UnityEvent<GameObject> { }
public class DespawnRatEvent : UnityEvent<GameObject> { }
public class DespawnRatHordeEvent : UnityEvent<GameObject> { }
public class DespawnWitchEvent : UnityEvent<GameObject> { }

//passes world coordinates where object should be spawned
public class RequestSpawnRatEvent : UnityEvent<Vector2> { }
public class RequestSpawnRatHordeEvent : UnityEvent<Vector2> { }

//passes spawned GameObject
public class SpawnCarriageEvent : UnityEvent<GameObject> { }
public class SpawnPersonEvent : UnityEvent<GameObject> { }
public class SpawnRatEvent : UnityEvent<GameObject> { }
public class SpawnRatHordeEvent : UnityEvent<GameObject> { }
public class SpawnWitchEvent : UnityEvent<GameObject> { }

//for dialogue
public class TriggerDialogue1Event : UnityEvent { }
public class TriggerDialogue2Event : UnityEvent { }
public class TriggerDialogue3Event : UnityEvent { }
public class TriggerDialogue4Event : UnityEvent { }