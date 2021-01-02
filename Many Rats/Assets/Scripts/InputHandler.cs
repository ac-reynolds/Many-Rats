using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject CheesePrefab;
    public Camera Camera;
    private bool _hasLearnedSummonRat = false;
    private bool _hasLearnedDispelHorde = false;
    private bool _mayClickOnce = false;

    private void Start() {
        EventManagerZeroArgs<TriggerDialogue2Event>.GetInstance().AddListener(AfterDialogue2);
        EventManagerZeroArgs<AllowSingleRatSpawnEvent>.GetInstance().AddListener(AllowOneRatSpawn);
    }

    private void AfterDialogue2() {
        _hasLearnedSummonRat = true;
    }

    private void AllowOneRatSpawn() {
        _mayClickOnce = true;
    }

    void Update()
    {
        //if user clicks on game field, a rat will spawn at cursor location
        if (_mayClickOnce || Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Mouse0)) {

            _mayClickOnce = false;

            Ray cameraRay = Camera.ScreenPointToRay(Input.mousePosition);
            Vector3 cameraRayOrigin = cameraRay.origin;
            Vector3 cameraRayDirection = cameraRay.direction;
            Vector3 newRatPosition = cameraRayOrigin - cameraRayOrigin.z / cameraRayDirection.z * cameraRayDirection;
            EventManagerOneArg<RequestSpawnRatEvent, Vector2>.GetInstance().InvokeEvent(newRatPosition);
        }
    }
}
