using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraMovmentController cameraMovementController;
    public Vector2 cameraOffset;
    public Vector2 cameraSpeed;
    [HideInInspector] public Camera mainCamera;

    public void Setup(CharacterManager target)
    {
        cameraMovementController = new CameraMovmentController(this, target);
        mainCamera = GetComponent<Camera>();

        if (mainCamera == null)
            Debug.LogWarning("Main camera not found", gameObject);
    }

    private void Update()
    {
        cameraMovementController.Tick();
    }

}
