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

    private void Update()
    {
        cameraMovmentController.Tick();
    }

}
