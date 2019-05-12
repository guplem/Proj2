using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraMovmentController cameraMovmentController;
    public Vector2 cameraOffset;
    public Camera mainCamera;

    public void Setup(GameObject target, float velocity)
    {
        cameraMovmentController = new CameraMovmentController(this, target, velocity);
        mainCamera = GetComponent<Camera>();

        if (mainCamera == null)
            Debug.LogWarning("Main camera not found", gameObject);
    }

    private void Update()
    {
        cameraMovmentController.Tick();
    }

}
