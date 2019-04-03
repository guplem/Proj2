using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraMovmentController cameraMovmentController;

    public void Setup(GameObject target, float velocity)
    {
        cameraMovmentController = new CameraMovmentController(this.gameObject, target, velocity);
    }

    public void SetCameraTarget(GameObject target)
    {
        cameraMovmentController.target = target;
    }

    public void SetCameraVelocity(float velocity)
    {
        cameraMovmentController.velocity = velocity;
    }

    private void Update()
    {
        if (cameraMovmentController != null)
            cameraMovmentController.Tick();
    }

    


    

}
