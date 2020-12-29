using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public GameObject RatPrefab;
    public GameObject CheesePrefab;
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

        //if user right clicks, cheese will spawn at cursor location if there is cheese available GameManager.cheeseAvailable
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (GameManager.cheeseAvailable > 0)
            {
                Ray cameraRay = Camera.ScreenPointToRay(Input.mousePosition);
                Vector3 cameraRayOrigin = cameraRay.origin;
                Vector3 cameraRayDirection = cameraRay.direction;
                Vector3 newCheesePosition = cameraRayOrigin - cameraRayOrigin.z / cameraRayDirection.z * cameraRayDirection;
                Instantiate(CheesePrefab, newCheesePosition, Quaternion.identity);
                GameManager.cheeseAvailable--;
            }
        }
    }
}
