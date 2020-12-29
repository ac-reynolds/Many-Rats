using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public GameObject RatPrefab;
    public Camera Camera;

    void Update()
    {
        //if user clicks on game field, a rat will spawn at cursor location
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            Ray cameraRay = Camera.ScreenPointToRay(Input.mousePosition);
            Vector3 cameraRayOrigin = cameraRay.origin;
            Vector3 cameraRayDirection = cameraRay.direction;
            Vector3 newRatPosition = cameraRayOrigin - cameraRayOrigin.z / cameraRayDirection.z * cameraRayDirection;
            Instantiate(RatPrefab, newRatPosition, Quaternion.identity);
        }
    }
}
