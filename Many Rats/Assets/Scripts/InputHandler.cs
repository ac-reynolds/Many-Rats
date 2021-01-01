using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Camera Camera;
    private bool _hasLearnedSummonRat = false;
    private bool _hasLearnedDispelHorde = false;

    public RaycastHit2D hit;

    private void Start() {
        EventManagerZeroArgs<TriggerDialogue2Event>.GetInstance().AddListener(AfterDialogue2);
        //EventManagerZeroArgs<TriggerDialogue3Event>.GetInstance().AddListener(AfterDialogue3);
    }

    private void AfterDialogue2() {
        _hasLearnedSummonRat = true;
    }

    private void AfterDialogue3()
    {
        _hasLearnedDispelHorde = true;
    }

    void Update()
    {
        //if user clicks on game field, a rat will spawn at cursor location
        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Mouse0) && _hasLearnedSummonRat) {
            Ray cameraRay = Camera.ScreenPointToRay(Input.mousePosition);
            Vector3 cameraRayOrigin = cameraRay.origin;
            Vector3 cameraRayDirection = cameraRay.direction;
            Vector3 newRatPosition = cameraRayOrigin - cameraRayOrigin.z / cameraRayDirection.z * cameraRayDirection;
            EventManagerOneArg<RequestSpawnRatEvent, Vector2>.GetInstance().InvokeEvent(newRatPosition);
        }
        
        //if user right clicks on game field, despawn rat horde
        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray cameraRay = Camera.ScreenPointToRay(Input.mousePosition);
            Vector3 cameraRayOrigin = cameraRay.origin;
            Vector3 cameraRayDirection = cameraRay.direction;

            RaycastHit2D hit = Physics2D.Raycast(cameraRayOrigin, cameraRayDirection);
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("RatHorde"))
                {

                    hit.collider.gameObject.SetActive(false);
                }
            }
            /*if(Physics2D.Raycast(cameraRayOrigin,cameraRayDirection, out hit))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("RatHorde"))
                    hit.collider.gameObject.SetActive(false);
            }*/
        }
    }
}
